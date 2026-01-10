using invoicing.DB.DBContext;
using invoicing.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace invoicing.Service
{
    /// <summary>
    /// 外掛表單共用服務實作
    /// </summary>
    public class PluginFormService : IPluginFormService
    {
        private readonly InvoicIngDbContext _dbContext;

        public PluginFormService(InvoicIngDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 建立來源資料表結構
        /// </summary>
        public DataTable CreateSourceDataTable()
        {
            var dataTable = new DataTable();
            string[] columns = { "貨品編號", "品名", "基本單位", "數量", "單價", "金額", "備註" };
            
            foreach (var columnName in columns)
            {
                var column = new DataColumn
                {
                    DataType = typeof(string),
                    Caption = columnName,
                    ColumnName = columnName
                };
                dataTable.Columns.Add(column);
            }
            
            return dataTable;
        }

        /// <summary>
        /// 載入客戶清單
        /// </summary>
        public async Task<List<string>> LoadCustomersAsync()
        {
            return await _dbContext.Customers
                .Where(c => c.IsDeleted != true)
                .OrderBy(c => c.CompanyFullName)
                .Select(c => c.CompanyFullName ?? string.Empty)
                .ToListAsync();
        }

        /// <summary>
        /// 儲存訂單至資料庫
        /// </summary>
        public async Task<string> SaveOrderAsync(DateTime date, string customerName, string remark, string totalAmount, DataTable sourceTable)
        {
            // 取得新的單子編號（使用新編號系統）
            string dateStr = date.ToString("yyyyMMdd");
            string newOrderNumberStr = await GenerateNewOrderNumberAsync(dateStr, "出貨單");

            // 建立主檔
            var order = new Models.Entity.CustomerOrder
            {
                Date = dateStr,
                Time = DateTime.Now.ToString("yyyyMMddHHmmss"),
                Customer = customerName,
                OrderName = "出貨單",
                Remark = remark,
                TotalAmount = totalAmount,
                OrderNumber = null,                 // 新資料不使用舊編號
                NewOrderNumber = newOrderNumberStr, // 使用新編號系統
                Deleted = "0"
            };

            _dbContext.CustomerOrders.Add(order);
            await _dbContext.SaveChangesAsync();

            // 儲存明細（使用 CustomerOrderId 關聯）
            foreach (DataRow row in sourceTable.Rows)
            {
                var detail = new Models.Entity.OrderDetail
                {
                    OrderNumber = null,
                    CustomerOrderId = order.Id,
                    ProductCode = row["貨品編號"]?.ToString() ?? string.Empty,
                    ProductName = row["品名"]?.ToString() ?? string.Empty,
                    Unit = row["基本單位"]?.ToString() ?? string.Empty,
                    Quantity = row["數量"]?.ToString() ?? string.Empty,
                    UnitPrice = row["單價"]?.ToString() ?? string.Empty,
                    Amount = row["金額"]?.ToString() ?? string.Empty,
                    Remark = row["備註"]?.ToString() ?? string.Empty
                };
                _dbContext.OrderDetails.Add(detail);
            }

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

        /// <summary>
        /// 取得客戶資訊
        /// </summary>
        public async Task<CustomerInfo?> GetCustomerInfoAsync(string customerName)
        {
            var customer = await _dbContext.Customers
                .FirstOrDefaultAsync(c => c.CompanyFullName == customerName);

            if (customer == null) return null;

            return new CustomerInfo
            {
                Phone = customer.Phone1 ?? string.Empty,
                Fax = customer.FaxNumber ?? string.Empty,
                Address = customer.DeliveryAddress ?? string.Empty
            };
        }

        /// <summary>
        /// 取得訂單編號
        /// </summary>
        public async Task<int?> GetOrderNumberAsync(DateTime date, string customerName, string remark, string totalAmount)
        {
            var order = await _dbContext.CustomerOrders
                .FirstOrDefaultAsync(o => 
                    o.Date == date.ToString("yyyyMMdd") &&
                    o.Customer == customerName &&
                    o.OrderName == "出貨單" &&
                    o.Remark == remark &&
                    o.TotalAmount == totalAmount);

            if (order == null) return null;
            
            // 優先使用 NewOrderNumber
            if (!string.IsNullOrEmpty(order.NewOrderNumber))
            {
                return int.TryParse(order.NewOrderNumber, out var newNum) ? newNum : null;
            }
            
            // 向後相容：舊資料使用 OrderNumber
            return order.OrderNumber != null && int.TryParse(order.OrderNumber, out var num) ? num : null;
        }

        /// <summary>
        /// 新增貨品主檔
        /// </summary>
        public async Task InsertProductAsync(string productCode, string productName, string unit)
        {
            var product = new Models.Entity.Product
            {
                ProductCode = productCode,
                ProductName = productName,
                Unit = unit
            };
            
            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 取得貨品資訊
        /// </summary>
        public async Task<ProductInfo?> GetProductInfoAsync(string productCode)
        {
            var product = await _dbContext.Products
                .FirstOrDefaultAsync(p => p.ProductCode == productCode);

            if (product == null) return null;

            return new ProductInfo
            {
                ProductCode = product.ProductCode ?? string.Empty,
                ProductName = product.ProductName ?? string.Empty,
                Unit = product.Unit ?? string.Empty,
                StandardPrice = product.StandardPrice?.ToString() ?? "0",
                StandardCost = product.StandardCost?.ToString() ?? "0"
            };
        }

        /// <summary>
        /// 取得建議售價
        /// </summary>
        public async Task<string?> GetSuggestedPriceAsync(string standardPrice)
        {
            var suggestedPrice = await _dbContext.SuggestedPrices
                .FirstOrDefaultAsync(s => s.StandardPrice == standardPrice);

            return suggestedPrice?.SuggestedSalePrice?.ToString();
        }
    }
}
