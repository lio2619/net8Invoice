using invoicing.DB.DBContext;
using invoicing.Models.DTO;
using invoicing.Models.Entity;
using invoicing.ReadForm;
using invoicing.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace invoicing.Service
{
    /// <summary>
    /// 交易表單按鈕服務
    /// 提供多個交易表單共用的按鈕功能實作
    /// </summary>
    public class TransactionsbtnService : ITransactionsbtnService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly InvoicIngDbContext _dbContext;

        public TransactionsbtnService(
            IServiceProvider serviceProvider,
            InvoicIngDbContext dbContext)
        {
            _serviceProvider = serviceProvider;
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public void OpenReadInvoicesForm(string callerFormType, string callerOrderName = "")
        {
            try
            {
                // 使用 DI 取得 ReadInvoicesForm 實例
                var readForm = _serviceProvider.GetRequiredService<ReadInvoicesForm>();
                readForm.CallerFormType = callerFormType;
                readForm.CallerOrderName = callerOrderName;
                readForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"開啟讀檔視窗失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <inheritdoc/>
        public async Task<SaveResult> SaveTransactionAsync(SaveTransactionRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.CustomerName))
                {
                    return new SaveResult { Success = false, Message = "請選擇客戶" };
                }

                // 更新模式：優先使用 CustomerOrderId 查找，確保精確定位
                if (request.IsUpdate)
                {
                    CustomerOrder? existingOrder = null;
                    bool isNewOrderData = false;

                    // 優先使用 CustomerOrderId 查詢（最精確）
                    if (request.CustomerOrderId.HasValue && request.CustomerOrderId.Value > 0)
                    {
                        existingOrder = await _dbContext.CustomerOrders
                            .FirstOrDefaultAsync(x => x.Id == request.CustomerOrderId.Value);
                        if (existingOrder != null)
                        {
                            isNewOrderData = !string.IsNullOrEmpty(existingOrder.NewOrderNumber);
                        }
                    }
                    // 次優先：舊資料使用 OrderNumber 查找
                    else if (!string.IsNullOrEmpty(request.OrderNumber))
                    {
                        existingOrder = await _dbContext.CustomerOrders
                            .FirstOrDefaultAsync(x => x.OrderNumber == request.OrderNumber);
                        isNewOrderData = false;
                    }
                    // 再次優先：新資料使用 NewOrderNumber + OrderType + Date 組合查找
                    else if (!string.IsNullOrEmpty(request.NewOrderNumber))
                    {
                        existingOrder = await _dbContext.CustomerOrders
                            .FirstOrDefaultAsync(x => x.NewOrderNumber == request.NewOrderNumber
                                && x.OrderName == request.OrderType
                                && x.Date == request.Date);
                        isNewOrderData = true;
                    }

                    if (existingOrder == null)
                    {
                        return new SaveResult { Success = false, Message = "找不到對應的單子" };
                    }

                    // 刪除舊的明細（根據資料類型使用不同關聯）
                    List<OrderDetail> existingDetails;
                    if (isNewOrderData)
                    {
                        existingDetails = await _dbContext.OrderDetails
                            .Where(x => x.CustomerOrderId == existingOrder.Id)
                            .ToListAsync();
                    }
                    else
                    {
                        existingDetails = await _dbContext.OrderDetails
                            .Where(x => x.OrderNumber == existingOrder.OrderNumber)
                            .ToListAsync();
                    }

                    _dbContext.OrderDetails.RemoveRange(existingDetails);

                    await _dbContext.SaveChangesAsync();

                    // 更新主單資料
                    existingOrder.TotalAmount = request.TotalAmount;
                    existingOrder.Remark = request.Remark;
                    existingOrder.Customer = request.CustomerName;
                    existingOrder.Date = request.Date;
                    _dbContext.CustomerOrders.Update(existingOrder);

                    // 插入新的明細（根據資料類型使用不同關聯）
                    if (isNewOrderData)
                    {
                        await InsertOrderDetailsWithCustomerOrderIdAsync(existingOrder.Id, request.Details);
                    }
                    else
                    {
                        await InsertOrderDetailsAsync(existingOrder.OrderNumber!, request.Details);
                    }
                    await _dbContext.SaveChangesAsync();

                    return new SaveResult
                    {
                        Success = true,
                        Message = "儲存完成",
                        OrderNumber = existingOrder.OrderNumber,
                        NewOrderNumber = existingOrder.NewOrderNumber
                    };
                }
                else
                {
                    // 新增模式
                    // 計算新的 NewOrderNumber（每月20號重置）
                    string newOrderNumberStr = await GenerateNewOrderNumberAsync(request.Date, request.OrderType);

                    var newOrder = new CustomerOrder
                    {
                        // 新資料 OrderNumber 為 null
                        OrderNumber = null,
                        NewOrderNumber = newOrderNumberStr,
                        Date = request.Date,
                        Time = DateTime.Now.ToString("yyyyMMddHHmmss"),
                        Customer = request.CustomerName,
                        OrderName = request.OrderType,
                        Remark = request.Remark,
                        TotalAmount = request.TotalAmount,
                        Deleted = "0"
                    };

                    await _dbContext.CustomerOrders.AddAsync(newOrder);
                    await _dbContext.SaveChangesAsync();

                    // 使用 CustomerOrderId 關聯明細（新資料）
                    await InsertOrderDetailsWithCustomerOrderIdAsync(newOrder.Id, request.Details);
                    await _dbContext.SaveChangesAsync();

                    return new SaveResult
                    {
                        Success = true,
                        Message = "儲存完成",
                        OrderNumber = null,
                        NewOrderNumber = newOrderNumberStr
                    };
                }
            }
            catch (Exception ex)
            {
                return new SaveResult { Success = false, Message = $"儲存失敗：{ex.Message}" };
            }
        }

        /// 計算新的 NewOrderNumber（每月1號重置，4位數格式）
        /// </summary>
        /// <param name="dateStr">日期字串（yyyyMMdd 格式）</param>
        /// <param name="orderType">單子類型</param>
        /// <returns>4位數的新單子編號</returns>
        private async Task<string> GenerateNewOrderNumberAsync(string dateStr, string orderType)
        {
            // 解析日期
            if (!DateTime.TryParseExact(dateStr, "yyyyMMdd", null,
                System.Globalization.DateTimeStyles.None, out var orderDate))
            {
                orderDate = DateTime.Now;
            }

            // --- 修改開始：計算週期的起始日期（每月1號為分隔點） ---

            // 週期起始日：當月 1 號
            DateTime periodStart = new DateTime(orderDate.Year, orderDate.Month, 1);

            // 週期結束日：當月最後一天 (下個月1號 減去 1天)
            DateTime periodEnd = periodStart.AddMonths(1).AddDays(-1);

            // --- 修改結束 ---

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
        /// 插入訂單明細（舊資料用 OrderNumber 關聯）
        /// </summary>
        private async Task InsertOrderDetailsAsync(string orderNumber, List<InvoicingDetailDTO> details)
        {
            foreach (var detail in details)
            {
                if (string.IsNullOrEmpty(detail.ProductCode) && string.IsNullOrEmpty(detail.ProductName))
                    continue;

                var orderDetail = new OrderDetail
                {
                    OrderNumber = orderNumber,
                    CustomerOrderId = null,
                    ProductCode = detail.ProductCode ?? "",
                    ProductName = detail.ProductName ?? "",
                    Quantity = detail.Quantity ?? "",
                    Unit = detail.Unit ?? "",
                    UnitPrice = detail.UnitPrice ?? "",
                    Amount = detail.Amount ?? "",
                    Remark = detail.Remark ?? ""
                };

                await _dbContext.OrderDetails.AddAsync(orderDetail);
            }
        }

        /// <summary>
        /// 插入訂單明細（新資料用 CustomerOrderId 關聯）
        /// </summary>
        private async Task InsertOrderDetailsWithCustomerOrderIdAsync(int customerOrderId, List<InvoicingDetailDTO> details)
        {
            foreach (var detail in details)
            {
                if (string.IsNullOrEmpty(detail.ProductCode) && string.IsNullOrEmpty(detail.ProductName))
                    continue;

                var orderDetail = new OrderDetail
                {
                    OrderNumber = null,
                    CustomerOrderId = customerOrderId,
                    ProductCode = detail.ProductCode ?? "",
                    ProductName = detail.ProductName ?? "",
                    Quantity = detail.Quantity ?? "",
                    Unit = detail.Unit ?? "",
                    UnitPrice = detail.UnitPrice ?? "",
                    Amount = detail.Amount ?? "",
                    Remark = detail.Remark ?? ""
                };

                await _dbContext.OrderDetails.AddAsync(orderDetail);
            }
        }

        /// <inheritdoc/>
        public RefreshResult ConfirmRefresh()
        {
            var result = MessageBox.Show("確定刷新", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return new RefreshResult { Confirmed = result == DialogResult.Yes };
        }

        /// <inheritdoc/>
        public async Task<DeleteResult> DeleteTransactionAsync(int customerOrderId, string orderNumber, bool isNewOrderNumber, string orderType, string date)
        {
            try
            {
                var result = MessageBox.Show("確定刪除單子", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                {
                    return new DeleteResult { Success = false, Message = "已取消" };
                }

                // 優先使用 CustomerOrderId 查找主單（最精確）
                CustomerOrder? order = null;
                if (customerOrderId > 0)
                {
                    order = await _dbContext.CustomerOrders
                        .FirstOrDefaultAsync(x => x.Id == customerOrderId);
                }
                // 備用：根據編號類型 + 單子類型 + 日期查找主單（更精確）
                if (order == null)
                {
                    if (isNewOrderNumber)
                    {
                        order = await _dbContext.CustomerOrders
                            .FirstOrDefaultAsync(x => x.NewOrderNumber == orderNumber 
                                && x.OrderName == orderType 
                                && x.Date == date);
                    }
                    else
                    {
                        order = await _dbContext.CustomerOrders
                            .FirstOrDefaultAsync(x => x.OrderNumber == orderNumber 
                                && x.OrderName == orderType 
                                && x.Date == date);
                    }
                }

                if (order == null)
                {
                    return new DeleteResult { Success = false, Message = "找不到對應的單子" };
                }

                // 軟刪除主單
                order.TotalAmount = "0";
                order.Deleted = "1";
                _dbContext.CustomerOrders.Update(order);

                // 刪除明細（根據資料類型使用不同關聯）
                List<OrderDetail> details;
                if (!string.IsNullOrEmpty(order.NewOrderNumber))
                {
                    // 新資料：用 CustomerOrderId 查詢
                    details = await _dbContext.OrderDetails
                        .Where(x => x.CustomerOrderId == order.Id)
                        .ToListAsync();
                }
                else
                {
                    // 舊資料：用 OrderNumber 查詢
                    details = await _dbContext.OrderDetails
                        .Where(x => x.OrderNumber == order.OrderNumber)
                        .ToListAsync();
                }

                _dbContext.OrderDetails.RemoveRange(details);
                await _dbContext.SaveChangesAsync();

                return new DeleteResult { Success = true, Message = "刪除成功" };
            }
            catch (Exception ex)
            {
                return new DeleteResult { Success = false, Message = $"刪除失敗：{ex.Message}" };
            }
        }

        /// <inheritdoc/>
        public async Task CreateExcelAsync(CreateExcelRequest request)
        {
            await Task.Run(async () =>
            {
                try
                {
                    string dateFolder = request.Date.ToString("yyyyMM");
                    string path = $"../單子/{dateFolder}/{request.OrderType}/";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    // 取得客戶資訊
                    var customer = await _dbContext.Customers
                        .FirstOrDefaultAsync(x => x.CompanyFullName == request.CustomerName);

                    // 依據資料類型取得顯示用的編號
                    string displayOrderNumber;
                    if (request.IsUsingNewOrderNumber)
                    {
                        // 新資料：使用 NewOrderNumber（4位數）
                        displayOrderNumber = request.NewOrderNumber ?? "0001";
                    }
                    else if (!string.IsNullOrEmpty(request.OrderNumber))
                    {
                        // 舊資料：使用 OrderNumber
                        displayOrderNumber = request.OrderNumber;
                    }
                    else
                    {
                        // 沒有傳入編號，嘗試查詢
                        var order = await _dbContext.CustomerOrders
                            .FirstOrDefaultAsync(x =>
                                x.Date == request.Date.ToString("yyyyMMdd") &&
                                x.Customer == request.CustomerName &&
                                x.OrderName == request.OrderType);

                        if (order != null)
                        {
                            displayOrderNumber = !string.IsNullOrEmpty(order.NewOrderNumber)
                                ? order.NewOrderNumber
                                : order.OrderNumber;
                        }
                        else
                        {
                            displayOrderNumber = "0001";
                        }
                    }

                    // 使用顯示編號作為檔案名稱
                    string fileName = $"{request.OrderType}_{request.Date:yyyy-MM-dd}_{request.CustomerName}_{displayOrderNumber}.xlsx";
                    string filePath = Path.Combine(path, fileName);

                    // 檢查檔案是否存在
                    if (File.Exists(filePath))
                    {
                        MessageBox.Show("已經有這個檔案了", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 使用 NPOI 建立 Excel
                    IWorkbook workbook = new XSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("MySheet");

                    // 表頭
                    IRow row0 = sheet.CreateRow(0);
                    row0.CreateCell(0).SetCellValue(request.OrderType);
                    row0.CreateCell(6).SetCellValue($"貨單日期：{request.Date:yyyyMMdd}");

                    IRow row1 = sheet.CreateRow(1);
                    row1.CreateCell(0).SetCellValue("客戶名稱：");
                    row1.CreateCell(1).SetCellValue(request.CustomerName);
                    row1.CreateCell(6).SetCellValue($"貨單編號：{request.Date:yyyyMMdd}{displayOrderNumber}");

                    IRow row2 = sheet.CreateRow(2);
                    if (customer != null)
                    {
                        row2.CreateCell(0).SetCellValue($"連絡電話：{customer.Phone1}");
                        row2.CreateCell(3).SetCellValue($"傳真號碼：{customer.FaxNumber}");
                    }

                    IRow row3 = sheet.CreateRow(3);
                    row3.CreateCell(0).SetCellValue("編號");
                    row3.CreateCell(1).SetCellValue("品名");
                    row3.CreateCell(4).SetCellValue("數量");
                    row3.CreateCell(5).SetCellValue("單位");
                    row3.CreateCell(6).SetCellValue("單價");
                    row3.CreateCell(7).SetCellValue("金額");
                    row3.CreateCell(8).SetCellValue("備註");

                    // 明細資料
                    int rowIndex = 4;
                    foreach (var detail in request.Details)
                    {
                        IRow dataRow = sheet.CreateRow(rowIndex);
                        dataRow.CreateCell(0).SetCellValue(detail.ProductCode ?? "");
                        dataRow.CreateCell(1).SetCellValue(detail.ProductName ?? "");
                        dataRow.CreateCell(4).SetCellValue(detail.Quantity ?? "");
                        dataRow.CreateCell(5).SetCellValue(detail.Unit ?? "");
                        dataRow.CreateCell(6).SetCellValue(detail.UnitPrice ?? "");
                        dataRow.CreateCell(7).SetCellValue(detail.Amount ?? "");
                        dataRow.CreateCell(8).SetCellValue(detail.Remark ?? "");
                        rowIndex++;
                    }

                    IRow summaryRow = sheet.CreateRow(rowIndex);
                    summaryRow.CreateCell(0).SetCellValue($"備註：{request.Remark}");
                    summaryRow.CreateCell(7).SetCellValue($"總計：{request.TotalAmount}");

                    // 儲存檔案
                    using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        workbook.Write(fs);
                    }

                    MessageBox.Show("完成", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"建立 Excel 失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }
    }
}
