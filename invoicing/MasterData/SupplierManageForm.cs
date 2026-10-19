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

            // 訂閱廠商專屬事件，避免接收到其他領域事件干擾
            _eventBus.Subscribe<SupplierSelectEvent>(OnSupplierSelected);
        }

        private async void OnSupplierSelected(SupplierSelectEvent evt)
        {
            if (evt.Sender == this || string.IsNullOrWhiteSpace(evt.FullyName)) return;

            // 僅同步連動更新表單資料，不呼叫 BringToFront() 與 Activate() 移動至最上層或搶奪焦點，
            // 避免干擾使用者在其他作業表單（如進貨單、採購單等）上的操作流程
            await SearchAsync(evt.FullyName, notifyOtherForms: false);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // 記得退訂，避免記憶體洩漏
            _eventBus.Unsubscribe<SupplierSelectEvent>(OnSupplierSelected);
            base.OnFormClosed(e);
        }

        private async Task<bool> SearchAsync(string companyName, bool notifyOtherForms = true)
        {
            if (string.IsNullOrWhiteSpace(companyName))
            {
                MessageBox.Show("請輸入廠商名稱", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
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

                    // 若是由管理表單主動搜尋，雙向通知廠商清單表單同步定位
                    if (notifyOtherForms && !string.IsNullOrEmpty(supplier.CompanyFullName))
                    {
                        _eventBus.Publish(new SupplierSelectEvent(supplier.CompanyFullName, this));
                    }

                    return true;
                }
                else
                {
                    MessageBox.Show("找不到此廠商", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查詢廠商發生錯誤：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// 驗證必填欄位
        /// </summary>
        private bool ValidateRequiredFields()
        {
            if (string.IsNullOrWhiteSpace(txtSupplierName.Text))
            {
                MessageBox.Show("請輸入廠商名稱", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSupplierName.Focus();
                return false;
            }
            return true;
        }

        private async void btnSupplierCreate_Click(object sender, EventArgs e)
        {
            try
            {
                // 驗證必填欄位
                if (!ValidateRequiredFields()) return;

                // 檢查廠商名稱是否已存在
                var existingSupplier = await _supplierRepository.Get(x => x.CompanyFullName == txtSupplierName.Text).FirstOrDefaultAsync();
                if (existingSupplier != null)
                {
                    MessageBox.Show("此廠商名稱已存在", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSupplierName.Focus();
                    return;
                }

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

                // 顯示新廠商編號
                lblSupplierIdValue.Text = supplier.CompanyCode;
                MessageBox.Show("新增成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"新增失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSupplierModify_Click(object sender, EventArgs e)
        {
            try
            {
                // 驗證必填欄位
                if (!ValidateRequiredFields()) return;

                if (string.IsNullOrWhiteSpace(lblSupplierIdValue.Text))
                {
                    MessageBox.Show("請先搜尋要修改的廠商", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var supplier = await _supplierRepository.Get(x => x.CompanyCode == lblSupplierIdValue.Text).FirstOrDefaultAsync();
                if (supplier == null)
                {
                    MessageBox.Show("找不到此廠商，請先搜尋或新增", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                supplier.CompanyFullName = txtSupplierName.Text;
                supplier.DeliveryAddress = txtSupplierAddress.Text;
                supplier.Phone1 = txtSupplierTel.Text;
                supplier.FaxNumber = txtSupplierFax.Text;
                await _supplierRepository.UpdateAsync(supplier);
                MessageBox.Show("修改成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"修改失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSupplierSearch_Click(object sender, EventArgs e)
        {
            string supplierName = Interaction.InputBox("請輸入廠商名稱", "搜尋廠商", "", -1, -1);
            if (!string.IsNullOrWhiteSpace(supplierName))
            {
                await SearchAsync(supplierName);
            }
        }

        private async void btnSupplierDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(lblSupplierIdValue.Text))
                {
                    MessageBox.Show("請先搜尋要刪除的廠商", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string supplierName = txtSupplierName.Text;
                DialogResult result = MessageBox.Show($"確定要刪除「{supplierName}」嗎？", "確認刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    var pk = await _supplierRepository.Get(x => x.CompanyCode == lblSupplierIdValue.Text).Select(y => y.Id).FirstOrDefaultAsync();
                    if (pk == 0)
                    {
                        MessageBox.Show("找不到此廠商", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    await _supplierRepository.DeleteAsync(pk);
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
            lblSupplierIdValue.Text = string.Empty;
            txtSupplierName.Text = string.Empty;
            txtSupplierAddress.Text = string.Empty;
            txtSupplierTel.Text = string.Empty;
            txtSupplierFax.Text = string.Empty;
        }
    }
}
