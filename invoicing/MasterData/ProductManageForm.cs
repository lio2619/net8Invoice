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
            _formUIService.AddTextBoxUnderline(txtProductPriceC);
            _formUIService.AddTextBoxUnderline(txtProductCurrentCost);
            _formUIService.AddTextBoxUnderline(txtProductStandardCost);

            //售價C不要顯示
            label8.Visible = false;
            txtProductPriceC.Visible = false;
            txtProductPriceC.Text = "0";

            //訂閱事件
            //TODO 這邊有一個bug，如果先開了產品的form並且沒有關閉此form，接下來開啟其他的form也會訂閱這個東西
            _eventBus.Subscribe<MasterSelectEvent>(OnProductSelected);
        }

        private void OnProductSelected(MasterSelectEvent masterSelectEvent)
        {
            SearchAsync(masterSelectEvent.FullyName);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // 記得退訂，避免記憶體洩漏
            _eventBus.Unsubscribe<MasterSelectEvent>(OnProductSelected);
            base.OnFormClosed(e);
        }

        private async Task SearchAsync(string productCode)
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
                                                            y.PriceC,
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
                txtProductPriceC.Text = product.PriceC.ToString();
                txtProductCurrentCost.Text = product.CurrentCost.ToString();
                txtProductStandardCost.Text = product.StandardCost.ToString();
            }
            else
            {
                MessageBox.Show("請輸入正確的產品編號", "錯誤");
            }
        }

        private async void btnProductCreate_Click(object sender, EventArgs e)
        {
            Product product = new Product
            {
                ProductCode = txtProductId.Text,
                ProductName = txtProductName.Text,
                Unit = txtProductUnit.Text,
                StandardPrice = decimal.Parse(txtProductStandardPrice.Text),
                PriceA = decimal.Parse(txtProductPriceA.Text),
                PriceB = decimal.Parse(txtProductPriceB.Text),
                PriceC = decimal.Parse(txtProductPriceC.Text),
                CurrentCost = decimal.Parse(txtProductCurrentCost.Text),
                StandardCost = decimal.Parse(txtProductStandardCost.Text)
            };
            await _productRepository.AddAsync(product);
        }

        private async void btnProductModify_Click(object sender, EventArgs e)
        {
            var product = await _productRepository.Get(x => x.ProductCode == txtProductId.Text).FirstOrDefaultAsync();
            product.ProductCode = txtProductId.Text;
            product.ProductName = txtProductName.Text;
            product.Unit = txtProductUnit.Text;
            product.StandardPrice = decimal.Parse(txtProductStandardPrice.Text);
            product.PriceA = decimal.Parse(txtProductPriceA.Text);
            product.PriceB = decimal.Parse(txtProductPriceB.Text);
            product.PriceC = decimal.Parse(txtProductPriceC.Text);
            product.CurrentCost = decimal.Parse(txtProductCurrentCost.Text);
            product.StandardCost = decimal.Parse(txtProductStandardCost.Text);
            await _productRepository.UpdateAsync(product);
        }

        private async void btnProductSearch_Click(object sender, EventArgs e)
        {
            string prodcuctCode = Interaction.InputBox("請輸入產品編號", "標題", "輸入框預設內容", -1, -1);
            await SearchAsync(prodcuctCode);

        }

        private async void btnProductDelete_Click(object sender, EventArgs e)
        {
            string prodcutCode = txtProductId.Text;
            string productName = txtProductName.Text;
            DialogResult result = MessageBox.Show($"確定要刪除{productName}嗎？", "確認", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                var pk = await _productRepository.Get(x => x.ProductCode == prodcutCode).Select(y => y.Id).FirstOrDefaultAsync();
                await _productRepository.DeleteAsync(pk);
            }
        }

        private void btnFormClear_Click(object sender, EventArgs e)
        {
            txtProductId.Text = string.Empty;
            txtProductName.Text = string.Empty;
            txtProductUnit.Text = string.Empty;
            txtProductStandardPrice.Text = string.Empty;
            txtProductPriceA.Text = string.Empty;
            txtProductPriceB.Text = string.Empty;
            txtProductPriceC.Text = string.Empty;
            txtProductCurrentCost.Text = string.Empty;
            txtProductStandardCost.Text = string.Empty;
        }
    }
}
