//所有客戶資料

using invoicing.Event;
using invoicing.Models.DTO;
using invoicing.Repository.Interface;
using invoicing.Service.Interface;
using System.Data;

namespace invoicing.MasterData
{
    public partial class CustomerForm : Form
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IFormUIService _formUIService;
        private readonly EventBus _eventBus;
        public CustomerForm()
        {
            InitializeComponent();
        }

        public CustomerForm(ICustomerRepository customerRepository, IFormUIService formUIService, EventBus eventBus)
        {
            InitializeComponent();
            _customerRepository = customerRepository;
            _formUIService = formUIService;
            _eventBus = eventBus;

            _formUIService.AddTextBoxUnderline(txtInput);
            var customerData = _customerRepository.Get(z => !string.IsNullOrEmpty(z.CompanyFullName))
                .Select(x => new CustomerDto
                {
                    CompanyFullName = x.CompanyFullName,
                    Phone1 = x.Phone1,
                    FaxNumber = x.FaxNumber,
                    DeliveryAddress = x.DeliveryAddress
                }).OrderBy(y => y.CompanyFullName).ToList();
            dgvCustomerAll.DataSource = customerData;
            dgvCustomerAll.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvCustomerAll.DefaultCellStyle.Font = new Font("Microsoft JhengHei", 12);
            // 訂閱客戶選取事件，以支援自客戶管理資料切換時之雙向同步選取定位
            _eventBus.Subscribe<CustomerSelectEvent>(OnCustomerSelectedFromOtherForm);
        }

        /// <summary>
        /// 當其他表單（如客戶管理資料）切換或搜尋客戶時，同步定位客戶清單表格
        /// </summary>
        private void OnCustomerSelectedFromOtherForm(CustomerSelectEvent evt)
        {
            if (evt.Sender == this || string.IsNullOrWhiteSpace(evt.FullyName)) return;

            // 尋找表格中相符的客戶名稱列
            var row = dgvCustomerAll.Rows.Cast<DataGridViewRow>()
                        .Where(r => !r.IsNewRow)
                        .FirstOrDefault(r =>
                            r.Cells[nameof(CustomerDto.CompanyFullName)].Value != null &&
                            string.Equals(r.Cells[nameof(CustomerDto.CompanyFullName)].Value.ToString(), evt.FullyName, StringComparison.OrdinalIgnoreCase));

            if (row != null)
            {
                dgvCustomerAll.CurrentCell = row.Cells[0];
                dgvCustomerAll.FirstDisplayedScrollingRowIndex = row.Index;
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // 退訂事件以防止記憶體洩漏
            _eventBus.Unsubscribe<CustomerSelectEvent>(OnCustomerSelectedFromOtherForm);
            base.OnFormClosed(e);
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            string search = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(search))
                return;

            var row = dgvCustomerAll.Rows.Cast<DataGridViewRow>()
                        .Where(r => !r.IsNewRow)
                        .FirstOrDefault(r =>
                            r.Cells[nameof(CustomerDto.CompanyFullName)].Value != null &&
                            r.Cells[nameof(CustomerDto.CompanyFullName)].Value.ToString()
                              .IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);

            if (row == null)
            {
                MessageBox.Show("沒有這個公司");
                return;
            }

            dgvCustomerAll.CurrentCell = row.Cells[0];
            dgvCustomerAll.FirstDisplayedScrollingRowIndex = row.Index;
        }

        private void dgvCustomerAll_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string? companyName = dgvCustomerAll.Rows[e.RowIndex].Cells[0].Value?.ToString();

            if (string.IsNullOrEmpty(companyName))
                return;

            // 發布強型別客戶選取事件（標記自身為 Sender 避免遞迴），通知客戶管理資料切換
            _eventBus.Publish(new CustomerSelectEvent(companyName, this));
        }
    }
}
