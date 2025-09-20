//所有廠商資料

using invoicing.Event;
using invoicing.Models.DTO;
using invoicing.Repository.Interface;
using invoicing.Service.Interface;

namespace invoicing.MasterData
{
    public partial class SupplierForm : Form
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IFormUIService _formUIService;
        private readonly EventBus _eventBus;
        public SupplierForm()
        {
            InitializeComponent();
        }

        public SupplierForm(ISupplierRepository supplierRepository, IFormUIService formUIService, EventBus eventBus)
        {
            InitializeComponent();
            _supplierRepository = supplierRepository;
            _formUIService = formUIService;
            _eventBus = eventBus;

            _formUIService.AddTextBoxUnderline(txtInput);
            var supplierData = _supplierRepository.Get(z => !string.IsNullOrEmpty(z.CompanyFullName))
                .Select(x => new SupplierDTO
                {
                    CompanyFullName = x.CompanyFullName,
                    Phone1 = x.Phone1,
                    FaxNumber = x.FaxNumber,
                    DeliveryAddress = x.DeliveryAddress
                }).OrderBy(y => y.CompanyFullName).ToList();
            dgvSupplierAll.DataSource = supplierData;
            dgvSupplierAll.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvSupplierAll.DefaultCellStyle.Font = new Font("Microsoft JhengHei", 12);
        }


        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            string search = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(search))
                return;

            var row = dgvSupplierAll.Rows.Cast<DataGridViewRow>()
                        .Where(r => !r.IsNewRow)
                        .FirstOrDefault(r =>
                            r.Cells[nameof(SupplierDTO.CompanyFullName)].Value != null &&
                            r.Cells[nameof(SupplierDTO.CompanyFullName)].Value.ToString()
                              .IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);

            if (row == null)
            {
                MessageBox.Show("沒有這個公司");
                return;
            }

            dgvSupplierAll.CurrentCell = row.Cells[0];
            dgvSupplierAll.FirstDisplayedScrollingRowIndex = row.Index;
        }

        private void dgvSupplierAll_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string? companyName = dgvSupplierAll.Rows[e.RowIndex].Cells[0].Value?.ToString();

            if (string.IsNullOrEmpty(companyName))
                return;

            // 觸發事件
            _eventBus.Publish(new MasterSelectEvent(companyName));
        }
    }
}
