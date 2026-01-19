using invoicing.DB.DBContext;
using invoicing.Models.DTO;
using invoicing.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace invoicing.Service
{
    /// <summary>
    /// 財務報表服務實作
    /// </summary>
    public class FinancialService : IFinancialService
    {
        private readonly InvoicIngDbContext _context;

        public FinancialService(InvoicIngDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<List<FinancialSummaryDto>> GetGroupedTotalByDateRangeAsync(
            DateTime startDate, DateTime endDate, string[] orderTypes)
        {
            string startDateStr = startDate.ToString("yyyyMMdd");
            string endDateStr = endDate.ToString("yyyyMMdd");

            // 先從資料庫查詢原始資料
            var rawData = await _context.CustomerOrders
                .AsNoTracking()
                .Where(o => o.Deleted == "0"
                    && o.Date != null
                    && string.Compare(o.Date, startDateStr) >= 0
                    && string.Compare(o.Date, endDateStr) <= 0
                    && o.OrderName != null
                    && orderTypes.Contains(o.OrderName))
                .Select(o => new
                {
                    o.Customer,
                    o.OrderName,
                    o.TotalAmount
                })
                .ToListAsync();

            // 在客戶端進行分組和金額轉換
            var result = rawData
                .GroupBy(o => new { o.Customer, o.OrderName })
                .Select(g => new FinancialSummaryDto
                {
                    Customer = g.Key.Customer ?? string.Empty,
                    OrderName = g.Key.OrderName ?? string.Empty,
                    Amount = g.Sum(o => decimal.TryParse(o.TotalAmount, out decimal amt) ? amt : 0)
                })
                .OrderBy(x => x.Customer)
                .ToList();

            return result;
        }

        /// <inheritdoc/>
        public async Task<decimal> CalculateNetTotalAsync(
            DateTime startDate, DateTime endDate,
            string positiveOrderType, string negativeOrderType)
        {
            string startDateStr = startDate.ToString("yyyyMMdd");
            string endDateStr = endDate.ToString("yyyyMMdd");

            // 查詢正向單據金額
            var positiveAmounts = await _context.CustomerOrders
                .AsNoTracking()
                .Where(o => o.Deleted == "0"
                    && o.Date != null
                    && string.Compare(o.Date, startDateStr) >= 0
                    && string.Compare(o.Date, endDateStr) <= 0
                    && o.OrderName == positiveOrderType)
                .Select(o => o.TotalAmount)
                .ToListAsync();

            decimal positiveTotal = positiveAmounts
                .Sum(amt => decimal.TryParse(amt, out decimal val) ? val : 0);

            // 查詢負向單據金額
            var negativeAmounts = await _context.CustomerOrders
                .AsNoTracking()
                .Where(o => o.Deleted == "0"
                    && o.Date != null
                    && string.Compare(o.Date, startDateStr) >= 0
                    && string.Compare(o.Date, endDateStr) <= 0
                    && o.OrderName == negativeOrderType)
                .Select(o => o.TotalAmount)
                .ToListAsync();

            decimal negativeTotal = negativeAmounts
                .Sum(amt => decimal.TryParse(amt, out decimal val) ? val : 0);

            return positiveTotal - negativeTotal;
        }

        /// <inheritdoc/>
        public async Task<List<string>> GetDistinctCustomersAsync(
            DateTime startDate, DateTime endDate, string[] orderTypes)
        {
            string startDateStr = startDate.ToString("yyyyMMdd");
            string endDateStr = endDate.ToString("yyyyMMdd");

            var result = await _context.CustomerOrders
                .AsNoTracking()
                .Where(o => o.Deleted == "0"
                    && o.Date != null
                    && string.Compare(o.Date, startDateStr) >= 0
                    && string.Compare(o.Date, endDateStr) <= 0
                    && o.OrderName != null
                    && orderTypes.Contains(o.OrderName)
                    && o.Customer != null)
                .Select(o => o.Customer!)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();

            return result;
        }

        /// <inheritdoc/>
        public async Task<List<AccountsReceivableDto>> GetOrderDetailsByCustomerAsync(
            DateTime startDate, DateTime endDate,
            string customer, string[] orderTypes)
        {
            string startDateStr = startDate.ToString("yyyyMMdd");
            string endDateStr = endDate.ToString("yyyyMMdd");

            var orders = await _context.CustomerOrders
                .AsNoTracking()
                .Where(o => o.Deleted == "0"
                    && o.Date != null
                    && string.Compare(o.Date, startDateStr) >= 0
                    && string.Compare(o.Date, endDateStr) <= 0
                    && o.OrderName != null
                    && orderTypes.Contains(o.OrderName)
                    && o.Customer == customer)
                .OrderBy(o => o.OrderName)
                .ToListAsync();

            // 轉換為 DTO，並生成 OrderUid
            var result = orders.Select(o =>
            {
                // 優先使用 NewOrderNumber，若無則使用 OrderNumber
                string orderNumber = o.NewOrderNumber ?? o.OrderNumber ?? "0";
                if (int.TryParse(orderNumber, out int no))
                {
                    orderNumber = no.ToString("0000");
                }
                string orderUid = (o.Date ?? "") + orderNumber;

                return new AccountsReceivableDto
                {
                    OrderName = o.OrderName ?? string.Empty,
                    Date = o.Date ?? string.Empty,
                    OrderUid = orderUid,
                    TotalAmount = decimal.TryParse(o.TotalAmount, out decimal amt) ? amt : 0
                };
            }).ToList();

            return result;
        }
    }
}
