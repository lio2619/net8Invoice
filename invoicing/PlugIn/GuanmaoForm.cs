//關貿

using invoicing.Service.Interface;
using System.Data;

namespace invoicing.PlugIn
{
    public partial class GuanmaoForm : Form
    {
        private readonly IPdfImportService _pdfImportService;

        public GuanmaoForm(IPdfImportService pdfImportService)
        {
            InitializeComponent();
            _pdfImportService = pdfImportService;
            
            btnLoad.Click += BtnLoad_Click;
            InitializeDataGridViewColumns();
        }

        private void InitializeDataGridViewColumns()
        {
            dgvInvoicing.Columns.Clear();
            dgvInvoicing.Columns.Add("客戶名稱", "客戶名稱");
            dgvInvoicing.Columns.Add("關貿採購單號", "關貿採購單號");
            dgvInvoicing.Columns.Add("進銷存單子編號", "進銷存單子編號");
            dgvInvoicing.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 禁用所有欄位的排序
            foreach (DataGridViewColumn column in dgvInvoicing.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        /// <summary>
        /// 讀取 PDF 檔案
        /// </summary>
        private async void BtnLoad_Click(object? sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Filter = "PDF 檔案 (*.pdf)|*.pdf"
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                var results = await _pdfImportService.ImportFromPdfAsync(dialog.FileName);

                dgvInvoicing.Rows.Clear();
                foreach (var result in results)
                {
                    dgvInvoicing.Rows.Add(
                        result.CustomerName,
                        result.PoNumber,
                        result.NewOrderNumber
                    );
                }

                MessageBox.Show($"成功匯入 {results.Count} 筆訂單", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"讀取 PDF 失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
