using invoicing.Service.Interface;
using System.ComponentModel;

namespace invoicing.Financials
{
    /// <summary>
    /// 總進貨額表單
    /// </summary>
    public partial class TotalPurchasesForm : Form
    {
        private readonly IFinancialService _financialService;

        // 進貨相關單據類型
        private static readonly string[] PurchaseOrderTypes = { "進貨退出單", "進貨單" };
        private const string PositiveOrderType = "進貨單";
        private const string NegativeOrderType = "進貨退出單";

        public TotalPurchasesForm(IFinancialService financialService)
        {
            InitializeComponent();
            _financialService = financialService;
            InitializeEventHandlers();
        }

        /// <summary>
        /// 初始化事件處理器
        /// </summary>
        private void InitializeEventHandlers()
        {
            btnSearch.Click += BtnSearch_Click;
        }

        /// <summary>
        /// 查詢按鈕點擊事件
        /// </summary>
        private async void BtnSearch_Click(object? sender, EventArgs e)
        {
            try
            {
                // 清除現有資料
                dgvTotalPurchases.DataSource = null;
                lblTotalNumer.Text = "0";

                // 取得日期區間
                DateTime startDate = dtpStart.Value.Date;
                DateTime endDate = dtpEnd.Value.Date;

                // 查詢分組彙總資料
                var summaryData = await _financialService.GetGroupedTotalByDateRangeAsync(
                    startDate, endDate, PurchaseOrderTypes);

                if (summaryData.Count > 0)
                {
                    // 綁定到 DataGridView
                    dgvTotalPurchases.DataSource = new BindingList<Models.DTO.FinancialSummaryDto>(summaryData);

                    // 設定欄位標題為中文
                    if (dgvTotalPurchases.Columns["Customer"] != null)
                        dgvTotalPurchases.Columns["Customer"]!.HeaderText = "客戶";
                    if (dgvTotalPurchases.Columns["OrderName"] != null)
                        dgvTotalPurchases.Columns["OrderName"]!.HeaderText = "單子";
                    if (dgvTotalPurchases.Columns["Amount"] != null)
                        dgvTotalPurchases.Columns["Amount"]!.HeaderText = "金額";

                    dgvTotalPurchases.AutoResizeColumns();
                }

                // 計算淨總額（進貨單 - 進貨退出單）
                decimal netTotal = await _financialService.CalculateNetTotalAsync(
                    startDate, endDate, PositiveOrderType, NegativeOrderType);

                lblTotalNumer.Text = netTotal.ToString("#,##0.###");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查詢時發生錯誤：{ex.Message}", "錯誤",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
