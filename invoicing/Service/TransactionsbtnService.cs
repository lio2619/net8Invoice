using invoicing.ReadForm;
using invoicing.Service.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace invoicing.Service
{
    /// <summary>
    /// 交易表單按鈕服務
    /// 提供多個交易表單共用的按鈕功能實作
    /// </summary>
    public class TransactionsbtnService : ITransactionsbtnService
    {
        private readonly IServiceProvider _serviceProvider;

        public TransactionsbtnService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc/>
        public void OpenReadInvoicesForm(string callerFormType, string callerOrderName = "")
        {
            try
            {
                // 使用 DI 取得 ReadInvoicesForm 實例
                var readForm = _serviceProvider.GetRequiredService<ReadInvoicesForm>();
                readForm.CallerFormType = callerFormType;
                readForm.CallerOrderName = callerOrderName;
                readForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"開啟讀檔視窗失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
