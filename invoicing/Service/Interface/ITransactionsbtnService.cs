using invoicing.Models.DTO;

namespace invoicing.Service.Interface
{
    /// <summary>
    /// 交易表單按鈕服務介面
    /// 提供多個交易表單共用的按鈕功能
    /// </summary>
    public interface ITransactionsbtnService
    {
        /// <summary>
        /// 開啟讀取單子視窗
        /// </summary>
        /// <param name="callerFormType">呼叫來源的表單類型名稱</param>
        /// <param name="callerOrderName">用於篩選的單子名稱（可為空）</param>
        void OpenReadInvoicesForm(string callerFormType, string callerOrderName = "");

        /// <summary>
        /// 儲存交易單子（新增或更新）
        /// </summary>
        /// <param name="request">儲存請求參數</param>
        /// <returns>儲存結果</returns>
        Task<SaveResult> SaveTransactionAsync(SaveTransactionRequest request);

        /// <summary>
        /// 刷新表單確認對話框
        /// </summary>
        /// <returns>刷新結果（是否確認刷新）</returns>
        RefreshResult ConfirmRefresh();

        /// <summary>
        /// 刪除交易單子
        /// </summary>
        /// <param name="customerOrderId">CustomerOrder 的 Id（優先使用）</param>
        /// <param name="orderNumber">單子編號（備用）</param>
        /// <param name="isNewOrderNumber">是否為新編號系統（NewOrderNumber）</param>
        /// <param name="orderType">單子類型（備用查詢時使用）</param>
        /// <param name="date">單子日期（備用查詢時使用，格式 yyyyMMdd）</param>
        /// <returns>刪除結果</returns>
        Task<DeleteResult> DeleteTransactionAsync(int customerOrderId, string orderNumber, bool isNewOrderNumber, string orderType, string date);

        /// <summary>
        /// 建立 Excel 檔案
        /// </summary>
        /// <param name="request">建立 Excel 請求參數</param>
        /// <returns>非同步任務</returns>
        Task CreateExcelAsync(CreateExcelRequest request);
    }
}
