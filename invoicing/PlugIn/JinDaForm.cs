//金大

using invoicing.Event;
using invoicing.Models.DTO;
using invoicing.Service.Interface;
using System.Data;

namespace invoicing.PlugIn
{
    public partial class JinDaForm : Form
    {
        private readonly IPluginFormService _pluginFormService;
        private readonly IExcelImportService _excelImportService;
        private readonly IPrintService _printService;
        private readonly EventBus _eventBus;

        private bool _isSaved = false;
        private DataTable _sourceTable = new DataTable();
        private string _currentOrderNumber = string.Empty;

        public JinDaForm(
            IPluginFormService pluginFormService, 
            IExcelImportService excelImportService,
            IPrintService printService,
            EventBus eventBus)
        {
            InitializeComponent();
            _pluginFormService = pluginFormService;
            _excelImportService = excelImportService;
            _printService = printService;
            _eventBus = eventBus;

            InitializeFormAsync();
            RegisterEvents();
        }

        private async void InitializeFormAsync()
        {
            lblNumber.Visible = false;
            lblPageNumberText.Visible = false;

            // 載入客戶清單
            var customers = await _pluginFormService.LoadCustomersAsync();
            cboCustomer.Items.AddRange(customers.ToArray());

            // 初始化來源資料表
            _sourceTable = _pluginFormService.CreateSourceDataTable();

            // 初始化 DataGridView 欄位
            InitializeDataGridViewColumns();
        }

        private void InitializeDataGridViewColumns()
        {
            dgvInvoicing.Columns.Clear();
            dgvInvoicing.Columns.Add("貨品編號", "貨品編號");
            dgvInvoicing.Columns.Add("品名", "品名");
            dgvInvoicing.Columns.Add("數量", "數量");
            dgvInvoicing.Columns.Add("基本單位", "基本單位");
            dgvInvoicing.Columns.Add("單價", "單價");
            dgvInvoicing.Columns.Add("金額", "金額");
            dgvInvoicing.Columns.Add("備註", "備註");
        }

        private void RegisterEvents()
        {
            btnLoad.Click += BtnLoad_Click;
            btnSave.Click += BtnSave_Click;
            btnPrint.Click += BtnPrint_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnCreateExcel.Click += BtnCreateExcel_Click;
            dgvInvoicing.RowPostPaint += DgvInvoicing_RowPostPaint;
            dgvInvoicing.CellMouseDown += DgvInvoicing_CellMouseDown;
            dgvInvoicing.CellEndEdit += DgvInvoicing_CellEndEdit;
        }

        #region 按鈕事件

        /// <summary>
        /// 讀取 Excel 檔案
        /// </summary>
        private async void BtnLoad_Click(object? sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Filter = "Excel 檔案 (*.xls)|*.xls"
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                var config = ExcelImportConfig.JinDaConfig;
                var importedData = await _excelImportService.ImportFromExcelAsync(
                    dialog.FileName, config, _pluginFormService);

                // 填充 DataGridView
                dgvInvoicing.Rows.Clear();
                foreach (var detail in importedData.Details)
                {
                    dgvInvoicing.Rows.Add(
                        detail.ProductCode,
                        detail.ProductName,
                        detail.Quantity,
                        detail.Unit,
                        detail.UnitPrice,
                        detail.Amount,
                        detail.Remark
                    );
                }

                _sourceTable = importedData.SourceTable;
                lblAmount.Text = importedData.TotalAmount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 儲存訂單
        /// </summary>
        private async void BtnSave_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboCustomer.Text))
            {
                MessageBox.Show("請選擇客戶", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_isSaved)
            {
                MessageBox.Show("已經儲存過", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var orderNumber = await _pluginFormService.SaveOrderAsync(
                    dtpDate.Value,
                    cboCustomer.Text,
                    txtRemark.Text,
                    lblAmount.Text,
                    _sourceTable
                );

                _currentOrderNumber = orderNumber;
                _isSaved = true;

                lblNumber.Text = orderNumber;
                lblNumber.Visible = true;
                lblPageNumberText.Visible = true;

                MessageBox.Show("儲存完成", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"儲存失敗：{ex.Message}", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 列印預覽
        /// </summary>
        private async void BtnPrint_Click(object? sender, EventArgs e)
        {
            if (!_isSaved)
            {
                MessageBox.Show("請先儲存檔案", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var customerInfo = await _pluginFormService.GetCustomerInfoAsync(cboCustomer.Text);
            if (customerInfo == null)
            {
                MessageBox.Show("請輸入正確的客戶名稱", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var orderNumber = await _pluginFormService.GetOrderNumberAsync(
                dtpDate.Value, cboCustomer.Text, txtRemark.Text, lblAmount.Text);

            if (!orderNumber.HasValue)
            {
                MessageBox.Show("請先儲存檔案", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var details = new List<InvoicingDetailDTO>();
            for (int i = 0; i < dgvInvoicing.RowCount - 1; i++)
            {
                details.Add(new InvoicingDetailDTO
                {
                    ProductCode = dgvInvoicing.Rows[i].Cells[0].Value?.ToString() ?? "",
                    ProductName = dgvInvoicing.Rows[i].Cells[1].Value?.ToString() ?? "",
                    Quantity = dgvInvoicing.Rows[i].Cells[2].Value?.ToString() ?? "",
                    Unit = dgvInvoicing.Rows[i].Cells[3].Value?.ToString() ?? "",
                    UnitPrice = dgvInvoicing.Rows[i].Cells[4].Value?.ToString() ?? "",
                    Amount = dgvInvoicing.Rows[i].Cells[5].Value?.ToString() ?? "",
                    Remark = dgvInvoicing.Rows[i].Cells[6].Value?.ToString() ?? ""
                });
            }

            var printRequest = new PrintInvoiceRequest
            {
                OrderType = "出貨單",
                CustomerName = cboCustomer.Text,
                Date = dtpDate.Text,
                OrderNumber = $"{dtpDate.Value:yyyyMMdd}{orderNumber.Value:000}",
                Phone = customerInfo.Phone,
                Fax = customerInfo.Fax,
                Address = customerInfo.Address,
                Details = details,
                Remark = txtRemark.Text,
                TotalAmount = lblAmount.Text
            };

            var pdfBytes = _printService.GenerateInvoicePdf(printRequest);
            _printService.ShowPrintPreviewAndPrint(pdfBytes);
        }

        /// <summary>
        /// 刷新表單
        /// </summary>
        private void BtnRefresh_Click(object? sender, EventArgs e)
        {
            var result = MessageBox.Show("確定刷新？", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            cboCustomer.Text = "";
            dgvInvoicing.Rows.Clear();
            lblAmount.Text = "0";
            txtRemark.Text = "";
            _isSaved = false;
            _sourceTable.Rows.Clear();
            _sourceTable.Columns.Clear();
            _sourceTable = _pluginFormService.CreateSourceDataTable();
            lblNumber.Visible = false;
            lblPageNumberText.Visible = false;
        }

        /// <summary>
        /// 建立 Excel
        /// </summary>
        private async void BtnCreateExcel_Click(object? sender, EventArgs e)
        {
            if (!_isSaved)
            {
                MessageBox.Show("請先儲存檔案", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var customerInfo = await _pluginFormService.GetCustomerInfoAsync(cboCustomer.Text);
            var orderNumber = await _pluginFormService.GetOrderNumberAsync(
                dtpDate.Value, cboCustomer.Text, txtRemark.Text, lblAmount.Text);

            if (!orderNumber.HasValue)
            {
                MessageBox.Show("找不到訂單編號", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var details = new List<InvoicingDetailDTO>();
            for (int i = 0; i < dgvInvoicing.RowCount - 1; i++)
            {
                details.Add(new InvoicingDetailDTO
                {
                    ProductCode = dgvInvoicing.Rows[i].Cells[0].Value?.ToString() ?? "",
                    ProductName = dgvInvoicing.Rows[i].Cells[1].Value?.ToString() ?? "",
                    Quantity = dgvInvoicing.Rows[i].Cells[2].Value?.ToString() ?? "",
                    Unit = dgvInvoicing.Rows[i].Cells[3].Value?.ToString() ?? "",
                    UnitPrice = dgvInvoicing.Rows[i].Cells[4].Value?.ToString() ?? "",
                    Amount = dgvInvoicing.Rows[i].Cells[5].Value?.ToString() ?? "",
                    Remark = dgvInvoicing.Rows[i].Cells[6].Value?.ToString() ?? ""
                });
            }

            var request = new CreateExcelRequest
            {
                Date = dtpDate.Value,
                OrderType = "出貨單",
                CustomerName = cboCustomer.Text,
                OrderNumber = orderNumber.Value.ToString("000"),
                Details = details,
                Remark = txtRemark.Text,
                TotalAmount = lblAmount.Text
            };

            // 建立目錄
            var date = dtpDate.Value.ToString("yyyyMM");
            var path = $"../單子/{date}/出貨單/";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // 使用 EPPlus 建立 Excel
            await CreateExcelFileAsync(request, path, customerInfo?.Phone ?? "", customerInfo?.Fax ?? "");
        }

        private async Task CreateExcelFileAsync(CreateExcelRequest request, string path, string phone, string fax)
        {
            var fileName = $"出貨單_{request.Date:yyyy-MM-dd}_{request.CustomerName}_{request.OrderNumber}.xlsx";
            var filePath = Path.Combine(path, fileName);

            try
            {
                OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using var excel = new OfficeOpenXml.ExcelPackage(new FileInfo(filePath));
                var ws = excel.Workbook.Worksheets.Add("MySheet");

                ws.Cells[1, 1].Value = "出貨單";
                ws.Cells[1, 7].Value = $"貨單日期：{request.Date:yyyyMMdd}";
                ws.Cells[2, 1].Value = "客戶名稱：";
                ws.Cells[2, 2].Value = request.CustomerName;
                ws.Cells[2, 7].Value = $"貨單編號：{request.Date:yyyyMMdd}{request.OrderNumber}";
                ws.Cells[3, 1].Value = $"連絡電話：{phone}";
                ws.Cells[3, 4].Value = $"傳真號碼：{fax}";
                ws.Cells[4, 1].Value = "編號";
                ws.Cells[4, 2].Value = "品名";
                ws.Cells[4, 5].Value = "數量";
                ws.Cells[4, 6].Value = "單位";
                ws.Cells[4, 7].Value = "單價";
                ws.Cells[4, 8].Value = "金額";
                ws.Cells[4, 9].Value = "備註";

                int[] cell = { 1, 2, 5, 6, 7, 8, 9 };
                int row = 5;

                foreach (var detail in request.Details)
                {
                    ws.Cells[row, cell[0]].Value = detail.ProductCode;
                    ws.Cells[row, cell[1]].Value = detail.ProductName;
                    ws.Cells[row, cell[2]].Value = detail.Quantity;
                    ws.Cells[row, cell[3]].Value = detail.Unit;
                    ws.Cells[row, cell[4]].Value = detail.UnitPrice;
                    ws.Cells[row, cell[5]].Value = detail.Amount;
                    ws.Cells[row, cell[6]].Value = detail.Remark;
                    row++;
                }

                ws.Cells[row, 1].Value = $"備註：{request.Remark}";
                ws.Cells[row, 8].Value = $"總計：{request.TotalAmount}";

                await excel.SaveAsync();
                MessageBox.Show("完成", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("已經有這個檔案了", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region DataGridView 事件

        /// <summary>
        /// 繪製行號
        /// </summary>
        private void DgvInvoicing_RowPostPaint(object? sender, DataGridViewRowPostPaintEventArgs e)
        {
            var rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y,
                dgvInvoicing.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dgvInvoicing.RowHeadersDefaultCellStyle.Font, rectangle,
                dgvInvoicing.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        /// <summary>
        /// 右鍵刪除
        /// </summary>
        private void DgvInvoicing_CellMouseDown(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            var result = MessageBox.Show("是否刪除？", "詢問", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            try
            {
                var rowIndex = dgvInvoicing.CurrentRow?.Index ?? -1;
                if (rowIndex < 0) return;

                // 重新計算總金額
                double total = 0;
                for (int i = 0; i < dgvInvoicing.RowCount - 1; i++)
                {
                    if (i == rowIndex) continue;
                    if (double.TryParse(dgvInvoicing.Rows[i].Cells["金額"].Value?.ToString(), out var amount))
                    {
                        total += amount;
                    }
                }
                lblAmount.Text = total.ToString();

                dgvInvoicing.Rows.RemoveAt(rowIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 儲存格編輯完成後計算金額
        /// </summary>
        private void DgvInvoicing_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            // 當編輯單價欄位 (ColumnIndex == 4) 時重新計算
            if (e.ColumnIndex == 4)
            {
                try
                {
                    var unitPrice = double.Parse(dgvInvoicing.Rows[e.RowIndex].Cells[4].Value?.ToString() ?? "0");
                    var quantity = double.Parse(dgvInvoicing.Rows[e.RowIndex].Cells[2].Value?.ToString() ?? "0");

                    if (unitPrice <= 0 || quantity <= 0)
                    {
                        MessageBox.Show("請輸入數字", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var oldAmount = double.Parse(dgvInvoicing.Rows[e.RowIndex].Cells[5].Value?.ToString() ?? "0");
                    var newAmount = unitPrice * quantity;
                    dgvInvoicing.Rows[e.RowIndex].Cells[5].Value = newAmount;

                    var currentTotal = double.Parse(lblAmount.Text);
                    lblAmount.Text = (currentTotal - oldAmount + newAmount).ToString();
                }
                catch (FormatException)
                {
                    MessageBox.Show("請輸入數量", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvInvoicing_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string? productCode = dgvInvoicing.Rows[e.RowIndex].Cells[0].Value?.ToString();

            if (string.IsNullOrEmpty(productCode))
                return;

            // 觸發事件
            _eventBus.Publish(new MasterSelectEvent(productCode));
        }

        #endregion

        /// <summary>
        /// Enter 轉 Tab
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
