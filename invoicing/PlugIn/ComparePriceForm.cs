using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using invoicing.DB.DBContext;
using Microsoft.EntityFrameworkCore;
using UglyToad.PdfPig;

namespace invoicing.PlugIn
{
    public partial class ComparePriceForm : Form
    {
        private readonly InvoicIngDbContext _dbContext;

        private class CompareDisplayRow
        {
            public string Barcode { get; set; }
            public string ProductName { get; set; }
            public string InvoicePrice { get; set; }
            public string PriceA { get; set; }
            public Color DisplayColor { get; set; }
        }

        public ComparePriceForm(InvoicIngDbContext dbContext)
        {
            InitializeComponent();
            _dbContext = dbContext;

            // 綁定按鈕點擊事件
            btnRead.Click += btnRead_Click;
        }

        private async void btnRead_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Filter = "PDF 檔案 (*.pdf)|*.pdf"
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;

            btnRead.Enabled = false;
            rtbErrorShow.Clear();
            rtbErrorShow.AppendText("正在讀取並比對單子價格，請稍候...\r\n");

            try
            {
                string filePath = dialog.FileName;

                // 1. 在背景執行緒解析 PDF
                var pdfItems = await Task.Run(() => ParsePdfProducts(filePath));

                if (pdfItems == null || pdfItems.Count == 0)
                {
                    rtbErrorShow.Clear();
                    rtbErrorShow.AppendText("此次沒有讀取到任何商品資訊");
                    return;
                }

                // 2. 收集所有不重複且非空白的國碼
                var barcodes = pdfItems
                    .Select(item => item.Barcode)
                    .Where(code => !string.IsNullOrEmpty(code) && !code.StartsWith("空白"))
                    .Distinct()
                    .ToList();

                // 3. 一次性查詢資料庫
                var products = await _dbContext.Products
                    .Where(p => barcodes.Contains(p.ProductCode))
                    .ToListAsync();

                var productDict = products.ToDictionary(p => p.ProductCode, p => p);
                var displayRows = new List<CompareDisplayRow>();

                // 4. 逐項比對並整理要顯示的資料行
                foreach (var item in pdfItems)
                {
                    string barcode = item.Barcode;
                    decimal invoicePrice = item.Price;

                    // 檢查是否為空白商品 (無法成功解析出國碼)
                    if (string.IsNullOrEmpty(barcode) || barcode.StartsWith("空白"))
                    {
                        displayRows.Add(new CompareDisplayRow
                        {
                            Barcode = barcode ?? string.Empty,
                            ProductName = "[條碼解析失敗/空白]",
                            InvoicePrice = invoicePrice.ToString("F2"),
                            PriceA = "[無]",
                            DisplayColor = Color.DarkOrange
                        });
                        continue;
                    }

                    if (productDict.TryGetValue(barcode, out var product))
                    {
                        string productName = product.ProductName ?? "未設定品名";
                        decimal? priceA = product.PriceA;

                        if (priceA == null || priceA.Value == 0m)
                        {
                            displayRows.Add(new CompareDisplayRow
                            {
                                Barcode = barcode,
                                ProductName = productName,
                                InvoicePrice = invoicePrice.ToString("F2"),
                                PriceA = "0.00 (未設定)",
                                DisplayColor = Color.DarkGoldenrod
                            });
                        }
                        else
                        {
                            // 虧錢判定：單子售價比售價A低 6% 以上 (單子售價 < PriceA * 0.94)
                            if (invoicePrice < priceA.Value * 0.94m)
                            {
                                displayRows.Add(new CompareDisplayRow
                                {
                                    Barcode = barcode,
                                    ProductName = productName,
                                    InvoicePrice = invoicePrice.ToString("F2"),
                                    PriceA = priceA.Value.ToString("F2"),
                                    DisplayColor = Color.Red
                                });
                            }
                        }
                    }
                    else
                    {
                        // 找不到商品
                        displayRows.Add(new CompareDisplayRow
                        {
                            Barcode = barcode,
                            ProductName = "[找不到商品]",
                            InvoicePrice = invoicePrice.ToString("F2"),
                            PriceA = "[無]",
                            DisplayColor = Color.DarkOrange
                        });
                    }
                }

                // 5. 輸出顯示結果
                rtbErrorShow.Clear();
                if (displayRows.Count == 0)
                {
                    AppendColorText("此次沒有會虧錢的商品", Color.Green, true);
                    return;
                }

                int tab1 = 0;
                int tab2 = 0;
                int tab3 = 0;
                int padding = 35; // 欄位間距
                float maxNameWidth = 0f;

                using (Graphics g = rtbErrorShow.CreateGraphics())
                {
                    Font font = rtbErrorShow.Font;

                    float maxCol0Width = g.MeasureString("國碼", font).Width;
                    float maxCol1Width = g.MeasureString("品名", font).Width;
                    float maxCol2Width = g.MeasureString("單子售價", font).Width;

                    foreach (var row in displayRows)
                    {
                        maxCol0Width = Math.Max(maxCol0Width, g.MeasureString(row.Barcode, font).Width);
                        maxCol1Width = Math.Max(maxCol1Width, g.MeasureString(row.ProductName, font).Width);
                        maxCol2Width = Math.Max(maxCol2Width, g.MeasureString(row.InvoicePrice, font).Width);
                    }

                    tab1 = (int)(maxCol0Width + padding);
                    tab2 = tab1 + 380; // 品名欄位固定為 354 像素寬度
                    maxNameWidth = 380f; // 限制品名最大寬度為 354 像素
                    tab3 = tab2 + (int)(maxCol2Width + padding);

                    rtbErrorShow.SelectionTabs = new int[] { tab1, tab2, tab3 };
                }

                // 印出表頭 (深灰色、粗體)
                Color headerColor = Color.FromArgb(64, 64, 64);
                AppendColorText("國碼\t品名\t單子售價\t售價A\r\n", headerColor, true);
                AppendColorText("--------------------------------------------------------------------------------------------------------\r\n", headerColor);

                using (Graphics g = rtbErrorShow.CreateGraphics())
                {
                    Font font = rtbErrorShow.Font;

                    // 逐行印出資料
                    foreach (var row in displayRows)
                    {
                        string truncatedName = TruncateProductName(row.ProductName, font, maxNameWidth, g);
                        string lineText = $"{row.Barcode}\t{truncatedName}\t{row.InvoicePrice}\t{row.PriceA}\r\n";
                        AppendColorText(lineText, row.DisplayColor);
                    }
                }
            }
            catch (Exception ex)
            {
                rtbErrorShow.Clear();
                rtbErrorShow.AppendText($"讀取或比對失敗：{ex.Message}");
            }
            finally
            {
                btnRead.Enabled = true;
            }
        }

        /// <summary>
        /// 解析 PDF 中的商品與價錢 (仿照 PdfImportService.ImportFromPdfNewAsync 邏輯)
        /// </summary>
        private List<(string Barcode, decimal Price)> ParsePdfProducts(string filePath)
        {
            var allProducts = new List<(string Barcode, decimal Price)>();

            // PDF 座標範圍設定
            var xRanges = new List<(double MinX, double MaxX, int CheckPattern)>
            {
                (43, 125, 0),  // 國碼
                (331, 345, 1), // 數量
                (400, 436, 2)  // 單價
            };
            double minY = 232;
            double maxY = 693;

            var regex = new Regex(@"^\d{13}$");
            var regexNumber = new Regex(@"^\d+(\.\d+)?$");
            var ans = new List<string>();

            using (var document = PdfDocument.Open(filePath))
            {
                foreach (var page in document.GetPages())
                {
                    var words = page.GetWords().ToList();

                    if (words.Count < 21) continue;

                    var storeName = words[9].Text;  //客戶名稱
                    var number = words[17].Text;    //廠編
                    var remarks = words[19].Text;   //單子代號

                    var filteredWords = words
                        .Where(word =>
                            xRanges.Any(range =>
                                word.BoundingBox.Left >= range.MinX && word.BoundingBox.Right <= range.MaxX &&
                                word.BoundingBox.Top >= minY && word.BoundingBox.Bottom <= maxY &&
                                (range.CheckPattern > 0 || regex.IsMatch(word.Text)) &&
                                (range.CheckPattern < 2 || regexNumber.IsMatch(word.Text))))
                        .Select(word => word.Text);

                    var temp = words[5].Text.LastOrDefault();
                    string totalPageNumber = string.Empty;
                    if (temp != '\0')
                    {
                        totalPageNumber = temp.ToString();
                    }

                    if (words[4].Text == totalPageNumber) // 這兩個是判斷目前的頁數是否是最後一頁
                    {
                        ans.AddRange(filteredWords);

                        // 分組解析此訂單
                        var grouped = GroupPdfData(ans);
                        foreach (var group in grouped)
                        {
                            var productCode = group[2];
                            var unitPriceStr = group[1];
                            if (decimal.TryParse(unitPriceStr, out decimal price))
                            {
                                allProducts.Add((productCode, price));
                            }
                            else
                            {
                                allProducts.Add((productCode, 0m));
                            }
                        }

                        ans.Clear();
                    }
                    else
                    {
                        ans.AddRange(filteredWords);
                    }
                }
            }

            return allProducts;
        }

        /// <summary>
        /// 分組邏輯
        /// </summary>
        private List<List<string>> GroupPdfData(List<string> list)
        {
            var grouped = new List<List<string>>();
            int i = 0;

            while (i < list.Count)
            {
                // 1. 取出數量與單價 (基本組成)
                string qty = list[i];
                string price = (i + 1 < list.Count) ? list[i + 1] : "0";

                // 2. 檢查是否有第三個元素，且是否符合條碼規則
                bool hasValidBarcode = false;
                string barcode = $"空白{i}"; // 預設為空白

                if (i + 2 < list.Count)
                {
                    string potentialBarcode = list[i + 2];
                    // 判斷：4 開頭 且 長度為 13
                    if (potentialBarcode.StartsWith("4") && potentialBarcode.Length == 13)
                    {
                        barcode = potentialBarcode;
                        hasValidBarcode = true;
                    }
                }

                // 3. 加入群組
                grouped.Add(new List<string> { qty, price, barcode });

                // 4. 關鍵：決定指標移動幾格
                if (hasValidBarcode)
                {
                    i += 3; // 正常的組，跳過 數量、單價、條碼
                }
                else
                {
                    i += 2; // 缺條碼的組，只跳過 數量、單價，原本的 i+2 會變成下一組
                }
            }

            return grouped;
        }

        /// <summary>
        /// 輔助富文字格式化輸出
        /// </summary>
        private void AppendColorText(string text, Color color, bool bold = false)
        {
            rtbErrorShow.SelectionStart = rtbErrorShow.TextLength;
            rtbErrorShow.SelectionLength = 0;
            rtbErrorShow.SelectionColor = color;
            if (bold)
            {
                rtbErrorShow.SelectionFont = new Font(rtbErrorShow.Font, FontStyle.Bold);
            }
            else
            {
                rtbErrorShow.SelectionFont = new Font(rtbErrorShow.Font, FontStyle.Regular);
            }
            rtbErrorShow.AppendText(text);
            rtbErrorShow.SelectionColor = rtbErrorShow.ForeColor; // 還原
        }

        /// <summary>
        /// 使用 Graphics.MeasureString 進行像素級別的字元截斷，並在超出時自動附加上 "..."
        /// </summary>
        private string TruncateProductName(string name, Font font, float maxWidth, Graphics g)
        {
            if (string.IsNullOrEmpty(name)) return string.Empty;

            float nameWidth = g.MeasureString(name, font).Width;
            if (nameWidth <= maxWidth) return name;

            string suffix = "...";
            float suffixWidth = g.MeasureString(suffix, font).Width;

            if (maxWidth <= suffixWidth) return suffix;

            float limit = maxWidth - suffixWidth;

            // 逐步遞減字元直到寬度符合限制
            for (int len = name.Length - 1; len > 0; len--)
            {
                string sub = name.Substring(0, len);
                if (g.MeasureString(sub, font).Width <= limit)
                {
                    return sub + suffix;
                }
            }

            return suffix;
        }
    }
}
