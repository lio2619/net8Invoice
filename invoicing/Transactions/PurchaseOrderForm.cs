// 採購單

using invoicing.Event;
using invoicing.Models.DTO;
using invoicing.Repository.Interface;
using invoicing.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace invoicing.Transactions
{
    /// <summary>
    /// 採購單表單（無單價、金額欄位，使用廠商資料）
    /// </summary>
    public partial class PurchaseOrderForm : Form
    {
        private readonly IFormUIService _formUIService;
        private readonly ITransactionsdgvService _transactionsdgvService;
        private readonly ITransactionsbtnService _transactionsbtnService;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IPrintService _printService;
        private readonly EventBus _eventBus;

        /// <summary>
        /// 使用 BindingList 作為資料來源（採購單專用 DTO，無單價金額）
        /// </summary>
        private readonly BindingList<PurchaseOrderDetailDTO> _invoicingData = new();

        /// <summary>
        /// 用於取消非同步操作的 CancellationTokenSource
        /// </summary>
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        /// <summary>
        /// 標記是否已儲存過
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
        /// 單子類型名稱
        /// </summary>
        private const string OrderType = "採購單";

        public PurchaseOrderForm()
        {
            InitializeComponent();
        }

        public PurchaseOrderForm(
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
            // 採購單使用專用的初始化方法（無單價金額）
            InitializePurchaseOrderDataGridView();
            _transactionsdgvService.SetupProductCodeAutoComplete(dgvInvoicing, "貨品編號", _cancellationTokenSource.Token);
        }

        /// <summary>
        /// 初始化 DataGridView（採購單專用，無單價金額欄位）
        /// </summary>
        private void InitializePurchaseOrderDataGridView()
        {
            dgvInvoicing.AutoGenerateColumns = true;
            dgvInvoicing.DataSource = _invoicingData;
            dgvInvoicing.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvInvoicing.AllowUserToAddRows = true;

            // 禁用所有欄位的排序
            foreach (DataGridViewColumn column in dgvInvoicing.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        /// <summary>
        /// 註冊事件處理器
        /// </summary>
        private void RegisterEventHandlers()
        {
            dgvInvoicing.RowPostPaint += _transactionsdgvService.HandleRowPostPaint;
            dgvInvoicing.MouseDown += (sender, e) => HandleRightClickDelete(sender, e);

            // 採購單：只需取得品名和單位，不需計算金額
            dgvInvoicing.CellEndEdit += async (sender, e) =>
            {
                await HandlePurchaseOrderCellEndEditAsync(e);
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

        /// <summary>
        /// 處理右鍵刪除
        /// </summary>
        private void HandleRightClickDelete(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            var dgv = sender as DataGridView;
            if (dgv == null) return;

            var result = MessageBox.Show("是否刪除", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                var rowIndex = dgv.CurrentRow?.Index ?? -1;
                if (rowIndex >= 0 && rowIndex < _invoicingData.Count)
                {
                    _invoicingData.RemoveAt(rowIndex);
                }
            }
        }

        /// <summary>
        /// 採購單專用：CellEndEdit 處理（只取得品名和單位，不計算金額）
        /// </summary>
        private async Task HandlePurchaseOrderCellEndEditAsync(DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0 || e.RowIndex < 0) return;

            var productCode = dgvInvoicing.Rows[e.RowIndex].Cells[0].Value?.ToString();
            if (string.IsNullOrEmpty(productCode)) return;

            try
            {
                // 從服務層取得產品資訊（不含價格）
                var row = dgvInvoicing.Rows[e.RowIndex];
                var result = await _transactionsdgvService.FetchProductInfoAsync(row, productCode);
                
                // 由於採購單沒有單價欄位，需要清除可能設定的單價
                // （FetchProductInfoAsync 可能會設定 UnitPrice，但採購單不需要）
            }
            catch (Exception ex)
            {
                MessageBox.Show($"取得產品資訊失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SubscribeToEvents()
        {
            _eventBus.Subscribe<InvoiceSelectedEvent>(OnInvoicesSelected);
        }

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

        private void BtnLoad_Click(object? sender, EventArgs e)
        {
            try
            {
                _transactionsbtnService.OpenReadInvoicesForm(nameof(PurchaseOrderForm), OrderType);
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
                if (string.IsNullOrEmpty(cboCustomer.Text))
                {
                    MessageBox.Show("請選擇廠商", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 採購單總金額固定為 "0"
                var request = new SaveTransactionRequest
                {
                    OrderType = OrderType,
                    CustomerName = cboCustomer.Text,
                    Date = dtpDate.Value.ToString("yyyyMMdd"),
                    Remark = txtRemark.Text,
                    TotalAmount = "0", // 採購單不計算金額
                    OrderNumber = _isSaved && !_isUsingNewOrderNumber ? _currentOrderNumber : null,
                    NewOrderNumber = _isSaved && _isUsingNewOrderNumber ? _currentOrderNumber : null,
                    IsUpdate = _isSaved,
                    // 將 PurchaseOrderDetailDTO 轉換為 InvoicingDetailDTO（無單價金額）
                    Details = _invoicingData.Select(x => new InvoicingDetailDTO
                    {
                        ProductCode = x.ProductCode,
                        ProductName = x.ProductName,
                        Quantity = x.Quantity,
                        Unit = x.Unit,
                        UnitPrice = null,
                        Amount = null,
                        Remark = x.Remark
                    }).ToList()
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
                    cboCustomer.Text = "";
                    txtRemark.Text = "";
                    lblNumber.Text = "0";
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
                    MessageBox.Show("請選擇廠商", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var request = new CreateExcelRequest
                {
                    OrderType = OrderType,
                    CustomerName = cboCustomer.Text,
                    Date = dtpDate.Value,
                    Remark = txtRemark.Text,
                    TotalAmount = "0",
                    Details = _invoicingData.Select(x => new InvoicingDetailDTO
                    {
                        ProductCode = x.ProductCode,
                        ProductName = x.ProductName,
                        Quantity = x.Quantity,
                        Unit = x.Unit,
                        Remark = x.Remark
                    }).ToList(),
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
                    MessageBox.Show("請選擇廠商", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!_isSaved)
                {
                    MessageBox.Show("請先儲存單子", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

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
                    Date = dtpDate.Text,
                    OrderNumber = dtpDate.Value.ToString("yyyyMMdd") + _currentOrderNumber.PadLeft(3, '0'),
                    Remark = txtRemark.Text,
                    TotalAmount = "0",
                    IsSupplier = true,
                    PurchaseOrderDetails = _invoicingData.ToList()
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
            if (e.CallerFormType != nameof(PurchaseOrderForm))
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
                _invoicingData.Add(new PurchaseOrderDetailDTO
                {
                    ProductCode = detail.ProductCode,
                    ProductName = detail.ProductName,
                    Quantity = detail.Quantity,
                    Unit = detail.Unit,
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
                _isSaved = true;
            }
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
