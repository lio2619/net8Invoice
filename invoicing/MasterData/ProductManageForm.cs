//管理產品資料

using invoicing.Event;
using invoicing.Models.Entity;
using invoicing.Repository.Interface;
using invoicing.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace invoicing.MasterData
{
    public partial class ProductManageForm : Form
    {
        private readonly IProductRepository _productRepository;
        private readonly IFormUIService _formUIService;
        private readonly EventBus _eventBus;
        public ProductManageForm()
        {
            InitializeComponent();
        }

        public ProductManageForm(IProductRepository productRepository, IFormUIService formUIService, EventBus eventBus)
        {
            InitializeComponent();
            _productRepository = productRepository;
            _formUIService = formUIService;
            _eventBus = eventBus;

            _formUIService.AddTextBoxUnderline(txtProductId);
            _formUIService.AddTextBoxUnderline(txtProductName);
            _formUIService.AddTextBoxUnderline(txtProductUnit);
            _formUIService.AddTextBoxUnderline(txtProductStandardPrice);
            _formUIService.AddTextBoxUnderline(txtProductPriceA);
            _formUIService.AddTextBoxUnderline(txtProductPriceB);
            _formUIService.AddTextBoxUnderline(txtProductBoxCost);
            _formUIService.AddTextBoxUnderline(txtProductCurrentCost);
            _formUIService.AddTextBoxUnderline(txtProductStandardCost);
            // 初始化清空
            btnFormClear_Click(null, null);

            // 訂閱產品專屬事件（解決舊有事件混淆 bug，不再被客戶或廠商事件干擾）
            _eventBus.Subscribe<ProductSelectEvent>(OnProductSelected);
        }

        private async void OnProductSelected(ProductSelectEvent evt)
        {
            if (evt.Sender == this || string.IsNullOrWhiteSpace(evt.ProductCode)) return;

            // 僅同步連動更新表單資料，不呼叫 BringToFront() 與 Activate() 移動至最上層或搶奪焦點，
            // 避免干擾使用者在出貨單等作業表單上的連續輸入操作
            await SearchAsync(evt.ProductCode, notifyOtherForms: false);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // 退訂事件以防止記憶體洩漏
            _eventBus.Unsubscribe<ProductSelectEvent>(OnProductSelected);
            base.OnFormClosed(e);
        }

        private async Task<bool> SearchAsync(string productCode, bool notifyOtherForms = true)
        {
            if (string.IsNullOrWhiteSpace(productCode))
            {
                MessageBox.Show("請輸入產品編號", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                var product = await _productRepository.Get(x => x.ProductCode == productCode)
                                                        .Select(
                                                            y => new
                                                            {
                                                                y.ProductName,
                                                                y.Unit,
                                                                y.StandardPrice,
                                                                y.PriceA,
                                                                y.PriceB,
                                                                y.BoxCost,
                                                                y.StandardCost,
                                                                y.CurrentCost,
                                                            })
                                                        .FirstOrDefaultAsync();
                if (product is not null)
                {
                    txtProductId.Text = productCode;
                    txtProductName.Text = product.ProductName;
                    txtProductUnit.Text = product.Unit;
                    txtProductStandardPrice.Text = product.StandardPrice.ToString();
                    txtProductPriceA.Text = product.PriceA.ToString();
                    txtProductPriceB.Text = product.PriceB.ToString();
                    txtProductBoxCost.Text = product.BoxCost.ToString();
                    txtProductStandardCost.Text = product.StandardCost.ToString();
                    txtProductCurrentCost.Text = product.CurrentCost.ToString();

                    // 若是由管理表單主動搜尋，雙向通知產品清單表單同步定位
                    if (notifyOtherForms && !string.IsNullOrEmpty(productCode))
                    {
                        _eventBus.Publish(new ProductSelectEvent(productCode, this));
                    }

                    return true;
                }
                else
                {
                    MessageBox.Show("找不到此產品編號", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查詢產品發生錯誤：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// 驗證必填欄位
        /// </summary>
        private bool ValidateRequiredFields()
        {
            if (string.IsNullOrWhiteSpace(txtProductId.Text))
            {
                MessageBox.Show("請輸入產品編號", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductId.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("請輸入產品名稱", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductName.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 安全解析數字，若解析失敗則返回預設值 0
        /// </summary>
        private decimal SafeParseDecimal(string text)
        {
            return decimal.TryParse(text, out var result) ? result : 0;
        }

        private async void btnProductCreate_Click(object sender, EventArgs e)
        {
            try
            {
                // 驗證必填欄位
                if (!ValidateRequiredFields()) return;

                // 檢查產品編號是否已存在
                var existingProduct = await _productRepository.Get(x => x.ProductCode == txtProductId.Text).FirstOrDefaultAsync();
                if (existingProduct != null)
                {
                    MessageBox.Show("此產品編號已存在，請使用其他編號", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtProductId.Focus();
                    return;
                }

                Product product = new Product
                {
                    ProductCode = txtProductId.Text,
                    ProductName = txtProductName.Text,
                    Unit = txtProductUnit.Text,
                    StandardPrice = SafeParseDecimal(txtProductStandardPrice.Text),
                    PriceA = SafeParseDecimal(txtProductPriceA.Text),
                    PriceB = SafeParseDecimal(txtProductPriceB.Text),
                    BoxCost = SafeParseDecimal(txtProductBoxCost.Text),
                    CurrentCost = SafeParseDecimal(txtProductCurrentCost.Text),
                    StandardCost = SafeParseDecimal(txtProductStandardCost.Text)
                };
                await _productRepository.AddAsync(product);
                MessageBox.Show("新增成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"新增失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnProductModify_Click(object sender, EventArgs e)
        {
            try
            {
                // 驗證必填欄位
                if (!ValidateRequiredFields()) return;

                var product = await _productRepository.Get(x => x.ProductCode == txtProductId.Text).FirstOrDefaultAsync();
                if (product == null)
                {
                    MessageBox.Show("找不到此產品，請先搜尋或新增", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                product.ProductCode = txtProductId.Text;
                product.ProductName = txtProductName.Text;
                product.Unit = txtProductUnit.Text;
                product.StandardPrice = SafeParseDecimal(txtProductStandardPrice.Text);
                product.PriceA = SafeParseDecimal(txtProductPriceA.Text);
                product.PriceB = SafeParseDecimal(txtProductPriceB.Text);
                product.BoxCost = SafeParseDecimal(txtProductBoxCost.Text);
                product.CurrentCost = SafeParseDecimal(txtProductCurrentCost.Text);
                product.StandardCost = SafeParseDecimal(txtProductStandardCost.Text);
                await _productRepository.UpdateAsync(product);
                MessageBox.Show("修改成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"修改失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnProductSearch_Click(object sender, EventArgs e)
        {
            string prodcuctCode = Interaction.InputBox("請輸入產品編號", "搜尋產品", "", -1, -1);
            if (!string.IsNullOrWhiteSpace(prodcuctCode))
            {
                await SearchAsync(prodcuctCode);
            }
        }

        private async void btnProductDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtProductId.Text))
                {
                    MessageBox.Show("請先搜尋要刪除的產品", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string prodcutCode = txtProductId.Text;
                string productName = txtProductName.Text;
                DialogResult result = MessageBox.Show($"確定要刪除「{productName}」嗎？", "確認刪除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    var pk = await _productRepository.Get(x => x.ProductCode == prodcutCode).Select(y => y.Id).FirstOrDefaultAsync();
                    if (pk == 0)
                    {
                        MessageBox.Show("找不到此產品", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    await _productRepository.DeleteAsync(pk);
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
            txtProductId.Text = string.Empty;
            txtProductName.Text = string.Empty;
            txtProductUnit.Text = string.Empty;
            txtProductStandardPrice.Text = string.Empty;
            txtProductPriceA.Text = string.Empty;
            txtProductPriceB.Text = string.Empty;
            txtProductBoxCost.Text = string.Empty;
            txtProductCurrentCost.Text = string.Empty;
            txtProductStandardCost.Text = string.Empty;
        }
    }
}
