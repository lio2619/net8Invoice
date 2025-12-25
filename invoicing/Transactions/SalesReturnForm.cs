// 銷貨退回單

using invoicing.Models.DTO;
using invoicing.Service.Interface;
using System.ComponentModel;

namespace invoicing.Transactions
{
    /// <summary>
    /// 銷貨退回單表單
    /// </summary>
    public partial class SalesReturnForm : Form
    {
        private readonly IFormUIService _formUIService;
        private readonly ITransactionsService _transactionsService;

        /// <summary>
        /// 使用 BindingList 作為資料來源，支援 DataGridView 自動增行功能。
        /// </summary>
        private readonly BindingList<InvoicingDTO> _invoicingData = new();

        /// <summary>
        /// 用於取消非同步操作的 CancellationTokenSource。
        /// </summary>
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        public SalesReturnForm()
        {
            InitializeComponent();
        }

        public SalesReturnForm(IFormUIService formUIService, ITransactionsService transactionsService)
        {
            InitializeComponent();
            _formUIService = formUIService;
            _transactionsService = transactionsService;

            InitializeFormControls();
            RegisterEventHandlers();
        }

        /// <summary>
        /// 初始化表單控制項
        /// </summary>
        private void InitializeFormControls()
        {
            _formUIService.AddTextBoxUnderline(txtRemark);
            _transactionsService.InitializeDataGridView(dgvInvoicing, _invoicingData);
            _transactionsService.SetupProductCodeAutoComplete(dgvInvoicing, "貨品編號", _cancellationTokenSource.Token);
        }

        /// <summary>
        /// 註冊事件處理器
        /// </summary>
        private void RegisterEventHandlers()
        {
            // DataGridView 事件（使用服務層方法）
            dgvInvoicing.RowPostPaint += _transactionsService.HandleRowPostPaint;
            dgvInvoicing.MouseDown += (sender, e) => _transactionsService.HandleRightClickDelete(sender, e);

            // 業務邏輯事件（委派給服務層處理）
            dgvInvoicing.CellEndEdit += async (sender, e) =>
            {
                await _transactionsService.HandleCellEndEditAsync(
                    dgvInvoicing,
                    e,
                    productColumnHeaderText: "貨品編號",
                    quantityColumnHeaderText: "數量",
                    priceColumnHeaderText: "單價",
                    onTotalAmountChanged: UpdateTotalAmountLabel,
                    _cancellationTokenSource.Token);
            };

            dgvInvoicing.RowsRemoved += (sender, e) =>
            {
                _transactionsService.HandleRowsRemoved(dgvInvoicing, UpdateTotalAmountLabel);
            };

            // 表單關閉事件
            FormClosing += (sender, e) =>
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
            };
        }

        /// <summary>
        /// 更新總金額標籤
        /// </summary>
        /// <param name="total">總金額</param>
        private void UpdateTotalAmountLabel(double total)
        {
            lblAmount.Text = total.ToString("0.##");
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // 優先處理建議清單的鍵盤操作（上/下/Enter/Escape）
            if (_transactionsService?.HandleSuggestionKeyPress(keyData) == true)
            {
                return true;
            }

            // 處理 Enter 轉 Tab
            if (_transactionsService?.HandleEnterAsTab(keyData) == true)
            {
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
