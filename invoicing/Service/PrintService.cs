using invoicing.Service.Interface;
using System.Configuration;
using System.Drawing.Printing;

namespace invoicing.Service
{
    public class PrintService : IPrintService
    {
        private readonly int[] _width = [20, 135, 520, 575, 625, 680, 750];
        private readonly string[] _vendorDocs = ["進貨退出單", "進貨單"];
        private readonly string _defaultCustomerName = ConfigurationManager.AppSettings["DefaultCustomerName"];
        private readonly string _defaultCustomerTel = ConfigurationManager.AppSettings["DefaultCustomerTel"];
        private readonly string _defaultCustomerFax = ConfigurationManager.AppSettings["DefaultCustomerFax"];

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
                                int page, int totalPage)
        {
            if (_vendorDocs.Contains(bili)) // 給廠商
            {
                DrawHeaderBase(e, bili, client, date, singleNumber, tex, fax, address, page, totalPage, "廠商名稱");
                DrawTableHeader(e, bili == "採購單" ? "採購單" : "廠商");
            }
            else if (bili == "採購單")
            {
                DrawHeaderBase(e, bili, client, date, singleNumber, tex, fax, address, page, totalPage, "廠商名稱");
                DrawTableHeader(e, "採購單");
            }
            else // 給客戶
            {
                DrawHeaderBase(e, bili, client, date, singleNumber, tex, fax, address, page, totalPage, "客戶名稱");
                DrawTableHeader(e, "客戶");
            }
        }

        /// <summary>
        /// 列印表格內的內容 (逐列印出數值)
        /// </summary>
        /// <param name="e">列印事件參數 (提供繪製功能)</param>
        /// <param name="value">要列印的文字內容</param>
        /// <param name="high">列印的垂直位置 (Y 座標)</param>
        /// <param name="j">目前欄位索引 (決定水平位置)</param>
        /// <param name="row">目前列索引</param>
        public void PrintInside(PrintPageEventArgs e, string value, int high, int j, int row)
        {
            if (row % 5 == 0 && row != 0 && row % 40 != 0)
            {
                using var blackPen = new Pen(Color.Black);
                e.Graphics.DrawLine(blackPen, new PointF(20, high), new PointF(790, high));
            }

            using var font = new Font("Arial", 10);
            using var stringFormat = new StringFormat { Alignment = StringAlignment.Far };

            if (j > 1)
            {
                e.Graphics.DrawString(value, font, Brushes.Black, new PointF(_width[j], high), stringFormat);
            }
            else
            {
                e.Graphics.DrawString(value, font, Brushes.Black, new PointF(_width[j], high));
            }
        }

        /// <summary>
        /// 列印表尾 (總計與備註)
        /// </summary>
        /// <param name="e">列印事件參數 (提供繪製功能)</param>
        /// <param name="value">總計數值</param>
        /// <param name="direction">備註文字</param>
        /// <param name="row">目前列索引</param>
        /// <param name="isLastPage">是否為最後一頁</param>
        public void PrintLast(PrintPageEventArgs e, string value, string direction, int row, bool isLastPage)
        {
            using var normalFont = new Font("Arial", 12);
            using var blackPen = new Pen(Color.Black);

            if (isLastPage)
            {
                e.Graphics.DrawLine(blackPen, new PointF(20, 1010), new PointF(790, 1010));
                e.Graphics.DrawString($"備註：{direction}", normalFont, Brushes.Black, new PointF(20, 1020));
                e.Graphics.DrawString($"總計：{value}", normalFont, Brushes.Black, new PointF(640, 1020));
            }
            else if (row % 40 < 16 && row % 40 != 0)
            {
                e.Graphics.DrawLine(blackPen, new PointF(20, 500), new PointF(790, 500));
                e.Graphics.DrawString($"備註：{direction}", normalFont, Brushes.Black, new PointF(20, 510));
                e.Graphics.DrawString($"總計：{value}", normalFont, Brushes.Black, new PointF(640, 510));
            }
            else
            {
                e.Graphics.DrawLine(blackPen, new PointF(20, 1010), new PointF(790, 1010));
                e.Graphics.DrawString($"備註：{direction}", normalFont, Brushes.Black, new PointF(20, 1020));
                e.Graphics.DrawString($"總計：{value}", normalFont, Brushes.Black, new PointF(640, 1020));
            }
        }

        /// <summary>
        /// 列印「應收帳款簡要表」表頭
        /// </summary>
        /// <param name="e">列印事件參數</param>
        /// <param name="startDate">帳款區間起始日</param>
        /// <param name="endDate">帳款區間結束日</param>
        /// <param name="company">客戶名稱</param>
        public void CollectMoneyHeader(PrintPageEventArgs e, string startDate, string endDate, string company)
        {
            using var titleFont = new Font("Arial", 20);
            using var subTitleFont = new Font("Arial", 16);
            using var normalFont = new Font("Arial", 12);
            using var blackPen = new Pen(Color.Black);

            e.Graphics.DrawString(_defaultCustomerName, titleFont, Brushes.Black, new PointF(320, 20));
            e.Graphics.DrawString("應收帳款簡要表", subTitleFont, Brushes.Black, new PointF(325, 60));
            e.Graphics.DrawString($"帳款區間：{startDate} ~ {endDate}", normalFont, Brushes.Black, new PointF(20, 80));
            e.Graphics.DrawString($"客戶名稱：{company}", normalFont, Brushes.Black, new PointF(20, 110));

            e.Graphics.DrawString("單別", normalFont, Brushes.Black, new PointF(70, 140));
            e.Graphics.DrawString("交易日期", normalFont, Brushes.Black, new PointF(140, 140));
            e.Graphics.DrawString("交易單號", normalFont, Brushes.Black, new PointF(260, 140));
            e.Graphics.DrawString("合計金額", normalFont, Brushes.Black, new PointF(550, 140));
            e.Graphics.DrawString("總計金額", normalFont, Brushes.Black, new PointF(650, 140));

            e.Graphics.DrawLine(blackPen, new PointF(20, 160), new PointF(790, 160));
        }

        /// <summary>
        /// 列印「應收帳款簡要表」內部的資料列
        /// </summary>
        /// <param name="e">列印事件參數</param>
        /// <param name="value">要列印的文字內容</param>
        /// <param name="high">列印的垂直位置 (Y 座標)</param>
        /// <param name="j">目前欄位索引</param>
        public void CollectMoneyInside(PrintPageEventArgs e, string value, int high, int j)
        {
            using var font = new Font("Arial", 12);
            using var stringFormat = new StringFormat { Alignment = StringAlignment.Far };

            int[] mWidth = { 120, 220, 365, 620, 720 };
            e.Graphics.DrawString(value, font, Brushes.Black, new PointF(mWidth[j], high), stringFormat);
        }

        /// <summary>
        /// 列印「應收帳款簡要表」的表尾 (畫橫線)
        /// </summary>
        /// <param name="e">列印事件參數</param>
        /// <param name="row">目前列索引</param>
        public void CollectMoneyLast(PrintPageEventArgs e, int row)
        {
            using var blackPen = new Pen(Color.Black);

            if (row % 41 < 16)
            {
                e.Graphics.DrawLine(blackPen, new PointF(20, 500), new PointF(790, 500));
            }
            else
            {
                e.Graphics.DrawLine(blackPen, new PointF(20, 1010), new PointF(790, 1010));
            }
        }

        /// <summary>
        /// 畫表頭的基本資訊 (單據名稱、頁次、公司資訊等)
        /// </summary>
        /// <param name="e">列印事件參數</param>
        /// <param name="bili">單據名稱</param>
        /// <param name="client">客戶/廠商名稱</param>
        /// <param name="date">單據日期</param>
        /// <param name="singleNumber">單據編號</param>
        /// <param name="tex">電話號碼</param>
        /// <param name="fax">傳真號碼</param>
        /// <param name="address">送貨地址</param>
        /// <param name="page">目前頁碼</param>
        /// <param name="totalPage">總頁數</param>
        /// <param name="clientLabel">顯示標籤 (客戶名稱/廠商名稱)</param>
        private void DrawHeaderBase(PrintPageEventArgs e, string bili, string client, string date,
                                    string singleNumber, string tex, string fax, string address,
                                    int page, int totalPage, string clientLabel)
        {
            using var titleFont = new Font("Arial", 22);
            using var normalFont = new Font("Arial", 12);

            e.Graphics.DrawString(_defaultCustomerName, titleFont, Brushes.Black, new PointF(20, 20));
            e.Graphics.DrawString(bili, titleFont, Brushes.Black, new PointF(560, 20));
            e.Graphics.DrawString($"Tel：{_defaultCustomerTel}", normalFont, Brushes.Black, new PointF(20, 60));
            e.Graphics.DrawString($"Fax：{_defaultCustomerFax}", normalFont, Brushes.Black, new PointF(250, 60));
            e.Graphics.DrawString($"頁次：{page + 1}/{totalPage + 1}", normalFont, Brushes.Black, new PointF(560, 60));
            e.Graphics.DrawString($"{clientLabel}：{client}", normalFont, Brushes.Black, new PointF(20, 90));
            e.Graphics.DrawString($"貨單日期：{date}", normalFont, Brushes.Black, new PointF(560, 90));
            e.Graphics.DrawString($"連絡電話：{tex}", normalFont, Brushes.Black, new PointF(20, 120));
            e.Graphics.DrawString($"傳真號碼：{fax}", normalFont, Brushes.Black, new PointF(250, 120));
            e.Graphics.DrawString($"貨單編號：{singleNumber}", normalFont, Brushes.Black, new PointF(560, 120));
            e.Graphics.DrawString($"送貨地址：{address}", normalFont, Brushes.Black, new PointF(20, 150));
        }

        /// <summary>
        /// 畫表格的欄位標題 (品名、數量、單位...等)
        /// </summary>
        /// <param name="e">列印事件參數</param>
        /// <param name="type">單據類型 (決定顯示的欄位)</param>
        private void DrawTableHeader(PrintPageEventArgs e, string type)
        {
            using var normalFont = new Font("Arial", 12);
            using var blackPen = new Pen(Color.Black);

            var lineY = 170;
            e.Graphics.DrawLine(blackPen, new PointF(20, lineY), new PointF(790, lineY));

            e.Graphics.DrawString("編號", normalFont, Brushes.Black, new PointF(20, lineY));
            e.Graphics.DrawString("品名", normalFont, Brushes.Black, new PointF(140, lineY));
            e.Graphics.DrawString("數量", normalFont, Brushes.Black, new PointF(490, lineY));
            e.Graphics.DrawString("單位", normalFont, Brushes.Black, new PointF(540, lineY));

            if (type == "採購單")
            {
                e.Graphics.DrawString("備註", normalFont, Brushes.Black, new PointF(640, lineY));
            }
            else
            {
                e.Graphics.DrawString("單價", normalFont, Brushes.Black, new PointF(590, lineY));
                e.Graphics.DrawString("金額", normalFont, Brushes.Black, new PointF(640, lineY));
                e.Graphics.DrawString("建議售價", normalFont, Brushes.Black, new PointF(700, lineY));
            }

            e.Graphics.DrawLine(blackPen, new PointF(20, 190), new PointF(790, 190));
        }
    }
}
