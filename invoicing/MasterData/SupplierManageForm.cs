//管理廠商資料

using invoicing.Event;
using invoicing.Models.Entity;
using invoicing.Repository.Interface;
using invoicing.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace invoicing.MasterData
{
    public partial class SupplierManageForm : Form
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IFormUIService _formUIService;
        private readonly EventBus _eventBus;
        public SupplierManageForm()
        {
            InitializeComponent();
        }

        public SupplierManageForm(ISupplierRepository supplierRepository, IFormUIService formUIService, EventBus eventBus)
        {
            InitializeComponent();
            _supplierRepository = supplierRepository;
            _formUIService = formUIService;
            _eventBus = eventBus;

            _formUIService.AddTextBoxUnderline(txtSupplierName);
            _formUIService.AddTextBoxUnderline(txtSupplierAddress);
            _formUIService.AddTextBoxUnderline(txtSupplierTel);
            _formUIService.AddTextBoxUnderline(txtSupplierFax);

            //訂閱事件
            _eventBus.Subscribe<MasterSelectEvent>(OnSupplierSelected);
        }

        private void OnSupplierSelected(MasterSelectEvent masterSelectEvent)
        {
            SearchAsync(masterSelectEvent.FullyName);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // 記得退訂，避免記憶體洩漏
            _eventBus.Unsubscribe<MasterSelectEvent>(OnSupplierSelected);
            base.OnFormClosed(e);
        }

        private async Task SearchAsync(string companyName)
        {
            var supplier = await _supplierRepository.Get(x => x.CompanyFullName == companyName)
                                                    .Select(
                                                        y => new
                                                        {
                                                            y.CompanyCode,
                                                            y.CompanyFullName,
                                                            y.DeliveryAddress,
                                                            y.Phone1,
                                                            y.FaxNumber
                                                        })
                                                    .FirstOrDefaultAsync();
            if (supplier is not null)
            {
                lblSupplierIdValue.Text = supplier.CompanyCode;
                txtSupplierName.Text = supplier.CompanyFullName;
                txtSupplierAddress.Text = supplier.DeliveryAddress;
                txtSupplierTel.Text = supplier.Phone1;
                txtSupplierFax.Text = supplier.FaxNumber;
            }
        }


        private async void btnSupplierCreate_Click(object sender, EventArgs e)
        {
            int nowMaxSupplierCode = await _supplierRepository.GetMaxSupplierCode();
            Supplier supplier = new Supplier
            {
                CompanyCode = (nowMaxSupplierCode + 1).ToString(),
                CompanyFullName = txtSupplierName.Text,
                DeliveryAddress = txtSupplierAddress.Text,
                Phone1 = txtSupplierTel.Text,
                FaxNumber = txtSupplierFax.Text,
            };
            await _supplierRepository.AddAsync(supplier);
        }

        private async void btnSupplierModify_Click(object sender, EventArgs e)
        {
            var supplier = await _supplierRepository.Get(x => x.CompanyCode == lblSupplierIdValue.Text).FirstOrDefaultAsync();
            supplier.CompanyFullName = txtSupplierName.Text;
            supplier.DeliveryAddress = txtSupplierAddress.Text;
            supplier.Phone1 = txtSupplierTel.Text;
            supplier.FaxNumber = txtSupplierFax.Text;
            await _supplierRepository.UpdateAsync(supplier);
        }

        private async void btnSupplierSearch_Click(object sender, EventArgs e)
        {
            string supplierName = Interaction.InputBox("請輸入廠商名稱", "標題", "輸入框預設內容", -1, -1);
            var supplier = await _supplierRepository.Get(x => x.CompanyFullName == supplierName)
                                                    .Select(
                                                        y => new
                                                        {
                                                            y.CompanyCode,
                                                            y.CompanyFullName,
                                                            y.DeliveryAddress,
                                                            y.Phone1,
                                                            y.FaxNumber
                                                        })
                                                    .FirstOrDefaultAsync();
            if (supplier == null)
            {
                MessageBox.Show("請輸入正確的客戶名稱", "錯誤");
            }
            else
            {
                lblSupplierIdValue.Text = supplier.CompanyCode;
                txtSupplierName.Text = supplier.CompanyFullName;
                txtSupplierAddress.Text = supplier.DeliveryAddress;
                txtSupplierTel.Text = supplier.Phone1;
                txtSupplierFax.Text = supplier.FaxNumber;
            }
        }

        private async void btnSupplierDelete_Click(object sender, EventArgs e)
        {
            string supplierName = txtSupplierName.Text;
            DialogResult result = MessageBox.Show($"確定要刪除{supplierName}嗎？", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                var pk = await _supplierRepository.Get(x => x.CompanyCode == lblSupplierId.Text).Select(y => y.Id).FirstOrDefaultAsync();
                await _supplierRepository.DeleteAsync(pk);
            }
        }

        private void btnFormClear_Click(object sender, EventArgs e)
        {
            lblSupplierIdValue.Text = string.Empty;
            txtSupplierName.Text = string.Empty;
            txtSupplierAddress.Text = string.Empty;
            txtSupplierTel.Text = string.Empty;
            txtSupplierFax.Text = string.Empty;
        }
    }
}
