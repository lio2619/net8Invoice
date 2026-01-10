namespace invoicing.Models.DTO
{
    /// <summary>
    /// Excel 匯入設定
    /// </summary>
    public class ExcelImportConfig
    {
        /// <summary>
        /// 貨品編號欄位名稱
        /// </summary>
        public string ProductCodeColumn { get; set; } = "F1";

        /// <summary>
        /// 品名欄位名稱
        /// </summary>
        public string ProductNameColumn { get; set; } = "F3";

        /// <summary>
        /// 數量欄位名稱
        /// </summary>
        public string QuantityColumn { get; set; } = "F6";

        /// <summary>
        /// 單位欄位名稱
        /// </summary>
        public string UnitColumn { get; set; } = "F7";

        /// <summary>
        /// 單價欄位名稱（可選）
        /// </summary>
        public string? UnitPriceColumn { get; set; } = "F8";

        /// <summary>
        /// 工作表範圍（例如 "A16:K"）
        /// </summary>
        public string SheetRange { get; set; } = "A16:K";

        /// <summary>
        /// SQL 查詢的欄位清單（例如 "F1, F3, F6, F7, F8"）
        /// </summary>
        public string SelectColumns { get; set; } = "F1, F3, F6, F7, F8";

        /// <summary>
        /// 建立金大設定
        /// </summary>
        public static ExcelImportConfig JinDaConfig => new()
        {
            ProductCodeColumn = "F1",
            ProductNameColumn = "F3",
            QuantityColumn = "F6",
            UnitColumn = "F7",
            UnitPriceColumn = "F8",
            SheetRange = "A16:K",
            SelectColumns = "F1, F3, F6, F7, F8"
        };

        /// <summary>
        /// 建立金大新設定
        /// </summary>
        public static ExcelImportConfig JinDaNewConfig => new()
        {
            ProductCodeColumn = "F1",
            ProductNameColumn = "F2",
            QuantityColumn = "F3",
            UnitColumn = "F4",
            UnitPriceColumn = "F5",
            SheetRange = "A16:H",
            SelectColumns = "F1, F2, F3, F4, F5"
        };

        /// <summary>
        /// 建立唐詣設定
        /// </summary>
        public static ExcelImportConfig TangYiConfig => new()
        {
            ProductCodeColumn = "F1",
            ProductNameColumn = "F4",
            QuantityColumn = "F8",
            UnitColumn = "F9",
            UnitPriceColumn = "F10",
            SheetRange = "A12:L",
            SelectColumns = "F1, F4, F8, F9, F10"
        };
    }
}
