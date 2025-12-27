// 銷貨退回單

using invoicing.Event;
using invoicing.Models.DTO;
using invoicing.Service.Interface;
using System.ComponentModel;

namespace invoicing.Transactions
{
    /// <summary>
    /// 銷貨退回單表單
    /// </summary>
    public partial class SalesReturnForm : Form
    {
        private readonly IFormUIService _formUIService;
        private readonly ITransactionsdgvService _transactionsdgvService;
        private readonly ITransactionsbtnService _transactionsbtnService;
        private readonly EventBus _eventBus;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 使用 BindingList 作為資料來源，支援 DataGridView 自動增行功能。
        /// </summary>
        private readonly BindingList<InvoicingDetailDTO> _invoicingData = new();

        /// <summary>
        /// 用於取消非同步操作的 CancellationTokenSource。
        /// </summary>
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        public SalesReturnForm()
        {
            InitializeComponent();
        }

        public SalesReturnForm(
            IFormUIService formUIService,
            ITransactionsdgvService transactionsdgvService,
            ITransactionsbtnService transactionsbtnService,
            EventBus eventBus,
            IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _formUIService = formUIService;
            _transactionsdgvService = transactionsdgvService;
            _transactionsbtnService = transactionsbtnService;
            _eventBus = eventBus;
            _serviceProvider = serviceProvider;

            InitializeFormControls();
            RegisterEventHandlers();
            SubscribeToEvents();
        }

        /// <summary>
        /// 初始化表單控制項
        /// </summary>
        private void InitializeFormControls()
        {
            _formUIService.AddTextBoxUnderline(txtRemark);
            _transactionsdgvService.InitializeDataGridView(dgvInvoicing, _invoicingData);
            _transactionsdgvService.SetupProductCodeAutoComplete(dgvInvoicing, "貨品編號", _cancellationTokenSource.Token);
        }

        /// <summary>
        /// 註冊事件處理器
        /// </summary>
        private void RegisterEventHandlers()
        {
            // DataGridView 事件（使用服務層方法）
            dgvInvoicing.RowPostPaint += _transactionsdgvService.HandleRowPostPaint;
            dgvInvoicing.MouseDown += (sender, e) => _transactionsdgvService.HandleRightClickDelete(sender, e);

            // 業務邏輯事件（委派給服務層處理）
            dgvInvoicing.CellEndEdit += async (sender, e) =>
            {
                await _transactionsdgvService.HandleCellEndEditAsync(
                    dgvInvoicing,
                    e,
                    productColumnHeaderText: "貨品編號",
                    quantityColumnHeaderText: "數量",
                    priceColumnHeaderText: "單價",
                    onTotalAmountChanged: UpdateTotalAmountLabel,
                    _cancellationTokenSource.Token);
            };

            dgvInvoicing.RowsRemoved += (sender, e) =>
            {
                _transactionsdgvService.HandleRowsRemoved(dgvInvoicing, UpdateTotalAmountLabel);
            };

            // 按鈕事件
            btnLoad.Click += BtnLoad_Click;

            // 表單關閉事件
            FormClosing += (sender, e) =>
            {
                UnsubscribeFromEvents();
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
            };
        }

        /// <summary>
        /// 訂閱 EventBus 事件
        /// </summary>
        private void SubscribeToEvents()
        {
            _eventBus.Subscribe<InvoiceSelectedEvent>(OnInvoicesSelected);
        }

        /// <summary>
        /// 取消訂閱 EventBus 事件
        /// </summary>
        private void UnsubscribeFromEvents()
        {
            _eventBus.Unsubscribe<InvoiceSelectedEvent>(OnInvoicesSelected);
        }

        /// <summary>
        /// 讀檔按鈕點擊事件
        /// </summary>
        private void BtnLoad_Click(object? sender, EventArgs e)
        {
            try
            {
                // 使用 Service 開啟 ReadInvoicesForm
                _transactionsbtnService.OpenReadInvoicesForm(nameof(SalesReturnForm), "出貨退出單");  // 銷貨退回單的單子名稱
            }
            catch (Exception ex)
            {
                MessageBox.Show($"開啟讀檔視窗失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 處理單子選擇事件
        /// </summary>
        private void OnInvoicesSelected(InvoiceSelectedEvent e)
        {
            // 確認事件來源是本表單
            if (e.CallerFormType != nameof(SalesReturnForm))
            {
                return;
            }

            // 詢問是否清空現有資料
            if (_invoicingData.Count > 0)
            {
                var result = MessageBox.Show(
                    "是否要清空現有資料並載入選擇的單子？\n選擇「否」將追加到現有資料後面。",
                    "載入確認",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Cancel)
                {
                    return;
                }

                if (result == DialogResult.Yes)
                {
                    _invoicingData.Clear();
                }
            }

            // 載入選中的明細資料
            foreach (var detail in e.SelectedDetails)
            {
                _invoicingData.Add(new InvoicingDetailDTO
                {
                    ProductCode = detail.ProductCode,
                    ProductName = detail.ProductName,
                    Quantity = detail.Quantity,
                    Unit = detail.Unit,
                    UnitPrice = detail.UnitPrice,
                    Amount = detail.Amount,
                    Remark = detail.Remark
                });
            }

            // 更新表單資訊（取用第一筆訂單的摘要）
            if (e.SelectedInvoices.Count > 0)
            {
                var firstInvoice = e.SelectedInvoices[0];

                // 設定客戶
                if (!string.IsNullOrEmpty(firstInvoice.Customer))
                {
                    cboCustomer.Text = firstInvoice.Customer;
                }

                // 設定日期
                if (!string.IsNullOrEmpty(firstInvoice.Date) && firstInvoice.Date.Length == 8)
                {
                    if (DateTime.TryParseExact(firstInvoice.Date, "yyyyMMdd", null,
                        System.Globalization.DateTimeStyles.None, out var date))
                    {
                        dtpDate.Value = date;
                    }
                }

                // 設定備註
                if (!string.IsNullOrEmpty(firstInvoice.Remark))
                {
                    txtRemark.Text = firstInvoice.Remark;
                }

                // 設定單子編號
                lblNumber.Text = firstInvoice.OrderNumber;
            }

            // 更新總金額
            UpdateTotalAmountFromData();
        }

        /// <summary>
        /// 從資料計算並更新總金額
        /// </summary>
        private void UpdateTotalAmountFromData()
        {
            double total = 0;
            foreach (var item in _invoicingData)
            {
                if (double.TryParse(item.Amount, out var amount))
                {
                    total += amount;
                }
            }
            UpdateTotalAmountLabel(total);
        }

        /// <summary>
        /// 更新總金額標籤
        /// </summary>
        /// <param name="total">總金額</param>
        private void UpdateTotalAmountLabel(double total)
        {
            lblAmount.Text = total.ToString("0.##");
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // 優先處理建議清單的鍵盤操作（上/下/Enter/Escape）
            if (_transactionsdgvService?.HandleSuggestionKeyPress(keyData) == true)
            {
                return true;
            }

            // 處理 Enter 轉 Tab
            if (_transactionsdgvService?.HandleEnterAsTab(keyData) == true)
            {
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
