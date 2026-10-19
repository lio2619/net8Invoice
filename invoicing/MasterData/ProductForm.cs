//所有產品資料

using invoicing.Event;
using invoicing.Models.DTO;
using invoicing.Repository.Interface;
using invoicing.Service.Interface;

namespace invoicing.MasterData
{
    public partial class ProductForm : Form
    {
        private readonly IProductRepository _productRepository;
        private readonly IFormUIService _formUIService;
        private readonly EventBus _eventBus;
        public ProductForm()
        {
            InitializeComponent();
        }

        public ProductForm(IProductRepository productRepository, IFormUIService formUIService, EventBus eventBus)
        {
            InitializeComponent();

            _productRepository = productRepository;
            _formUIService = formUIService;
            _eventBus = eventBus;
            cboSelect.SelectedIndex = 0;

            _formUIService.AddTextBoxUnderline(txtInput);
            var productData = _productRepository.Get()
                .Select(x => new ProductDTO
                {
                    ProductCode = x.ProductCode,
                    ProductName = x.ProductName,
                    StandardPrice = x.StandardPrice,
                    PriceA = x.PriceA,
                    PriceB = x.PriceB,
                    StandardCost = x.StandardCost
                }).OrderBy(y => y.ProductCode).ToList();
            dgvProductAll.DataSource = productData;
            dgvProductAll.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvProductAll.DefaultCellStyle.Font = new Font("Microsoft JhengHei", 12);

            // 訂閱產品選取事件，支援產品管理資料或其他單據切換時之雙向定位
            _eventBus.Subscribe<ProductSelectEvent>(OnProductSelectedFromOtherForm);
        }

        /// <summary>
        /// 當其他表單（如產品管理資料或單據）切換貨品時，同步定位產品清單表格
        /// </summary>
        private void OnProductSelectedFromOtherForm(ProductSelectEvent evt)
        {
            if (evt.Sender == this || string.IsNullOrWhiteSpace(evt.ProductCode)) return;

            var row = dgvProductAll.Rows.Cast<DataGridViewRow>()
                        .Where(r => !r.IsNewRow)
                        .FirstOrDefault(r =>
                            r.Cells[nameof(ProductDTO.ProductCode)].Value != null &&
                            string.Equals(r.Cells[nameof(ProductDTO.ProductCode)].Value.ToString(), evt.ProductCode, StringComparison.OrdinalIgnoreCase));

            if (row != null)
            {
                dgvProductAll.CurrentCell = row.Cells[0];
                dgvProductAll.FirstDisplayedScrollingRowIndex = row.Index;
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // 退訂事件以防止記憶體洩漏
            _eventBus.Unsubscribe<ProductSelectEvent>(OnProductSelectedFromOtherForm);
            base.OnFormClosed(e);
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            string search = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(search))
                return;

            DataGridViewRow row;

            if (cboSelect.Text.ToString() == "貨品編號")
            {
                row = dgvProductAll.Rows.Cast<DataGridViewRow>()
                            .Where(r => !r.IsNewRow)
                            .FirstOrDefault(r =>
                                r.Cells[nameof(ProductDTO.ProductCode)].Value != null &&
                                r.Cells[nameof(ProductDTO.ProductCode)].Value.ToString()
                                  .StartsWith(search));
            }
            else
            {
                row = dgvProductAll.Rows.Cast<DataGridViewRow>()
                        .Where(r => !r.IsNewRow)
                        .FirstOrDefault(r =>
                            r.Cells[nameof(ProductDTO.ProductName)].Value != null &&
                            r.Cells[nameof(ProductDTO.ProductName)].Value.ToString()
                              .IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            }


            if (row == null)
            {
                MessageBox.Show("沒有這個商品");
                return;
            }

            dgvProductAll.CurrentCell = row.Cells[0];
            dgvProductAll.FirstDisplayedScrollingRowIndex = row.Index;
        }

        private void dgvProductAll_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string? productCode = dgvProductAll.Rows[e.RowIndex].Cells[0].Value?.ToString();

            if (string.IsNullOrEmpty(productCode))
                return;

            // 發布強型別產品選取事件（標記自身為 Sender 避免遞迴）
            _eventBus.Publish(new ProductSelectEvent(productCode, this));
        }
    }
}
