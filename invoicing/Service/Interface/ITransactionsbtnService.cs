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
    }
}
