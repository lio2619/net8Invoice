using System.Drawing.Printing;

namespace invoicing.Service.Interface
{
    public interface IPrintService
    {
        /// <summary>
        /// 列印表頭 (依據單據類型：客戶/廠商/採購單)
        /// </summary>
        /// <param name="e">列印事件參數 (提供繪製功能)</param>
        /// <param name="bili">單據名稱 (例如：進貨單、出貨單)</param>
        /// <param name="client">客戶或廠商名稱</param>
        /// <param name="date">單據日期</param>
        /// <param name="singleNumber">單據編號</param>
        /// <param name="tex">電話號碼</param>
        /// <param name="fax">傳真號碼</param>
        /// <param name="address">送貨地址</param>
        /// <param name="page">目前頁碼</param>
        /// <param name="totalPage">總頁數</param>
        public void PrintHeader(PrintPageEventArgs e, string bili, string client, string date,
                                string singleNumber, string tex, string fax, string address,
                                int page, int totalPage);

        /// <summary>
        /// 列印表格內的內容 (逐列印出數值)
        /// </summary>
        /// <param name="e">列印事件參數 (提供繪製功能)</param>
        /// <param name="value">要列印的文字內容</param>
        /// <param name="high">列印的垂直位置 (Y 座標)</param>
        /// <param name="j">目前欄位索引 (決定水平位置)</param>
        /// <param name="row">目前列索引</param>
        public void PrintInside(PrintPageEventArgs e, string value, int high, int j, int row);

        /// <summary>
        /// 列印表尾 (總計與備註)
        /// </summary>
        /// <param name="e">列印事件參數 (提供繪製功能)</param>
        /// <param name="value">總計數值</param>
        /// <param name="direction">備註文字</param>
        /// <param name="row">目前列索引</param>
        /// <param name="isLastPage">是否為最後一頁</param>
        public void PrintLast(PrintPageEventArgs e, string value, string direction, int row, bool isLastPage);

        /// <summary>
        /// 列印「應收帳款簡要表」表頭
        /// </summary>
        /// <param name="e">列印事件參數</param>
        /// <param name="startDate">帳款區間起始日</param>
        /// <param name="endDate">帳款區間結束日</param>
        /// <param name="company">客戶名稱</param>
        public void CollectMoneyHeader(PrintPageEventArgs e, string startDate, string endDate, string company);

        /// <summary>
        /// 列印「應收帳款簡要表」內部的資料列
        /// </summary>
        /// <param name="e">列印事件參數</param>
        /// <param name="value">要列印的文字內容</param>
        /// <param name="high">列印的垂直位置 (Y 座標)</param>
        /// <param name="j">目前欄位索引</param>
        public void CollectMoneyInside(PrintPageEventArgs e, string value, int high, int j);

        /// <summary>
        /// 列印「應收帳款簡要表」的表尾 (畫橫線)
        /// </summary>
        /// <param name="e">列印事件參數</param>
        /// <param name="row">目前列索引</param>
        public void CollectMoneyLast(PrintPageEventArgs e, int row);
    }
}
