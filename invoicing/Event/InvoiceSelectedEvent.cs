using invoicing.Models.DTO;

namespace invoicing.Event
{
    /// <summary>
    /// 發票選擇事件，用於表單間傳遞選中的發票資料
    /// </summary>
    /// <param name="SelectedInvoices">選中的訂單摘要清單</param>
    /// <param name="SelectedDetails">選中的訂單明細清單</param>
    /// <param name="CallerFormType">呼叫來源的表單類型名稱</param>
    public record InvoiceSelectedEvent(
        List<InvoicingDTO> SelectedInvoices,
        List<InvoicingDetailDTO> SelectedDetails,
        string CallerFormType);
}
