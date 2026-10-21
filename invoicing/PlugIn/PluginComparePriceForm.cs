using System.Collections.Generic;
using invoicing.Models.DTO;

namespace invoicing.PlugIn
{
    /// <summary>
    /// 提供給外掛 (金大/唐詣/金大新) Excel 成本比價彈出視窗使用的專屬 MDI 子表單類別。
    /// 【修改原因】：
    /// 原本外掛比價視窗直接實例化 ComparePriceForm，當作為 MDI 子視窗加入主工作區後，
    /// 其型別為 ComparePriceForm，導致 HomeScreenForm.OpenForm&lt;ComparePriceForm&gt;()
    /// 誤將外掛比價視窗識別為系統主選單的「比價 (PDF 比價)」視窗，僅對其執行 Focus()，
    /// 造成使用者在匯入 Excel 後再點選主選單「比價」時毫無反應，無法開啟原本的 PDF 比價表單。
    /// 建立專屬衍生類別 PluginComparePriceForm 進行型別隔離，使外掛比價與主選單比價完全獨立並存。
    /// </summary>
    public class PluginComparePriceForm : ComparePriceForm
    {
        public PluginComparePriceForm(List<CompareDisplayRow> rows, string title = "外掛 Excel 成本比價結果")
            : base(rows, title)
        {
        }
    }
}
