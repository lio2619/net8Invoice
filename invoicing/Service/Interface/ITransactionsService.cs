using System.ComponentModel;

namespace invoicing.Service.Interface
{
    /// <summary>
    /// 提供交易表單 DataGridView 相關的共用服務介面
    /// </summary>
    public interface ITransactionsService
    {
        #region DataGridView 使用功能
        /// <summary>
        /// 初始化 DataGridView，設定自動增行、刪除行及資料錯誤處理
        /// </summary>
        /// <typeparam name="T">資料類型</typeparam>
        /// <param name="dgv">要初始化的 DataGridView</param>
        /// <param name="dataSource">BindingList 資料來源</param>
        void InitializeDataGridView<T>(DataGridView dgv, BindingList<T> dataSource) where T : new();

        /// <summary>
        /// 初始化 DataGridView，設定自動增行及資料錯誤處理（可自訂刪除權限）
        /// </summary>
        /// <typeparam name="T">資料類型</typeparam>
        /// <param name="dgv">要初始化的 DataGridView</param>
        /// <param name="dataSource">BindingList 資料來源</param>
        /// <param name="allowDelete">是否允許使用者刪除行</param>
        void InitializeDataGridView<T>(DataGridView dgv, BindingList<T> dataSource, bool allowDelete) where T : new();

        /// <summary>
        /// 處理 DataGridView 的 RowPostPaint 事件，在行標題中顯示行號
        /// </summary>
        /// <param name="sender">事件來源的 DataGridView</param>
        /// <param name="e">RowPostPaint 事件參數</param>
        void HandleRowPostPaint(object? sender, DataGridViewRowPostPaintEventArgs e);

        /// <summary>
        /// 處理 DataGridView 的右鍵刪除功能
        /// </summary>
        /// <param name="sender">事件來源的 DataGridView</param>
        /// <param name="e">滑鼠事件參數</param>
        /// <returns>刪除操作執行結果：true 表示已刪除，false 表示取消或無法刪除</returns>
        bool HandleRightClickDelete(object? sender, MouseEventArgs e);

        /// <summary>
        /// 處理 Enter 鍵轉換為 Tab 鍵的行為（用於 ProcessCmdKey）
        /// </summary>
        /// <param name="keyData">按下的按鍵</param>
        /// <returns>true 表示已處理 Enter 鍵，false 表示未處理</returns>
        bool HandleEnterAsTab(Keys keyData);
        #endregion

        #region 產品編號自動完成相關功能
        /// <summary>
        /// 設定 DataGridView 欄位的產品編號自動完成功能（顯示產品編號與名稱）
        /// </summary>
        /// <param name="dgv">要設定的 DataGridView</param>
        /// <param name="productColumnHeaderText">產品編號欄位的標題文字</param>
        /// <param name="cancellationToken">用於取消非同步操作的 Token</param>
        void SetupProductCodeAutoComplete(DataGridView dgv, string productColumnHeaderText, CancellationToken cancellationToken);

        /// <summary>
        /// 處理產品編號欄位的 TextChanged 事件，包含防抖動機制
        /// </summary>
        /// <param name="textBox">觸發事件的 TextBox</param>
        /// <param name="cancellationToken">用於取消非同步操作的 Token</param>
        Task HandleProductCodeTextChangedAsync(TextBox textBox, CancellationToken cancellationToken);

        /// <summary>
        /// 根據產品編號取得產品資訊並填入 DataGridView 行
        /// </summary>
        /// <param name="row">要填入資料的 DataGridView 行</param>
        /// <param name="productCode">產品編號</param>
        /// <returns>若找到產品回傳 true，否則回傳 false</returns>
        Task<bool> FetchProductInfoAsync(DataGridViewRow row, string productCode);

        /// <summary>
        /// 計算單行金額（數量 × 單價）
        /// </summary>
        /// <param name="row">要計算的 DataGridView 行</param>
        void CalculateRowAmount(DataGridViewRow row);

        /// <summary>
        /// 計算所有行的總金額
        /// </summary>
        /// <param name="dgv">要計算的 DataGridView</param>
        /// <returns>總金額</returns>
        double CalculateTotalAmount(DataGridView dgv);
        #endregion

        #region 表單事件處理
        /// <summary>
        /// 處理 DataGridView 的 CellEndEdit 事件
        /// </summary>
        /// <param name="dgv">DataGridView 控制項</param>
        /// <param name="e">儲存格事件參數</param>
        /// <param name="productColumnHeaderText">產品編號欄位的標題文字</param>
        /// <param name="quantityColumnHeaderText">數量欄位的標題文字</param>
        /// <param name="priceColumnHeaderText">單價欄位的標題文字</param>
        /// <param name="onTotalAmountChanged">總金額變更時的回呼 Action</param>
        /// <param name="cancellationToken">用於取消非同步操作的 Token</param>
        Task HandleCellEndEditAsync(
            DataGridView dgv,
            DataGridViewCellEventArgs e,
            string productColumnHeaderText,
            string quantityColumnHeaderText,
            string priceColumnHeaderText,
            Action<double> onTotalAmountChanged,
            CancellationToken cancellationToken);

        /// <summary>
        /// 處理 DataGridView 的 RowsRemoved 事件
        /// </summary>
        /// <param name="dgv">DataGridView 控制項</param>
        /// <param name="onTotalAmountChanged">總金額變更時的回呼 Action</param>
        void HandleRowsRemoved(DataGridView dgv, Action<double> onTotalAmountChanged);

        /// <summary>
        /// 處理建議清單的鍵盤操作（用於 Form.ProcessCmdKey）
        /// 當建議清單可見時，處理上/下/Enter/Escape 鍵
        /// </summary>
        /// <param name="keyData">按下的按鍵</param>
        /// <returns>true 表示已處理按鍵，false 表示未處理</returns>
        bool HandleSuggestionKeyPress(Keys keyData);
        #endregion
    }
}
