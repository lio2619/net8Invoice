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

            // 訂閱客戶選取專屬事件，避免接收到出貨單等其他表單的產品編號
            _eventBus.Subscribe<CustomerSelectEvent>(OnCustomerSelected);
        }

        private async void OnCustomerSelected(CustomerSelectEvent evt)
        {
            // 若為自身發布則略過，避免重複處理
            if (evt.Sender == this || string.IsNullOrWhiteSpace(evt.FullyName)) return;

            // 僅同步連動更新表單資料，不呼叫 BringToFront() 與 Activate() 移動至最上層或搶奪焦點，
            // 避免干擾使用者在其他作業表單（如出貨單、銷貨單等）上的操作流程
            await SearchAsync(evt.FullyName, notifyOtherForms: false);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // 記得退訂，避免記憶體洩漏
            _eventBus.Unsubscribe<CustomerSelectEvent>(OnCustomerSelected);
            base.OnFormClosed(e);
        }

        private async Task<bool> SearchAsync(string companyName, bool notifyOtherForms = true)
        {
            if (string.IsNullOrWhiteSpace(companyName))
            {
                MessageBox.Show("請輸入客戶名稱", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
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

                    // 若是由管理表單主動搜尋，則雙向通知客戶資料清單表單同步定位該客戶
                    if (notifyOtherForms && !string.IsNullOrEmpty(customer.CompanyFullName))
                    {
                        _eventBus.Publish(new CustomerSelectEvent(customer.CompanyFullName, this));
                    }

                    return true;
                }
                else
                {
                    MessageBox.Show("找不到此客戶", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查詢客戶發生錯誤：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// 驗證必填欄位
        /// </summary>
        private bool ValidateRequiredFields()
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                MessageBox.Show("請輸入客戶名稱", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomerName.Focus();
                return false;
            }
            return true;
        }

        private async void btnCustomerCreate_Click(object sender, EventArgs e)
        {
            try
            {
                // 驗證必填欄位
                if (!ValidateRequiredFields()) return;

                // 檢查客戶名稱是否已存在
                var existingCustomer = await _customerRepository.Get(x => x.CompanyFullName == txtCustomerName.Text).FirstOrDefaultAsync();
                if (existingCustomer != null)
                {
                    MessageBox.Show("此客戶名稱已存在", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCustomerName.Focus();
                    return;
                }

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
                
                // 顯示新客戶編號
                lblCustomerIdValue.Text = customer.CompanyCode;
                MessageBox.Show("新增成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"新增失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCustomerModify_Click(object sender, EventArgs e)
        {
            try
            {
                // 驗證必填欄位
                if (!ValidateRequiredFields()) return;

                if (string.IsNullOrWhiteSpace(lblCustomerIdValue.Text))
                {
                    MessageBox.Show("請先搜尋要修改的客戶", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var customer = await _customerRepository.Get(x => x.CompanyCode == lblCustomerIdValue.Text).FirstOrDefaultAsync();
                if (customer == null)
                {
                    MessageBox.Show("找不到此客戶，請先搜尋或新增", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                customer.CompanyFullName = txtCustomerName.Text;
                customer.DeliveryAddress = txtCustomerAddress.Text;
                customer.Phone1 = txtCustomerTel.Text;
                customer.FaxNumber = txtCustomerFax.Text;
                await _customerRepository.UpdateAsync(customer);
                MessageBox.Show("修改成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"修改失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            string customerName = Interaction.InputBox("請輸入客戶名稱", "搜尋客戶", "", -1, -1);
            if (!string.IsNullOrWhiteSpace(customerName))
            {
                await SearchAsync(customerName);
            }
        }

        private async void btnCustomerDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(lblCustomerIdValue.Text))
                {
                    MessageBox.Show("請先搜尋要刪除的客戶", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string companyName = txtCustomerName.Text;
                DialogResult result = MessageBox.Show($"確定要刪除「{companyName}」嗎？", "確認刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    var pk = await _customerRepository.Get(x => x.CompanyCode == lblCustomerIdValue.Text).Select(y => y.Id).FirstOrDefaultAsync();
                    if (pk == 0)
                    {
                        MessageBox.Show("找不到此客戶", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    await _customerRepository.DeleteAsync(pk);
                    MessageBox.Show("刪除成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"刪除失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFormClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        /// <summary>
        /// 清空表單
        /// </summary>
        private void ClearForm()
        {
            lblCustomerIdValue.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtCustomerAddress.Text = string.Empty;
            txtCustomerTel.Text = string.Empty;
            txtCustomerFax.Text = string.Empty;
        }
    }
}
