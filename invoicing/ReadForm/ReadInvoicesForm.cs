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
            dtpStart.Value = DateTime.Today.AddMonths(-1);
            dtpEnd.Value = DateTime.Today;

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
                    (x.IsDeleted == false) &&
                    x.Date != null &&
                    x.Date.CompareTo(startDate) >= 0 &&
                    x.Date.CompareTo(endDate) <= 0);

                // 動態條件：若有來源單子名稱，加入篩選
                if (!string.IsNullOrEmpty(CallerOrderName))
                {
                    query = query.Where(x => x.OrderName == CallerOrderName);
                }

                // 轉換為 DTO
                _invoicingData = query
                    .OrderByDescending(x => x.OrderNumber)
                    .ThenByDescending(x => x.Date)
                    .Select(y => new InvoicingDTO
                    {
                        OrderNumber = y.OrderNumber,
                        Date = y.Date,
                        Customer = y.Customer,
                        OrderName = y.OrderName,
                        TotalAmount = y.TotalAmount,
                        Remark = y.Remark
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

                // 查詢選中訂單的明細資料
                var selectedDetails = _orderDetailRepository
                    .Get(x => selectedOrderNumbers.Contains(x.OrderNumber))
                    .Select(d => new InvoicingDetailDTO
                    {
                        ProductCode = d.ProductCode,
                        ProductName = d.ProductName,
                        Quantity = d.Quantity,
                        Unit = d.Unit,
                        UnitPrice = d.UnitPrice,
                        Amount = d.Amount,
                        Remark = d.Remark
                    }).ToList();

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
