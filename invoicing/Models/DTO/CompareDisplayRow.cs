using System.Drawing;

namespace invoicing.Models.DTO
{
    /// <summary>
    /// 比價顯示資料列 DTO
    /// 【修改原因】：將原本僅存於 ComparePriceForm 內的私有 CompareDisplayRow 抽離並升級為共用 DTO，
    /// 供原本的 PDF 比價（比對售價A）與新增加的外掛 Excel 匯入比價（金大/唐詣/金大新比對現行成本）共同使用。
    /// </summary>
    public class CompareDisplayRow
    {
        /// <summary>
        /// 貨品編號或國碼
        /// </summary>
        public string Barcode { get; set; } = string.Empty;

        /// <summary>
        /// 品名
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// 單子售價 / 匯入單價（若為唐詣則為乘上 1.05 後之折算單價）
        /// </summary>
        public string InvoicePrice { get; set; } = string.Empty;

        /// <summary>
        /// 原始 Excel 單價（未折算前，供參考與提示）
        /// </summary>
        public string? OriginalPrice { get; set; }

        /// <summary>
        /// 售價A（PDF 比價使用）
        /// </summary>
        public string? PriceA { get; set; }

        /// <summary>
        /// 現行成本（Excel 比價使用）
        /// </summary>
        public string? StandardCost { get; set; }

        /// <summary>
        /// 顯示顏色（例如紅色代表虧損、橘黃色代表成本未設定或條碼異常）
        /// </summary>
        public Color DisplayColor { get; set; } = Color.Black;

        /// <summary>
        /// 狀態備註（例如：虧本、成本未設定/0）
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 是否為虧本商品（單價低於現行成本）
        /// </summary>
        public bool IsBelowCost { get; set; }

        /// <summary>
        /// 是否為成本未設定商品
        /// </summary>
        public bool IsCostNotSet { get; set; }
    }
}
