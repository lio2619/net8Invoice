using invoicing.DB.DBContext;
using invoicing.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using UglyToad.PdfPig;

namespace invoicing.Service
{
    /// <summary>
    /// PDF 匯入服務實作（關貿專用）
    /// </summary>
    public class PdfImportService : IPdfImportService
    {
        private readonly InvoicIngDbContext _dbContext;

        public PdfImportService(InvoicIngDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 從 PDF 檔案匯入資料
        /// </summary>
        public async Task<List<PdfImportResult>> ImportFromPdfAsync(string filePath)
        {
            var results = new List<PdfImportResult>();

            // PDF 座標範圍設定
            var xRanges = new List<(double MinX, double MaxX, int CheckPattern)>
            {
                (43, 125, 0),  // 國碼
                (331, 345, 1), // 數量
                (407, 436, 2)  // 單價
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

                    var storeName = words[10].Text;
                    var number = words[18].Text;
                    var remarks = words[20].Text;

                    var filteredWords = words
                        .Where(word =>
                            xRanges.Any(range =>
                                word.BoundingBox.Left >= range.MinX && word.BoundingBox.Right <= range.MaxX &&
                                word.BoundingBox.Top >= minY && word.BoundingBox.Bottom <= maxY &&
                                (range.CheckPattern > 0 || regex.IsMatch(word.Text)) && 
                                (range.CheckPattern < 2 || regexNumber.IsMatch(word.Text))))
                        .Select(word => word.Text);

                    if (words[4].Text == words[6].Text)
                    {
                        ans.AddRange(filteredWords);
                        var customerName = await GetCustomerNameAsync(storeName, number);
                        var orderNumber = await SavePdfDataAsync(ans, customerName, remarks);
                        
                        results.Add(new PdfImportResult
                        {
                            CustomerName = customerName,
                            PoNumber = remarks,
                            NewOrderNumber = orderNumber
                        });
                        
                        ans.Clear();
                    }
                    else
                    {
                        ans.AddRange(filteredWords);
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// 取得客戶名稱
        /// </summary>
        private async Task<string> GetCustomerNameAsync(string name, string number)
        {
            var searchPattern = $"%{name}({number.Substring(0, 4)}%";
            var customer = await _dbContext.Customers
                .FirstOrDefaultAsync(c => EF.Functions.Like(c.CompanyFullName ?? "", searchPattern));

            return customer?.CompanyFullName ?? string.Empty;
        }

        /// <summary>
        /// 儲存 PDF 資料至資料庫
        /// </summary>
        private async Task<string> SavePdfDataAsync(List<string> list, string storeName, string poNumber)
        {
            // 取得新的單子編號（使用新編號系統）
            string orderType = "出貨單";
            string dateStr = DateTime.Now.ToString("yyyyMMdd");
            string newOrderNumberStr = await GenerateNewOrderNumberAsync(dateStr, orderType);

            // 分組：每 3 個元素為一組（國碼、數量、單價）
            var grouped = list
                .Select((value, index) => new { value, index })
                .GroupBy(x => x.index / 3)
                .Select(g => g.Select(x => x.value).ToList())
                .Where(g => g.Count == 3)
                .ToList();

            decimal totalCost = 0m;

            // 先儲存主檔以取得 Id
            var order = new Models.Entity.CustomerOrder
            {
                Date = dateStr,
                Time = DateTime.Now.ToString("yyyyMMddHHmmss"),
                Customer = storeName,
                OrderName = orderType,
                Remark = poNumber,
                TotalAmount = "0", // 稍後更新
                OrderNumber = null,                 // 新資料不使用舊編號
                NewOrderNumber = newOrderNumberStr, // 使用新編號系統
                Deleted = "0",
                Customs = "1"
            };
            _dbContext.CustomerOrders.Add(order);
            await _dbContext.SaveChangesAsync();

            foreach (var group in grouped)
            {
                var productCode = group[2];
                var quantity = group[0];
                var unitPrice = group[1];
                var cost = decimal.Parse(quantity) * decimal.Parse(unitPrice);
                totalCost += cost;

                // 查詢貨品資訊
                var product = await _dbContext.Products
                    .FirstOrDefaultAsync(p => p.ProductCode == productCode);

                string productName, unit;

                if (product == null)
                {
                    productName = "空白";
                    unit = "空白";

                    // 新增貨品
                    var newProduct = new Models.Entity.Product
                    {
                        ProductCode = productCode,
                        ProductName = productName,
                        Unit = unit
                    };
                    _dbContext.Products.Add(newProduct);
                }
                else
                {
                    productName = product.ProductName ?? string.Empty;
                    unit = product.Unit ?? string.Empty;
                }

                // 新增明細（使用 CustomerOrderId 關聯）
                var detail = new Models.Entity.OrderDetail
                {
                    OrderNumber = null,
                    CustomerOrderId = order.Id,
                    ProductCode = productCode,
                    ProductName = productName,
                    Quantity = quantity,
                    Unit = unit,
                    UnitPrice = unitPrice,
                    Amount = cost.ToString(),
                    Remark = string.Empty
                };
                _dbContext.OrderDetails.Add(detail);
            }

            // 更新主檔總金額
            order.TotalAmount = totalCost.ToString();
            _dbContext.CustomerOrders.Update(order);

            await _dbContext.SaveChangesAsync();

            return newOrderNumberStr;
        }

        /// <summary>
        /// 計算新的 NewOrderNumber（每月20號重置，4位數格式）
        /// </summary>
        private async Task<string> GenerateNewOrderNumberAsync(string dateStr, string orderType)
        {
            // 解析日期
            if (!DateTime.TryParseExact(dateStr, "yyyyMMdd", null,
                System.Globalization.DateTimeStyles.None, out var orderDate))
            {
                orderDate = DateTime.Now;
            }

            // 計算週期的起始日期（每月20號為分隔點）
            DateTime periodStart;
            DateTime periodEnd;

            if (orderDate.Day >= 20)
            {
                // 當月20號 ~ 下月19號
                periodStart = new DateTime(orderDate.Year, orderDate.Month, 20);
                periodEnd = periodStart.AddMonths(1).AddDays(-1);
            }
            else
            {
                // 上月20號 ~ 當月19號
                periodStart = new DateTime(orderDate.Year, orderDate.Month, 1).AddMonths(-1);
                periodStart = new DateTime(periodStart.Year, periodStart.Month, 20);
                periodEnd = new DateTime(orderDate.Year, orderDate.Month, 19);
            }

            string periodStartStr = periodStart.ToString("yyyyMMdd");
            string periodEndStr = periodEnd.ToString("yyyyMMdd");

            // 查詢該週期內同類型單子的最大 NewOrderNumber
            var existingOrders = await _dbContext.CustomerOrders
                .Where(x => x.OrderName == orderType
                         && x.NewOrderNumber != null
                         && string.Compare(x.Date, periodStartStr) >= 0
                         && string.Compare(x.Date, periodEndStr) <= 0)
                .Select(x => x.NewOrderNumber)
                .ToListAsync();

            int maxNumber = 0;
            foreach (var numStr in existingOrders)
            {
                if (int.TryParse(numStr, out var num) && num > maxNumber)
                {
                    maxNumber = num;
                }
            }

            // 回傳下一個號碼（4位數格式）
            return (maxNumber + 1).ToString("D4");
        }
    }
}
