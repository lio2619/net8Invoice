using ExcelDataReader;
using invoicing.Models.DTO;
using invoicing.Service.Interface;
using System.Data;
using System.Text;

namespace invoicing.Service
{
    /// <summary>
    /// Excel 匯入服務實作（使用 ExcelDataReader 支援舊版 Excel 格式）
    /// </summary>
    public class ExcelImportService : IExcelImportService
    {
        static ExcelImportService()
        {
            // 註冊編碼提供者以支援舊版 Excel 格式
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        /// <summary>
        /// 從 Excel 檔案匯入資料
        /// </summary>
        public async Task<ImportedOrderData> ImportFromExcelAsync(string filePath, ExcelImportConfig config, IPluginFormService pluginFormService)
        {
            var result = new ImportedOrderData
            {
                SourceTable = pluginFormService.CreateSourceDataTable()
            };

            var excelData = new List<Dictionary<string, string>>();

            // 使用 ExcelDataReader 讀取 Excel 檔案（支援舊版格式）
            await Task.Run(() =>
            {
                using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
                using var reader = ExcelReaderFactory.CreateReader(stream);

                // 解析起始列（例如 "A16:K" 取 16）
                int startRow = ParseStartRow(config.SheetRange);

                // 讀取資料
                int currentRow = 0;
                while (reader.Read())
                {
                    currentRow++;
                    if (currentRow < startRow) continue;

                    var rowData = new Dictionary<string, string>();

                    // 讀取需要的欄位（F1=第1欄, F2=第2欄...）
                    var columnMappings = ParseColumnMappings(config.SelectColumns);
                    foreach (var mapping in columnMappings)
                    {
                        int colIndex = ColumnNameToIndex(mapping);
                        var value = reader.GetValue(colIndex);
                        rowData[mapping] = value?.ToString() ?? string.Empty;
                    }

                    // 檢查是否為數字（貨品編號）
                    var productCodeValue = rowData.GetValueOrDefault(config.ProductCodeColumn, "");
                    if (!string.IsNullOrEmpty(productCodeValue) && IsNumeric(productCodeValue))
                    {
                        excelData.Add(rowData);
                    }
                }
            });

            // 處理匯入的資料
            double totalAmount = 0.0;

            foreach (var rowData in excelData)
            {
                var productCode = rowData.GetValueOrDefault(config.ProductCodeColumn, "");
                if (string.IsNullOrEmpty(productCode)) continue;

                var productName = rowData.GetValueOrDefault(config.ProductNameColumn, "");
                var quantity = rowData.GetValueOrDefault(config.QuantityColumn, "0");
                var unit = rowData.GetValueOrDefault(config.UnitColumn, "");

                // 查詢貨品資訊
                var productInfo = await pluginFormService.GetProductInfoAsync(productCode);

                var detail = new ImportedOrderDetail
                {
                    ProductCode = productCode,
                    Quantity = quantity
                };

                if (productInfo == null)
                {
                    detail.ProductName = productName;
                    detail.Unit = unit;
                    detail.UnitPrice = "0";
                    detail.Amount = "0";

                    // 新增貨品至資料庫
                    await pluginFormService.InsertProductAsync(productCode, productName, unit);
                }
                else
                {
                    // 貨品存在，使用資料庫中的資料
                    if (!double.TryParse(productInfo.StandardPrice, out var standardPrice)) standardPrice = 0;
                    if (!double.TryParse(quantity, out var qty)) qty = 0;
                    var amount = standardPrice * qty;

                    detail.ProductName = productInfo.ProductName;
                    detail.Unit = productInfo.Unit;
                    detail.UnitPrice = decimal.Parse(productInfo.StandardPrice).ToString("0.################");
                    detail.Amount = amount.ToString();

                    // 查詢建議售價
                    var suggestedPrice = await pluginFormService.GetSuggestedPriceAsync(productInfo.StandardPrice);
                    detail.Remark = suggestedPrice ?? "";

                    totalAmount += amount;
                }

                result.Details.Add(detail);

                // 新增至來源資料表
                var sourceRow = result.SourceTable.NewRow();
                sourceRow["貨品編號"] = detail.ProductCode;
                sourceRow["品名"] = detail.ProductName;
                sourceRow["基本單位"] = detail.Unit;
                sourceRow["數量"] = detail.Quantity;
                sourceRow["單價"] = detail.UnitPrice;
                sourceRow["金額"] = detail.Amount;
                sourceRow["備註"] = detail.Remark;
                result.SourceTable.Rows.Add(sourceRow);
            }

            result.TotalAmount = totalAmount;
            return result;
        }

        /// <summary>
        /// 解析起始列號
        /// </summary>
        private int ParseStartRow(string sheetRange)
        {
            // 例如 "A16:K" 取 16
            var match = System.Text.RegularExpressions.Regex.Match(sheetRange, @"\d+");
            return match.Success ? int.Parse(match.Value) : 1;
        }

        /// <summary>
        /// 解析欄位對應（"F1, F3, F6" -> ["F1", "F3", "F6"]）
        /// </summary>
        private List<string> ParseColumnMappings(string selectColumns)
        {
            return selectColumns.Split(',')
                .Select(c => c.Trim())
                .ToList();
        }

        /// <summary>
        /// 欄位名稱轉索引（F1=0, F2=1, F3=2...）
        /// </summary>
        private int ColumnNameToIndex(string columnName)
        {
            // F1, F2, F3... 對應第 0, 1, 2... 欄
            var match = System.Text.RegularExpressions.Regex.Match(columnName, @"F(\d+)");
            if (match.Success && int.TryParse(match.Groups[1].Value, out var num))
            {
                return num - 1; // F1 = 第 0 欄
            }
            return 0;
        }

        /// <summary>
        /// 檢查是否為數字
        /// </summary>
        private bool IsNumeric(string value)
        {
            return double.TryParse(value, out _);
        }
    }
}
