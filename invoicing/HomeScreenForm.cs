using invoicing.Financials;
using invoicing.MasterData;
using invoicing.PlugIn;
using invoicing.Transactions;
using Microsoft.Extensions.DependencyInjection;

namespace invoicing
{
    public partial class HomeScreenForm : Form
    {
        public HomeScreenForm()
        {
            InitializeComponent();
        }

        private void CustomerMangeMenu_Click(object sender, EventArgs e)
        {
            OpenForm<CustomerManageForm>();
        }

        private void CustomerMenu_Click(object sender, EventArgs e)
        {
            OpenForm<CustomerForm>();
        }

        private void SupplierMangeMenu_Click(object sender, EventArgs e)
        {
            OpenForm<SupplierManageForm>();
        }

        private void SupplierMenu_Click(object sender, EventArgs e)
        {
            OpenForm<SupplierForm>();
        }

        private void ProductMangeMenu_Click(object sender, EventArgs e)
        {
            OpenForm<ProductManageForm>();
        }

        private void ProductMenu_Click(object sender, EventArgs e)
        {
            OpenForm<ProductForm>();
        }

        private void SalesDeliveryMenu_Click(object sender, EventArgs e)
        {
            OpenForm<SalesDeliveryForm>();
        }

        private void PurchaseReceiptMenu_Click(object sender, EventArgs e)
        {
            OpenForm<PurchaseReceiptForm>();
        }

        private void PurchaseOrderMenu_Click(object sender, EventArgs e)
        {
            OpenForm<PurchaseOrderForm>();
        }

        private void SalesOrderMenu_Click(object sender, EventArgs e)
        {
            OpenForm<SalesOrderForm>();
        }

        private void SalesReturnMenu_Click(object sender, EventArgs e)
        {
            OpenForm<SalesReturnForm>();
        }

        private void PurchaseReturnMenu_Click(object sender, EventArgs e)
        {
            OpenForm<PurchaseReturnForm>();
        }

        private void TotalPurchasesMenu_Click(object sender, EventArgs e)
        {
            OpenForm<TotalPurchasesForm>();
        }

        private void TotalSalesMenu_Click(object sender, EventArgs e)
        {
            OpenForm<TotalSalesForm>();
        }

        private void AccountsReceivableMenu_Click(object sender, EventArgs e)
        {
            OpenForm<AccountsReceivableForm>();
        }

        private void TangYiMenu_Click(object sender, EventArgs e)
        {
            OpenForm<TangYiForm>();
        }

        private void JinDaMenu_Click(object sender, EventArgs e)
        {
            OpenForm<JinDaForm>();
        }

        private void JinDaNewMenu_Click(object sender, EventArgs e)
        {
            OpenForm<JinDaNewForm>();
        }

        private void GuanmaoMenu_Click(object sender, EventArgs e)
        {
            OpenForm<GuanmaoForm>();
        }

        private void GuanmaoNewMenu_Click(object sender, EventArgs e)
        {
            OpenForm<GuanmaoNewForm>();
        }

        private void ComparePriceMenu_Click(object sender, EventArgs e)
        {
            OpenForm<ComparePriceForm>();
        }

        /// <summary>
        /// 判斷該視窗是否已經顯示在畫面上了，若沒有就呼叫該視窗，若有就不用管該請求
        /// 【修改原因】：原本直接從根容器解析 Scoped 服務，導致所有表單共用同一個 InvoicIngDbContext 實例。
        /// 當連續開啟不同表單時，非同步查詢會在同一個 DbContext 上同時執行，造成 InvalidOperationException。
        /// 改為替每個開啟的表單建立獨立的 IServiceScope，並在表單關閉時自動 Dispose 釋放資源，確保每個表單擁有獨立的 DbContext 與服務實例。
        /// </summary>
        /// <typeparam name="T">表單型別</typeparam>
        private void OpenForm<T>() where T : Form
        {
            var existingForm = MdiChildren.FirstOrDefault(f => f.GetType() == typeof(T));

            if (existingForm != null)
            {
                existingForm.Focus();
            }
            else
            {
                // 為每個表單建立專屬的服務生命週期範圍 (IServiceScope)
                var scope = Program.ServiceProvider.CreateScope();
                try
                {
                    // 從獨立的 Scope 中解析表單實例，確保其注入的 DbContext 彼此隔離
                    var newForm = scope.ServiceProvider.GetRequiredService<T>();
                    newForm.MdiParent = this;

                    // 當表單關閉時，釋放該 Scope 及其包含的 DbContext 與相關 Scoped 資源
                    newForm.FormClosed += (sender, e) =>
                    {
                        scope.Dispose();
                    };

                    newForm.Show();
                }
                catch
                {
                    // 若建立或顯示表單失敗，立即釋放該 Scope 避免記憶體洩漏
                    scope.Dispose();
                    throw;
                }
            }
        }
    }
}
