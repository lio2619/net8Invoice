using invoicing.MasterData;
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

        /// <summary>
        /// 判斷該視窗是否已經顯示在畫面上了，若沒有就呼叫該視窗，若有就不用管該請求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private void OpenForm<T>() where T : Form, new()
        {
            var existingForm = MdiChildren.FirstOrDefault(f => f.GetType() == typeof(T));

            if (existingForm != null)
            {
                existingForm.Focus();
            }
            else
            {
                // 使用 DI 容器來建立表單實例
                var newForm = Program.ServiceProvider.GetRequiredService<T>();
                newForm.MdiParent = this;
                newForm.Show();
            }
        }
    }
}
