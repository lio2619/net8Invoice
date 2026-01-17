using invoicing.Models.DTO;

namespace invoicing.Service.Interface
{
    public interface IPrintService
    {
        #region QuestPDF + PdfiumViewer 列印功能

        /// <summary>
        /// 使用 QuestPDF 產生發票 PDF
        /// </summary>
        /// <param name="request">列印請求資料</param>
        /// <returns>PDF 檔案的 byte 陣列</returns>
        byte[] GenerateInvoicePdf(PrintInvoiceRequest request);

        /// <summary>
        /// 使用 PdfiumViewer 顯示 PDF 預覽視窗並提供列印功能
        /// </summary>
        /// <param name="pdfBytes">PDF 檔案的 byte 陣列</param>
        /// <param name="copies">列印份數</param>
        void ShowPrintPreviewAndPrint(byte[] pdfBytes, int copies = 1);

        /// <summary>
        /// 產生應收帳款簡要表 PDF
        /// </summary>
        /// <param name="request">應收帳款列印請求資料</param>
        /// <returns>PDF 檔案的 byte 陣列</returns>
        byte[] GenerateAccountsReceivablePdf(AccountsReceivablePrintRequest request);

        #endregion
    }
}
