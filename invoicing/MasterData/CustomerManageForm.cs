//管理客戶資料

using invoicing.Event;
using invoicing.Models.Entity;
using invoicing.Repository.Interface;
using invoicing.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace invoicing.MasterData
{
    public partial class CustomerManageForm : Form
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IFormUIService _formUIService;
        private readonly EventBus _eventBus;
        public CustomerManageForm()
        {
            InitializeComponent();
        }

        public CustomerManageForm(ICustomerRepository customerRepository, IFormUIService formUIService, EventBus eventBus)
        {
            InitializeComponent();
            _customerRepository = customerRepository;
            _formUIService = formUIService;
            _eventBus = eventBus;

            _formUIService.AddTextBoxUnderline(txtCustomerName);
            _formUIService.AddTextBoxUnderline(txtCustomerAddress);
            _formUIService.AddTextBoxUnderline(txtCustomerTel);
            _formUIService.AddTextBoxUnderline(txtCustomerFax);

            //訂閱事件
            _eventBus.Subscribe<MasterSelectEvent>(OnCustomerSelected);
        }

        private void OnCustomerSelected(MasterSelectEvent masterSelectEvent)
        {
            SearchAsync(masterSelectEvent.FullyName);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // 記得退訂，避免記憶體洩漏
            _eventBus.Unsubscribe<MasterSelectEvent>(OnCustomerSelected);
            base.OnFormClosed(e);
        }

        private async Task SearchAsync(string companyName)
        {
            var customer = await _customerRepository.Get(x => x.CompanyFullName == companyName)
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
            if (customer is not null)
            {
                lblCustomerIdValue.Text = customer.CompanyCode;
                txtCustomerName.Text = customer.CompanyFullName;
                txtCustomerAddress.Text = customer.DeliveryAddress;
                txtCustomerTel.Text = customer.Phone1;
                txtCustomerFax.Text = customer.FaxNumber;
            }
            else
            {
                MessageBox.Show("請輸入正確的客戶名稱", "錯誤");
            }
        }

        private async void btnCustomerCreate_Click(object sender, EventArgs e)
        {
            int nowMaxCompanyCode = await _customerRepository.GetMaxCompanyCode();
            Customer customer = new Customer
            {
                CompanyCode = (nowMaxCompanyCode + 1).ToString(),
                CompanyFullName = txtCustomerName.Text,
                DeliveryAddress = txtCustomerAddress.Text,
                Phone1 = txtCustomerTel.Text,
                FaxNumber = txtCustomerFax.Text,
            };
            await _customerRepository.AddAsync(customer);
        }

        private async void btnCustomerModify_Click(object sender, EventArgs e)
        {
            var customer = await _customerRepository.Get(x => x.CompanyCode == lblCustomerIdValue.Text).FirstOrDefaultAsync();
            customer.CompanyFullName = txtCustomerName.Text;
            customer.DeliveryAddress = txtCustomerAddress.Text;
            customer.Phone1 = txtCustomerTel.Text;
            customer.FaxNumber = txtCustomerFax.Text;
            await _customerRepository.UpdateAsync(customer);
        }

        private async void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            string customerName = Interaction.InputBox("請輸入客戶名稱", "標題", "輸入框預設內容", -1, -1);
            await SearchAsync(customerName);
        }

        private async void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            string companyName = txtCustomerName.Text;
            DialogResult result = MessageBox.Show($"確定要刪除{companyName}嗎？", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                var pk = await _customerRepository.Get(x => x.CompanyCode == lblCustomerIdValue.Text).Select(y => y.Id).FirstOrDefaultAsync();
                await _customerRepository.DeleteAsync(pk);
            }
        }

        private void btnFormClear_Click(object sender, EventArgs e)
        {
            lblCustomerIdValue.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtCustomerAddress.Text = string.Empty;
            txtCustomerTel.Text = string.Empty;
            txtCustomerFax.Text = string.Empty;
        }
    }
}
