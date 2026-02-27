using invoicing.Event;
using invoicing.Models.DTO;
using invoicing.Models.Entity;
using invoicing.Repository.Interface;

namespace invoicing.ReadForm
{
    /// <summary>
    /// 讀取單子表單，支援時間篩選與多選資料回傳
    /// </summary>
    public partial class ReadInvoicesForm : Form
    {
        private readonly IBaseRepository<CustomerOrder>? _customerOrderRepository;
        private readonly IBaseRepository<OrderDetail>? _orderDetailRepository;
        private readonly EventBus? _eventBus;

        /// <summary>
        /// 呼叫來源的表單類型名稱
        /// </summary>
        public string CallerFormType { get; set; } = string.Empty;

        /// <summary>
        /// 用於動態篩選的單子名稱（例如「銷退」、「銷出」）
        /// </summary>
        public string CallerOrderName { get; set; } = string.Empty;

        /// <summary>
        /// 查詢結果資料
        /// </summary>
        private List<InvoicingDTO> _invoicingData = new();

        /// <summary>
        /// 記錄上一次勾選的行索引（-1 表示尚未選取）
        /// </summary>
        private int _lastSelectedRowIndex = -1;

        public ReadInvoicesForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// DI 建構函式
        /// </summary>
        public ReadInvoicesForm(
            IBaseRepository<CustomerOrder> customerOrderRepository,
            IBaseRepository<OrderDetail> orderDetailRepository,
            EventBus eventBus)
        {
            InitializeComponent();
            _customerOrderRepository = customerOrderRepository;
            _orderDetailRepository = orderDetailRepository;
            _eventBus = eventBus;

            InitializeFormControls();
            RegisterEventHandlers();
        }

        /// <summary>
        /// 初始化表單控制項
        /// </summary>
        private void InitializeFormControls()
        {
            // 設定 DateTimePicker 預設值
            dtpStart.Value = DateTime.Today;
            dtpEnd.Value = DateTime.Today;

            // 初始化九乘九篩選控制項
            rdbNine.Checked = true;
            cboNine.Items.AddRange(new object[] { "全部", "7503", "7235", "5231", "5331" });
            cboNine.SelectedIndex = 0;
            cboNine.Enabled = true;

            // 初始化 DataGridView
            InitializeDataGridView();
        }

        /// <summary>
        /// 初始化 DataGridView，設定勾選欄位
        /// </summary>
        private void InitializeDataGridView()
        {
            dgvReadInvoicing.AllowUserToAddRows = false;
            dgvReadInvoicing.AllowUserToDeleteRows = false;
            dgvReadInvoicing.ReadOnly = false;
            dgvReadInvoicing.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReadInvoicing.MultiSelect = true;
        }

        /// <summary>
        /// 註冊事件處理器
        /// </summary>
        private void RegisterEventHandlers()
        {
            btnSearch.Click += BtnSearch_Click;
            btnOK.Click += BtnOK_Click;
            dgvReadInvoicing.CellClick += DgvReadInvoicing_CellClick;
            rdbNine.CheckedChanged += RdbNine_CheckedChanged;
        }

        /// <summary>
        /// 九乘九 RadioButton 切換事件，控制 cboNine 啟用狀態
        /// </summary>
        private void RdbNine_CheckedChanged(object? sender, EventArgs e)
        {
            cboNine.Enabled = rdbNine.Checked;
            if (!rdbNine.Checked)
            {
                cboNine.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 查詢按鈕點擊事件
        /// </summary>
        private void BtnSearch_Click(object? sender, EventArgs e)
        {
            try
            {
                string startDate = dtpStart.Value.ToString("yyyyMMdd");
                string endDate = dtpEnd.Value.ToString("yyyyMMdd");

                // 基本查詢條件：未刪除且在時間範圍內
                var query = _customerOrderRepository.Get(x =>
                    (x.Deleted == "0") &&
                    x.Date != null &&
                    x.Date.CompareTo(startDate) >= 0 &&
                    x.Date.CompareTo(endDate) <= 0);

                // 動態條件：若有來源單子名稱，加入篩選
                if (!string.IsNullOrEmpty(CallerOrderName))
                {
                    query = query.Where(x => x.OrderName == CallerOrderName);
                }

                // 九乘九客戶篩選
                if (rdbNine.Checked)
                {
                    // 僅顯示九乘九相關客戶
                    query = query.Where(x => x.Customer != null && x.Customer.Contains("九乘九"));

                    // 若選擇特定店號（非「全部」），進一步篩選
                    string selectedStore = cboNine.SelectedItem?.ToString() ?? "";
                    if (selectedStore != "全部" && !string.IsNullOrEmpty(selectedStore))
                    {
                        query = query.Where(x => x.Customer != null && x.Customer.Contains(selectedStore));
                    }
                }
                else
                {
                    // 排除九乘九相關客戶
                    query = query.Where(x => x.Customer == null || !x.Customer.Contains("九乘九"));
                }

                // 轉換為 DTO
                _invoicingData = query
                    .OrderByDescending(x => x.NewOrderNumber)
                    .ThenByDescending(x => x.Date)
                    .Select(y => new InvoicingDTO
                    {
                        Id = y.Id,
                        OrderNumber = y.OrderNumber,
                        NewOrderNumber = y.NewOrderNumber,
                        Date = y.Date,
                        Customer = y.Customer,
                        OrderName = y.OrderName,
                        TotalAmount = y.TotalAmount,
                        Remark = y.Remark,
                        Customs = y.Customs
                    }).ToList();

                // 重新設定 DataGridView
                RefreshDataGridView();

                if (_invoicingData.Count == 0)
                {
                    MessageBox.Show("查無符合條件的資料", "查詢結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查詢發生錯誤：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 重新整理 DataGridView，包含勾選欄位
        /// </summary>
        private void RefreshDataGridView()
        {
            dgvReadInvoicing.DataSource = null;
            dgvReadInvoicing.Columns.Clear();

            // 新增勾選欄位
            var checkColumn = new DataGridViewCheckBoxColumn
            {
                Name = "Select",
                HeaderText = "選擇",
                Width = 50,
                ReadOnly = false,
                TrueValue = true,
                FalseValue = false
            };
            dgvReadInvoicing.Columns.Add(checkColumn);

            // 綁定資料
            dgvReadInvoicing.DataSource = _invoicingData;

            // 設定其他欄位為唯讀
            foreach (DataGridViewColumn col in dgvReadInvoicing.Columns)
            {
                if (col.Name != "Select")
                {
                    col.ReadOnly = true;
                }
            }

            // 重置上次選取的行索引
            _lastSelectedRowIndex = -1;
        }

        /// <summary>
        /// 點擊任意欄位時，僅勾選該行（單選模式，O(1) 操作）
        /// </summary>
        private void DgvReadInvoicing_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // 取消上一次勾選的行
            if (_lastSelectedRowIndex >= 0 && _lastSelectedRowIndex < dgvReadInvoicing.Rows.Count)
            {
                dgvReadInvoicing.Rows[_lastSelectedRowIndex].Cells["Select"].Value = false;
            }

            // 勾選當前點擊的行
            dgvReadInvoicing.Rows[e.RowIndex].Cells["Select"].Value = true;
            _lastSelectedRowIndex = e.RowIndex;
        }

        /// <summary>
        /// 確定按鈕點擊事件
        /// </summary>
        private void BtnOK_Click(object? sender, EventArgs e)
        {
            try
            {
                // 取得勾選的訂單編號
                var selectedOrderNumbers = new List<string>();
                var selectedInvoices = new List<InvoicingDTO>();

                foreach (DataGridViewRow row in dgvReadInvoicing.Rows)
                {
                    if (row.Cells["Select"].Value != null &&
                        Convert.ToBoolean(row.Cells["Select"].Value) == true)
                    {
                        var invoice = _invoicingData[row.Index];
                        selectedInvoices.Add(invoice);
                        selectedOrderNumbers.Add(invoice.OrderNumber);
                    }
                }

                if (selectedInvoices.Count == 0)
                {
                    MessageBox.Show("請至少選擇一筆資料", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 查詢選中訂單的明細資料（區分新舊資料）
                var selectedDetails = new List<InvoicingDetailDTO>();

                foreach (var invoice in selectedInvoices)
                {
                    List<OrderDetail> details;
                    if (!string.IsNullOrEmpty(invoice.NewOrderNumber))
                    {
                        // 新資料：用 CustomerOrderId 查詢
                        details = _orderDetailRepository
                            .Get(x => x.CustomerOrderId == invoice.Id)
                            .ToList();
                    }
                    else
                    {
                        // 舊資料：用 OrderNumber 查詢
                        details = _orderDetailRepository
                            .Get(x => x.OrderNumber == invoice.OrderNumber)
                            .ToList();
                    }

                    selectedDetails.AddRange(details.Select(d => new InvoicingDetailDTO
                    {
                        ProductCode = d.ProductCode,
                        ProductName = d.ProductName,
                        Quantity = d.Quantity,
                        Unit = d.Unit,
                        UnitPrice = d.UnitPrice,
                        Amount = d.Amount,
                        Remark = d.Remark
                    }));
                }

                // 發布事件
                _eventBus.Publish(new InvoiceSelectedEvent(
                    selectedInvoices,
                    selectedDetails,
                    CallerFormType));

                // 關閉視窗
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"處理發生錯誤：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
