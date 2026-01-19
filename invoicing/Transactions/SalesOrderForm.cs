// 訂貨單

using invoicing.Event;
using invoicing.Models.DTO;
using invoicing.Repository.Interface;
using invoicing.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace invoicing.Transactions
{
    /// <summary>
    /// 訂貨單表單
    /// </summary>
    public partial class SalesOrderForm : Form
    {
        private readonly IFormUIService _formUIService;
        private readonly ITransactionsdgvService _transactionsdgvService;
        private readonly ITransactionsbtnService _transactionsbtnService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPrintService _printService;
        private readonly EventBus _eventBus;

        /// <summary>
        /// 使用 BindingList 作為資料來源，支援 DataGridView 自動增行功能。
        /// </summary>
        private readonly BindingList<InvoicingDetailDTO> _invoicingData = new();

        /// <summary>
        /// 用於取消非同步操作的 CancellationTokenSource。
        /// </summary>
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        /// <summary>
        /// 標記是否已儲存過（用於判斷新增或更新模式）
        /// </summary>
        private bool _isSaved = false;

        /// <summary>
        /// 當前使用的單子編號
        /// </summary>
        private string _currentOrderNumber = string.Empty;

        /// <summary>
        /// 標記是否使用新編號系統
        /// </summary>
        private bool _isUsingNewOrderNumber = false;

        /// <summary>
        /// 當前訂單的 CustomerOrder.Id（用於精確定位）
        /// </summary>
        private int _currentCustomerOrderId = 0;

        /// <summary>
        /// 單子類型名稱
        /// </summary>
        private const string OrderType = "訂貨單";

        public SalesOrderForm()
        {
            InitializeComponent();
        }

        public SalesOrderForm(
            IFormUIService formUIService,
            ITransactionsdgvService transactionsdgvService,
            ITransactionsbtnService transactionsbtnService,
            ICustomerRepository customerRepository,
            IPrintService printService,
            EventBus eventBus)
        {
            InitializeComponent();
            _formUIService = formUIService;
            _transactionsdgvService = transactionsdgvService;
            _transactionsbtnService = transactionsbtnService;
            _customerRepository = customerRepository;
            _printService = printService;
            _eventBus = eventBus;

            InitializeFormControls();
            RegisterEventHandlers();
            SubscribeToEvents();
            _ = LoadCustomersAsync();
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
            dgvInvoicing.RowPostPaint += _transactionsdgvService.HandleRowPostPaint;
            dgvInvoicing.MouseDown += (sender, e) => _transactionsdgvService.HandleRightClickDelete(sender, e);
            dgvInvoicing.CellClick += (sender, e) => dgvInvoicing_CellClick(sender, e);

            dgvInvoicing.CellEndEdit += async (sender, e) =>
            {
                await _transactionsdgvService.HandleCellEndEditAsync(
                    dgvInvoicing,
                    e,
                    productColumnHeaderText: "貨品編號",
                    quantityColumnHeaderText: "數量",
                    priceColumnHeaderText: "單價",
                    onTotalAmountChanged: UpdateTotalAmountLabel,
                    onProductCodeSelected: code => _eventBus.Publish(new MasterSelectEvent(code)),
                    _cancellationTokenSource.Token);
            };


            dgvInvoicing.RowsRemoved += (sender, e) =>
            {
                _transactionsdgvService.HandleRowsRemoved(dgvInvoicing, UpdateTotalAmountLabel);
            };

            btnLoad.Click += BtnLoad_Click;
            btnSave.Click += BtnSave_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnDelete.Click += BtnDelete_Click;
            btnCreateExcel.Click += BtnCreateExcel_Click;
            btnPrint.Click += BtnPrint_Click;

            FormClosing += (sender, e) =>
            {
                UnsubscribeFromEvents();
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
            };
        }

        private void SubscribeToEvents()
        {
            _eventBus.Subscribe<InvoiceSelectedEvent>(OnInvoicesSelected);
        }

        private void UnsubscribeFromEvents()
        {
            _eventBus.Unsubscribe<InvoiceSelectedEvent>(OnInvoicesSelected);
        }

        private async Task LoadCustomersAsync()
        {
            try
            {
                var customers = await _customerRepository.Get()
                    .Where(x => !x.IsDeleted)
                    .OrderBy(x => x.CompanyFullName)
                    .Select(x => x.CompanyFullName)
                    .ToListAsync();

                cboCustomer.Items.Clear();
                foreach (var customer in customers)
                {
                    cboCustomer.Items.Add(customer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"載入客戶資料失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region 按鈕事件處理

        private void BtnLoad_Click(object? sender, EventArgs e)
        {
            try
            {
                _transactionsbtnService.OpenReadInvoicesForm(nameof(SalesOrderForm), OrderType);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"開啟讀檔視窗失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnSave_Click(object? sender, EventArgs e)
        {
            try
            {
                var request = new SaveTransactionRequest
                {
                    OrderType = OrderType,
                    CustomerName = cboCustomer.Text,
                    Date = dtpDate.Value.ToString("yyyyMMdd"),
                    Remark = txtRemark.Text,
                    TotalAmount = lblAmount.Text,
                    OrderNumber = _isSaved && !_isUsingNewOrderNumber ? _currentOrderNumber : null,
                    NewOrderNumber = _isSaved && _isUsingNewOrderNumber ? _currentOrderNumber : null,
                    IsUpdate = _isSaved,
                    Details = _invoicingData.ToList(),
                    CustomerOrderId = _isSaved ? _currentCustomerOrderId : null
                };

                var result = await _transactionsbtnService.SaveTransactionAsync(request);

                if (result.Success)
                {
                    _isSaved = true;
                    if (!string.IsNullOrEmpty(result.NewOrderNumber))
                    {
                        _currentOrderNumber = result.NewOrderNumber;
                        _isUsingNewOrderNumber = true;
                        lblNumber.Text = result.NewOrderNumber;
                    }
                    else if (!string.IsNullOrEmpty(result.OrderNumber))
                    {
                        _currentOrderNumber = result.OrderNumber;
                        lblNumber.Text = result.OrderNumber;
                    }
                }

                MessageBox.Show(result.Message, result.Success ? "成功" : "錯誤",
                    MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"儲存失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            try
            {
                var result = _transactionsbtnService.ConfirmRefresh();
                if (result.Confirmed)
                {
                    _isSaved = false;
                    _currentOrderNumber = string.Empty;
                    _isUsingNewOrderNumber = false;
                    _currentCustomerOrderId = 0;
                    cboCustomer.Text = "";
                    txtRemark.Text = "";
                    lblNumber.Text = "0";
                    lblAmount.Text = "0";
                    dtpDate.Value = DateTime.Now;
                    _invoicingData.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"刷新失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnDelete_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_currentOrderNumber))
                {
                    MessageBox.Show("沒有可刪除的單子", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var result = await _transactionsbtnService.DeleteTransactionAsync(
                    _currentCustomerOrderId,
                    _currentOrderNumber,
                    _isUsingNewOrderNumber,
                    OrderType,
                    dtpDate.Value.ToString("yyyyMMdd"));

                if (result.Success)
                {
                    _isSaved = false;
                    _currentOrderNumber = string.Empty;
                    _isUsingNewOrderNumber = false;
                    _currentCustomerOrderId = 0;
                    cboCustomer.Text = "";
                    txtRemark.Text = "";
                    lblNumber.Text = "0";
                    lblAmount.Text = "0";
                    _invoicingData.Clear();
                }

                MessageBox.Show(result.Message, result.Success ? "成功" : "錯誤",
                    MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"刪除失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnCreateExcel_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cboCustomer.Text))
                {
                    MessageBox.Show("請選擇客戶", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var request = new CreateExcelRequest
                {
                    OrderType = OrderType,
                    CustomerName = cboCustomer.Text,
                    Date = dtpDate.Value,
                    Remark = txtRemark.Text,
                    TotalAmount = lblAmount.Text,
                    Details = _invoicingData.ToList(),
                    OrderNumber = !_isUsingNewOrderNumber ? _currentOrderNumber : null,
                    NewOrderNumber = _isUsingNewOrderNumber ? _currentOrderNumber : null,
                    IsUsingNewOrderNumber = _isUsingNewOrderNumber
                };

                await _transactionsbtnService.CreateExcelAsync(request);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"建立 Excel 失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnPrint_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cboCustomer.Text))
                {
                    MessageBox.Show("請選擇客戶", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!_isSaved)
                {
                    MessageBox.Show("請先儲存單子", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var customer = await _customerRepository.Get()
                    .Where(x => x.CompanyFullName == cboCustomer.Text && !x.IsDeleted)
                    .FirstOrDefaultAsync();

                var request = new PrintInvoiceRequest
                {
                    OrderType = OrderType,
                    CustomerName = cboCustomer.Text,
                    Phone = customer?.Phone1 ?? "",
                    Fax = customer?.FaxNumber ?? "",
                    Address = customer?.DeliveryAddress ?? "",
                    Date = dtpDate.Text,
                    OrderNumber = dtpDate.Value.ToString("yyyyMMdd") + _currentOrderNumber.PadLeft(3, '0'),
                    Remark = txtRemark.Text,
                    TotalAmount = lblAmount.Text,
                    IsSupplier = false,
                    Details = _invoicingData.ToList()
                };

                var pdfBytes = _printService.GenerateInvoicePdf(request);
                _printService.ShowPrintPreviewAndPrint(pdfBytes);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"列印失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void OnInvoicesSelected(InvoiceSelectedEvent e)
        {
            if (e.CallerFormType != nameof(SalesOrderForm))
            {
                return;
            }

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

            if (e.SelectedInvoices.Count > 0)
            {
                var firstInvoice = e.SelectedInvoices[0];

                if (!string.IsNullOrEmpty(firstInvoice.Customer))
                {
                    cboCustomer.Text = firstInvoice.Customer;
                }

                if (!string.IsNullOrEmpty(firstInvoice.Date) && firstInvoice.Date.Length == 8)
                {
                    if (DateTime.TryParseExact(firstInvoice.Date, "yyyyMMdd", null,
                        System.Globalization.DateTimeStyles.None, out var date))
                    {
                        dtpDate.Value = date;
                    }
                }

                if (!string.IsNullOrEmpty(firstInvoice.Remark))
                {
                    txtRemark.Text = firstInvoice.Remark;
                }

                if (!string.IsNullOrEmpty(firstInvoice.NewOrderNumber))
                {
                    _currentOrderNumber = firstInvoice.NewOrderNumber;
                    _isUsingNewOrderNumber = true;
                    lblNumber.Text = firstInvoice.NewOrderNumber;
                }
                else
                {
                    _currentOrderNumber = firstInvoice.OrderNumber;
                    _isUsingNewOrderNumber = false;
                    lblNumber.Text = firstInvoice.OrderNumber;
                }
                _currentCustomerOrderId = firstInvoice.Id;
                _isSaved = true;
            }

            UpdateTotalAmountFromData();
        }

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

        private void UpdateTotalAmountLabel(double total)
        {
            lblAmount.Text = total.ToString("0.##");
        }

        private void dgvInvoicing_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string? productCode = dgvInvoicing.Rows[e.RowIndex].Cells[0].Value?.ToString();

            if (string.IsNullOrEmpty(productCode))
                return;

            // 觸發事件
            _eventBus.Publish(new MasterSelectEvent(productCode));
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (_transactionsdgvService?.HandleSuggestionKeyPress(keyData) == true)
            {
                return true;
            }

            if (_transactionsdgvService?.HandleEnterAsTab(keyData) == true)
            {
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
