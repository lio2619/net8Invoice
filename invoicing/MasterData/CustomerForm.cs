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

            // 觸發事件
            _eventBus.Publish(new MasterSelectEvent(companyName));
        }
    }
}
