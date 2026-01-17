// 進貨單

using invoicing.Event;
using invoicing.Models.DTO;
using invoicing.Repository;
using invoicing.Repository.Interface;
using invoicing.Service;
using invoicing.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace invoicing.Transactions
{
    /// <summary>
    /// 進貨單表單
    /// </summary>
    public partial class PurchaseReceiptForm : Form
    {
        private readonly IFormUIService _formUIService;
        private readonly ITransactionsdgvService _transactionsdgvService;
        private readonly ITransactionsbtnService _transactionsbtnService;
        private readonly ISupplierRepository _supplierRepository;
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
        /// 當前使用的單子編號（舊資料用 OrderNumber，新資料用 NewOrderNumber）
        /// </summary>
        private string _currentOrderNumber = string.Empty;

        /// <summary>
        /// 標記是否使用新編號系統（NewOrderNumber）
        /// </summary>
        private bool _isUsingNewOrderNumber = false;

        /// <summary>
        /// 單子類型名稱
        /// </summary>
        private const string OrderType = "進貨單";

        public PurchaseReceiptForm()
        {
            InitializeComponent();
        }

        public PurchaseReceiptForm(
            IFormUIService formUIService,
            ITransactionsdgvService transactionsdgvService,
            ITransactionsbtnService transactionsbtnService,
            ISupplierRepository supplierRepository,
            IPrintService printService,
            EventBus eventBus)
        {
            InitializeComponent();
            _formUIService = formUIService;
            _transactionsdgvService = transactionsdgvService;
            _transactionsbtnService = transactionsbtnService;
            _supplierRepository = supplierRepository;
            _printService = printService;
            _eventBus = eventBus;

            InitializeFormControls();
            RegisterEventHandlers();
            SubscribeToEvents();
            _ = LoadSuppliersAsync();
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
            btnSave.Click += BtnSave_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnDelete.Click += BtnDelete_Click;
            btnCreateExcel.Click += BtnCreateExcel_Click;
            btnPrint.Click += BtnPrint_Click;

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
        /// 載入廠商資料到下拉選單
        /// </summary>
        private async Task LoadSuppliersAsync()
        {
            try
            {
                var suppliers = await _supplierRepository.Get()
                    .Where(x => !x.IsDeleted)
                    .OrderBy(x => x.CompanyFullName)
                    .Select(x => x.CompanyFullName)
                    .ToListAsync();

                cboCustomer.Items.Clear();
                foreach (var supplier in suppliers)
                {
                    cboCustomer.Items.Add(supplier);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"載入廠商資料失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region 按鈕事件處理

        /// <summary>
        /// 讀檔按鈕點擊事件
        /// </summary>
        private void BtnLoad_Click(object? sender, EventArgs e)
        {
            try
            {
                // 使用 Service 開啟 ReadInvoicesForm
                _transactionsbtnService.OpenReadInvoicesForm(nameof(PurchaseReceiptForm), OrderType);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"開啟讀檔視窗失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 儲存按鈕點擊事件
        /// </summary>
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
                    // 舊資料使用 OrderNumber，新資料使用 NewOrderNumber
                    OrderNumber = _isSaved && !_isUsingNewOrderNumber ? _currentOrderNumber : null,
                    NewOrderNumber = _isSaved && _isUsingNewOrderNumber ? _currentOrderNumber : null,
                    IsUpdate = _isSaved,
                    Details = _invoicingData.ToList()
                };

                var result = await _transactionsbtnService.SaveTransactionAsync(request);

                if (result.Success)
                {
                    _isSaved = true;
                    // 新資料使用 NewOrderNumber
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

        /// <summary>
        /// 刷新按鈕點擊事件
        /// </summary>
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

        /// <summary>
        /// 刪單按鈕點擊事件
        /// </summary>
        private async void BtnDelete_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_currentOrderNumber))
                {
                    MessageBox.Show("沒有可刪除的單子", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 依據資料類型傳入不同的編號
                var result = await _transactionsbtnService.DeleteTransactionAsync(
                    _currentOrderNumber,
                    _isUsingNewOrderNumber);

                if (result.Success)
                {
                    _isSaved = false;
                    _currentOrderNumber = string.Empty;
                    _isUsingNewOrderNumber = false;
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

        /// <summary>
        /// 創建 Excel 按鈕點擊事件
        /// </summary>
        private async void BtnCreateExcel_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cboCustomer.Text))
                {
                    MessageBox.Show("請選擇廠商", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    // 依據資料類型傳入不同的編號
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

        /// <summary>
        /// 列印按鈕點擊事件
        /// </summary>
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

                // 取得客戶資訊
                var supplier = await _supplierRepository.Get()
                    .Where(x => x.CompanyFullName == cboCustomer.Text && !x.IsDeleted)
                    .FirstOrDefaultAsync();

                var request = new PrintInvoiceRequest
                {
                    OrderType = OrderType,
                    CustomerName = cboCustomer.Text,
                    Phone = supplier?.Phone1 ?? "",
                    Fax = supplier?.FaxNumber ?? "",
                    Address = supplier?.DeliveryAddress ?? "",
                    // 關貿格式使用空白日期
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

        /// <summary>
        /// 處理單子選擇事件
        /// </summary>
        private void OnInvoicesSelected(InvoiceSelectedEvent e)
        {
            // 確認事件來源是本表單
            if (e.CallerFormType != nameof(PurchaseReceiptForm))
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

                // 設定廠商
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

                // 設定單子編號（判斷使用新/舊編號系統）
                if (!string.IsNullOrEmpty(firstInvoice.NewOrderNumber))
                {
                    // 新資料：使用 NewOrderNumber
                    _currentOrderNumber = firstInvoice.NewOrderNumber;
                    _isUsingNewOrderNumber = true;
                    lblNumber.Text = firstInvoice.NewOrderNumber;
                }
                else
                {
                    // 舊資料：使用 OrderNumber
                    _currentOrderNumber = firstInvoice.OrderNumber;
                    _isUsingNewOrderNumber = false;
                    lblNumber.Text = firstInvoice.OrderNumber;
                }
                _isSaved = true;
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
