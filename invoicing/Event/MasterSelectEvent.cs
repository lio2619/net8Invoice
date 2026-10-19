// 客戶、廠商、產品 單筆與全部的對應事件
// 【修改原因】：原本單一 MasterSelectEvent 被客戶、產品、廠商及交易單據全部共用，
// 導致出貨單等表單選取貨品編號時誤觸客戶管理表單，引起「找不到此客戶」之錯誤。
// 依據領域驅動與責任分離原則，將事件拆分為專屬的強型別事件，並加入 Sender 參數支援雙向連動時防止無窮迴圈。

namespace invoicing.Event
{
    /// <summary>
    /// 客戶選取與切換事件（用於客戶資料清單與客戶資料管理之雙向連動）
    /// </summary>
    /// <param name="FullyName">客戶公司全名</param>
    /// <param name="Sender">事件發送來源物件（避免自身發送又自身重複處理）</param>
    public record CustomerSelectEvent(string FullyName, object? Sender = null);

    /// <summary>
    /// 產品選取與切換事件（用於產品資料、產品管理資料、出貨/進貨單之商品連動）
    /// </summary>
    /// <param name="ProductCode">貨品編號</param>
    /// <param name="Sender">事件發送來源物件</param>
    public record ProductSelectEvent(string ProductCode, object? Sender = null);

    /// <summary>
    /// 廠商選取與切換事件（用於廠商資料清單與廠商資料管理之雙向連動）
    /// </summary>
    /// <param name="FullyName">廠商名稱</param>
    /// <param name="Sender">事件發送來源物件</param>
    public record SupplierSelectEvent(string FullyName, object? Sender = null);

    /// <summary>
    /// 舊有相容事件（建議優先使用上述強型別領域事件）
    /// </summary>
    [Obsolete("請依領域改用 CustomerSelectEvent, ProductSelectEvent 或 SupplierSelectEvent")]
    public record MasterSelectEvent(string FullyName);
}
