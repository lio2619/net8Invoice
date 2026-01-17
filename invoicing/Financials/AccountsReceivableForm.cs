using invoicing.Models.DTO;
using invoicing.Service.Interface;
using System.ComponentModel;

namespace invoicing.Financials
{
    /// <summary>
    /// 應收帳款表單
    /// </summary>
    public partial class AccountsReceivableForm : Form
    {
        private readonly IFormUIService _formUIService;
        private readonly IFinancialService _financialService;
        private readonly IPrintService _printService;

        // 出貨相關單據類型
        private static readonly string[] SalesOrderTypes = { "出貨退出單", "出貨單" };
        private const string PositiveOrderType = "出貨單";
        private const string NegativeOrderType = "出貨退出單";

        // 當前查詢結果（用於列印）
        private List<AccountsReceivableDto> _currentDetails = new();
        private decimal _currentTotal = 0;

        public AccountsReceivableForm(
            IFormUIService formUIService,
            IFinancialService financialService,
            IPrintService printService)
        {
            InitializeComponent();
            _formUIService = formUIService;
            _financialService = financialService;
            _printService = printService;
            InitializeEventHandlers();
        }

        /// <summary>
        /// 初始化事件處理器
        /// </summary>
        private void InitializeEventHandlers()
        {
            _formUIService.AddTextBoxUnderline(txtTax);
            dtpStart.ValueChanged += DateTimePicker_ValueChanged;
            dtpEnd.ValueChanged += DateTimePicker_ValueChanged;
            cboCustomer.TextChanged += CboCustomer_TextChanged;
            btnPrint.Click += BtnPrint_Click;
        }

        /// <summary>
        /// 日期區間變更時更新客戶下拉選單
        /// </summary>
        private async void DateTimePicker_ValueChanged(object? sender, EventArgs e)
        {
            try
            {
                DateTime startDate = dtpStart.Value.Date;
                DateTime endDate = dtpEnd.Value.Date;

                // 查詢有資料的客戶清單
                var customers = await _financialService.GetDistinctCustomersAsync(
                    startDate, endDate, SalesOrderTypes);

                // 更新下拉選單
                cboCustomer.Items.Clear();
                foreach (var customer in customers)
                {
                    cboCustomer.Items.Add(customer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"載入客戶清單時發生錯誤：{ex.Message}", "錯誤",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 客戶選擇變更時查詢明細
        /// </summary>
        private async void CboCustomer_TextChanged(object? sender, EventArgs e)
        {
            try
            {
                // 重置
                txtTax.Text = "";
                _currentTotal = 0;
                _currentDetails.Clear();

                if (string.IsNullOrWhiteSpace(cboCustomer.Text))
                {
                    dgvAccountsReceivable.DataSource = null;
                    lblNTotalumber.Text = "0";
                    return;
                }

                DateTime startDate = dtpStart.Value.Date;
                DateTime endDate = dtpEnd.Value.Date;
                string customer = cboCustomer.Text;

                // 查詢單據明細
                _currentDetails = await _financialService.GetOrderDetailsByCustomerAsync(
                    startDate, endDate, customer, SalesOrderTypes);

                if (_currentDetails.Count > 0)
                {
                    // 綁定到 DataGridView
                    dgvAccountsReceivable.DataSource = new BindingList<AccountsReceivableDto>(_currentDetails);

                    // 設定欄位標題為中文
                    if (dgvAccountsReceivable.Columns["OrderName"] != null)
                        dgvAccountsReceivable.Columns["OrderName"]!.HeaderText = "單子";
                    if (dgvAccountsReceivable.Columns["Date"] != null)
                        dgvAccountsReceivable.Columns["Date"]!.HeaderText = "日期";
                    if (dgvAccountsReceivable.Columns["OrderUid"] != null)
                        dgvAccountsReceivable.Columns["OrderUid"]!.HeaderText = "單子編號";
                    if (dgvAccountsReceivable.Columns["TotalAmount"] != null)
                        dgvAccountsReceivable.Columns["TotalAmount"]!.HeaderText = "總金額";

                    dgvAccountsReceivable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
                else
                {
                    dgvAccountsReceivable.DataSource = null;
                }

                // 計算本期合計（出貨單 - 出貨退出單）
                _currentTotal = await _financialService.CalculateNetTotalAsync(
                    startDate, endDate, PositiveOrderType, NegativeOrderType);

                // 因客戶篩選，需重新計算
                decimal positiveSum = _currentDetails
                    .Where(d => d.OrderName == PositiveOrderType)
                    .Sum(d => d.TotalAmount);
                decimal negativeSum = _currentDetails
                    .Where(d => d.OrderName == NegativeOrderType)
                    .Sum(d => d.TotalAmount);
                _currentTotal = positiveSum - negativeSum;

                lblNTotalumber.Text = _currentTotal.ToString("#,##0.###");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查詢明細時發生錯誤：{ex.Message}", "錯誤",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 列印按鈕點擊事件
        /// </summary>
        private void BtnPrint_Click(object? sender, EventArgs e)
        {
            try
            {
                // 驗證輸入
                if (string.IsNullOrWhiteSpace(cboCustomer.Text))
                {
                    MessageBox.Show("請選擇客戶", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTax.Text))
                {
                    MessageBox.Show("請輸入營業稅", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtTax.Text.Trim(), out decimal tax))
                {
                    MessageBox.Show("營業稅請輸入數值", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 計算本期總計（本期合計 + 營業稅）
                decimal totalWithTax = _currentTotal + tax;

                // 建立列印請求
                var details = _currentDetails.Select(d => new InvoicingDetailDTO
                {
                    ProductCode = d.OrderName,
                    ProductName = d.Date,
                    Quantity = d.OrderUid,
                    UnitPrice = d.OrderName == NegativeOrderType
                        ? $"- {d.TotalAmount:N0}"
                        : d.TotalAmount.ToString("N0"),
                    Amount = d.OrderName == NegativeOrderType
                        ? $"- {d.TotalAmount:N0}"
                        : d.TotalAmount.ToString("N0")
                }).ToList();

                var request = new PrintInvoiceRequest
                {
                    OrderType = "應收帳款",
                    CustomerName = cboCustomer.Text,
                    Date = $"{dtpStart.Value:yyyyMMdd} - {dtpEnd.Value:yyyyMMdd}",
                    TotalAmount = totalWithTax.ToString("N0"),
                    Remark = $"本期合計：{_currentTotal:N0}  營業稅：{tax:N0}  本期總計：{totalWithTax:N0}",
                    Details = details
                };

                // 產生 PDF 並列印
                byte[] pdfBytes = _printService.GenerateInvoicePdf(request);
                _printService.ShowPrintPreviewAndPrint(pdfBytes);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"列印時發生錯誤：{ex.Message}", "錯誤",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
