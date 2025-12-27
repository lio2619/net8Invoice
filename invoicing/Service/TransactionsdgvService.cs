using invoicing.Repository.Interface;
using invoicing.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace invoicing.Service
{
    /// <summary>
    /// 交易表單 DataGridView 共用服務實作
    /// </summary>
    public class TransactionsdgvService : ITransactionsdgvService
    {
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// 防抖動延遲時間（毫秒）
        /// </summary>
        private const int DebounceDelayMs = 300;

        /// <summary>
        /// 用於追蹤防抖動操作的 CancellationTokenSource
        /// </summary>
        private CancellationTokenSource? _debounceCts;

        /// <summary>
        /// 自訂下拉選單 ListBox
        /// </summary>
        private ListBox? _suggestionListBox;

        /// <summary>
        /// 目前編輯中的 TextBox
        /// </summary>
        private TextBox? _currentTextBox;

        /// <summary>
        /// 產品建議清單（編號, 名稱）
        /// </summary>
        private List<(string ProductCode, string ProductName)> _currentSuggestions = new();

        /// <summary>
        /// 最後選擇的產品編號（用於防止選擇後重複觸發查詢）
        /// </summary>
        private string? _lastSelectedProductCode = null;

        /// <summary>
        /// 建構函式，注入產品資料庫儲存庫
        /// </summary>
        /// <param name="productRepository">產品資料庫儲存庫</param>
        public TransactionsdgvService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        #region DataGridView 使用功能
        /// <summary>
        /// 初始化 DataGridView，設定自動增行、刪除行及資料錯誤處理
        /// </summary>
        /// <typeparam name="T">資料類型</typeparam>
        /// <param name="dgv">要初始化的 DataGridView</param>
        /// <param name="dataSource">BindingList 資料來源</param>
        public void InitializeDataGridView<T>(DataGridView dgv, BindingList<T> dataSource) where T : new()
        {
            InitializeDataGridView(dgv, dataSource, allowDelete: true);
        }

        /// <summary>
        /// 初始化 DataGridView，設定自動增行及資料錯誤處理（可自訂刪除權限）
        /// </summary>
        /// <typeparam name="T">資料類型</typeparam>
        /// <param name="dgv">要初始化的 DataGridView</param>
        /// <param name="dataSource">BindingList 資料來源</param>
        /// <param name="allowDelete">是否允許使用者刪除行</param>
        public void InitializeDataGridView<T>(DataGridView dgv, BindingList<T> dataSource, bool allowDelete) where T : new()
        {
            ArgumentNullException.ThrowIfNull(dgv);
            ArgumentNullException.ThrowIfNull(dataSource);

            // 設定 DataGridView 基本屬性
            dgv.AllowUserToAddRows = true;
            dgv.AllowUserToDeleteRows = allowDelete;

            // 使用 BindingList 作為資料來源
            dgv.DataSource = dataSource;

            // 註冊事件處理器以優化使用者體驗
            dgv.DataError += HandleDataError;
        }

        /// <summary>
        /// 處理 DataGridView 的 RowPostPaint 事件，在行標題中顯示行號
        /// </summary>
        /// <param name="sender">事件來源的 DataGridView</param>
        /// <param name="e">RowPostPaint 事件參數</param>
        public void HandleRowPostPaint(object? sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (sender is not DataGridView dgv) return;

            // 計算行標題繪製區域（右對齊，留 4px 邊距）
            var rectangle = new Rectangle(
                e.RowBounds.Location.X,
                e.RowBounds.Location.Y,
                dgv.RowHeadersWidth - 4,
                e.RowBounds.Height);

            // 繪製行號（從 1 開始）
            TextRenderer.DrawText(
                e.Graphics,
                (e.RowIndex + 1).ToString(),
                dgv.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dgv.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        /// <summary>
        /// 處理 DataGridView 的右鍵刪除功能
        /// </summary>
        /// <param name="sender">事件來源的 DataGridView</param>
        /// <param name="e">滑鼠事件參數</param>
        /// <returns>刪除操作執行結果：true 表示已刪除，false 表示取消或無法刪除</returns>
        public bool HandleRightClickDelete(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return false;
            if (sender is not DataGridView dgv) return false;

            if (dgv.CurrentRow == null)
            {
                MessageBox.Show("請先選擇要刪除的資料列", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            int rowIndex = dgv.CurrentRow.Index;

            if (dgv.CurrentRow.IsNewRow)
            {
                MessageBox.Show("無法刪除新增行", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            var dialogResult = MessageBox.Show(
                "確定要刪除選取的資料列嗎？",
                "確認刪除",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dialogResult != DialogResult.Yes) return false;

            try
            {
                dgv.Rows.RemoveAt(rowIndex);
                return true;
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("無法刪除此資料列，請稍後再試", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        /// <summary>
        /// 處理 Enter 鍵轉換為 Tab 鍵的行為（用於 ProcessCmdKey）
        /// </summary>
        /// <param name="keyData">按下的按鍵</param>
        /// <returns>true 表示已處理 Enter 鍵，false 表示未處理</returns>
        public bool HandleEnterAsTab(Keys keyData)
        {
            if (keyData != Keys.Enter) return false;

            SendKeys.Send("{TAB}");
            return true;
        }

        /// <summary>
        /// 處理 DataGridView 資料錯誤，避免因資料格式問題導致例外
        /// </summary>
        private void HandleDataError(object? sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;

#if DEBUG
            System.Diagnostics.Debug.WriteLine(
                $"DataGridView 資料錯誤: Row={e.RowIndex}, Column={e.ColumnIndex}, " +
                $"Exception={e.Exception?.Message}");
#endif
        }
        #endregion

        #region 產品編號自動完成相關功能（含產品名稱顯示）
        /// <summary>
        /// 設定 DataGridView 欄位的產品編號自動完成功能（顯示產品編號與名稱）
        /// </summary>
        /// <param name="dgv">要設定的 DataGridView</param>
        /// <param name="productColumnHeaderText">產品編號欄位的標題文字</param>
        /// <param name="cancellationToken">用於取消非同步操作的 Token</param>
        public void SetupProductCodeAutoComplete(DataGridView dgv, string productColumnHeaderText, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(dgv);

            // 建立自訂下拉選單
            _suggestionListBox = new ListBox
            {
                Visible = false,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font(dgv.Font.FontFamily, 12f, dgv.Font.Style),
                IntegralHeight = false
            };

            // 將下拉選單加入表單
            var parentForm = dgv.FindForm();
            if (parentForm != null)
            {
                parentForm.Controls.Add(_suggestionListBox);
                _suggestionListBox.BringToFront();
            }

            // 下拉選單選擇事件
            _suggestionListBox.Click += SuggestionListBox_Click;
            _suggestionListBox.KeyDown += SuggestionListBox_KeyDown;

            dgv.EditingControlShowing += (sender, e) =>
            {
                if (cancellationToken.IsCancellationRequested) return;

                var currentColumnHeader = dgv.CurrentCell?.OwningColumn?.HeaderCell?.Value?.ToString();

                if (currentColumnHeader == productColumnHeaderText && e.Control is TextBox textBox)
                {
                    _currentTextBox = textBox;

                    // 禁用內建自動完成（我們使用自訂下拉選單）
                    textBox.AutoCompleteMode = AutoCompleteMode.None;

                    // 移除現有的事件處理器以避免重複註冊
                    textBox.TextChanged -= CreateProductCodeTextChangedHandler(cancellationToken);
                    textBox.TextChanged += CreateProductCodeTextChangedHandler(cancellationToken);

                    textBox.LostFocus -= TextBox_LostFocus;
                    textBox.LostFocus += TextBox_LostFocus;
                }
                else
                {
                    // 不是產品編號欄位時，清除狀態並隱藏建議清單
                    _currentTextBox = null;
                    HideSuggestionListBox();
                }
            };

            // 當編輯結束時隱藏下拉選單並清除狀態
            dgv.CellEndEdit += (sender, e) =>
            {
                _currentTextBox = null;
                HideSuggestionListBox();
            };
        }

        /// <summary>
        /// TextBox 失去焦點事件處理
        /// </summary>
        private void TextBox_LostFocus(object? sender, EventArgs e)
        {
            // 延遲隱藏，讓點擊選單項目有機會執行
            Task.Delay(150).ContinueWith(_ =>
            {
                if (_suggestionListBox?.InvokeRequired == true)
                {
                    _suggestionListBox.BeginInvoke(HideSuggestionListBox);
                }
                else
                {
                    HideSuggestionListBox();
                }
            });
        }

        /// <summary>
        /// 處理建議清單的鍵盤操作（用於 Form.ProcessCmdKey）
        /// 當建議清單可見時，處理上/下/Enter/Escape 鍵
        /// </summary>
        /// <param name="keyData">按下的按鍵</param>
        /// <returns>true 表示已處理按鍵，false 表示未處理</returns>
        public bool HandleSuggestionKeyPress(Keys keyData)
        {
            // 如果建議清單未顯示，不處理
            if (_suggestionListBox == null || !_suggestionListBox.Visible)
            {
                return false;
            }

            switch (keyData)
            {
                case Keys.Down:
                    if (_suggestionListBox.SelectedIndex < _suggestionListBox.Items.Count - 1)
                        _suggestionListBox.SelectedIndex++;
                    return true;

                case Keys.Up:
                    if (_suggestionListBox.SelectedIndex > 0)
                        _suggestionListBox.SelectedIndex--;
                    return true;

                case Keys.Enter:
                    if (_suggestionListBox.SelectedIndex >= 0)
                    {
                        SelectSuggestion();
                    }
                    return true;

                case Keys.Escape:
                    HideSuggestionListBox();
                    return true;

                default:
                    return false;
            }
        }

        /// <summary>
        /// 下拉選單點擊事件
        /// </summary>
        private void SuggestionListBox_Click(object? sender, EventArgs e)
        {
            SelectSuggestion();
        }

        /// <summary>
        /// 下拉選單按鍵事件
        /// </summary>
        private void SuggestionListBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectSuggestion();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                HideSuggestionListBox();
                e.Handled = true;
            }
        }

        /// <summary>
        /// 選擇建議項目，只帶入產品編號
        /// </summary>
        private void SelectSuggestion()
        {
            if (_suggestionListBox == null || _currentTextBox == null) return;
            if (_suggestionListBox.SelectedIndex < 0 || _suggestionListBox.SelectedIndex >= _currentSuggestions.Count) return;

            var selected = _currentSuggestions[_suggestionListBox.SelectedIndex];

            // 記錄已選擇的產品編號，防止 TextChanged 事件再次觸發查詢
            _lastSelectedProductCode = selected.ProductCode;

            _currentTextBox.Text = selected.ProductCode;
            _currentTextBox.SelectionStart = _currentTextBox.Text.Length;

            HideSuggestionListBox();
        }

        /// <summary>
        /// 隱藏下拉選單
        /// </summary>
        private void HideSuggestionListBox()
        {
            if (_suggestionListBox != null)
            {
                _suggestionListBox.Visible = false;
            }
        }

        /// <summary>
        /// 建立產品編號 TextChanged 事件處理器
        /// </summary>
        private EventHandler CreateProductCodeTextChangedHandler(CancellationToken cancellationToken)
        {
            return async (sender, e) =>
            {
                if (sender is TextBox textBox)
                {
                    await HandleProductCodeTextChangedAsync(textBox, cancellationToken);
                }
            };
        }

        /// <summary>
        /// 處理產品編號欄位的 TextChanged 事件，包含防抖動機制
        /// </summary>
        /// <param name="textBox">觸發事件的 TextBox</param>
        /// <param name="cancellationToken">用於取消非同步操作的 Token</param>
        public async Task HandleProductCodeTextChangedAsync(TextBox textBox, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) return;

            // 確認是目前正在編輯的產品編號欄位，否則不處理
            // 這可以防止 DataGridView 重複使用 TextBox 時在其他欄位觸發查詢
            if (textBox != _currentTextBox || _currentTextBox == null)
            {
                return;
            }

            string text = textBox.Text;
            if (string.IsNullOrEmpty(text))
            {
                _lastSelectedProductCode = null;
                HideSuggestionListBox();
                return;
            }

            // 如果目前文字與最後選擇的產品編號相同，不需要再查詢
            if (_lastSelectedProductCode != null && text == _lastSelectedProductCode)
            {
                return;
            }

            // 使用者修改了文字，清除最後選擇的記錄
            _lastSelectedProductCode = null;

            // 取消先前的防抖動操作
            _debounceCts?.Cancel();
            _debounceCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            try
            {
                await Task.Delay(DebounceDelayMs, _debounceCts.Token);

                if (_debounceCts.Token.IsCancellationRequested) return;
                if (textBox.IsDisposed || !textBox.IsHandleCreated) return;

                // 從資料庫查詢符合前綴的產品編號與名稱
                var suggestions = await _productRepository.GetProductCodesWithNameByPrefixAsync(text);

                if (textBox.IsDisposed || !textBox.IsHandleCreated) return;
                if (_debounceCts.Token.IsCancellationRequested) return;

                if (suggestions.Any())
                {
                    _currentSuggestions = suggestions;
                    ShowSuggestionListBox(textBox, suggestions);
                }
                else
                {
                    HideSuggestionListBox();
                }
            }
            catch (OperationCanceledException)
            {
                // 操作被取消是預期的行為
            }
            catch (ObjectDisposedException)
            {
                // 控制項已被釋放
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"產品編號自動完成錯誤: {ex.Message}");
            }
        }

        /// <summary>
        /// 顯示自訂下拉選單
        /// </summary>
        /// <param name="textBox">觸發的 TextBox</param>
        /// <param name="suggestions">建議清單</param>
        private void ShowSuggestionListBox(TextBox textBox, List<(string ProductCode, string ProductName)> suggestions)
        {
            if (_suggestionListBox == null) return;

            try
            {
                // 清空並填入新項目
                _suggestionListBox.Items.Clear();

                // 計算需要的最大寬度
                int maxWidth = 200;
                using (var g = _suggestionListBox.CreateGraphics())
                {
                    foreach (var (code, name) in suggestions)
                    {
                        string displayText = $"{code} - {name}";
                        _suggestionListBox.Items.Add(displayText);

                        var textSize = g.MeasureString(displayText, _suggestionListBox.Font);
                        maxWidth = Math.Max(maxWidth, (int)textSize.Width + 20);
                    }
                }

                // 設定下拉選單位置（在 TextBox 下方）
                var textBoxScreenLocation = textBox.PointToScreen(Point.Empty);
                var parentForm = _suggestionListBox.FindForm();
                if (parentForm != null)
                {
                    var locationOnForm = parentForm.PointToClient(textBoxScreenLocation);
                    _suggestionListBox.Location = new Point(locationOnForm.X, locationOnForm.Y + textBox.Height);
                }

                // 設定下拉選單大小
                _suggestionListBox.Width = Math.Max(maxWidth, textBox.Width);
                _suggestionListBox.Height = Math.Min(suggestions.Count * _suggestionListBox.ItemHeight + 4, 200);

                _suggestionListBox.SelectedIndex = 0;
                _suggestionListBox.Visible = true;
                _suggestionListBox.BringToFront();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"顯示下拉選單錯誤: {ex.Message}");
            }
        }

        /// <summary>
        /// 根據產品編號取得產品資訊並填入 DataGridView 行
        /// </summary>
        /// <param name="row">要填入資料的 DataGridView 行</param>
        /// <param name="productCode">產品編號</param>
        /// <returns>若找到產品回傳 true，否則回傳 false</returns>
        public async Task<bool> FetchProductInfoAsync(DataGridViewRow row, string productCode)
        {
            ArgumentNullException.ThrowIfNull(row);

            if (string.IsNullOrWhiteSpace(productCode)) return false;

            try
            {
                var product = await _productRepository.Get(p => p.ProductCode == productCode).FirstOrDefaultAsync();

                if (product != null)
                {
                    row.Cells["ProductName"].Value = product.ProductName;
                    row.Cells["Unit"].Value = product.Unit;
                    row.Cells["UnitPrice"].Value = product.StandardPrice?.ToString("0.##") ?? "0";
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"取得產品資訊時發生錯誤: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 計算單行金額（數量 × 單價）
        /// </summary>
        /// <param name="row">要計算的 DataGridView 行</param>
        public void CalculateRowAmount(DataGridViewRow row)
        {
            ArgumentNullException.ThrowIfNull(row);

            if (double.TryParse(row.Cells["Quantity"]?.Value?.ToString(), out double quantity) &&
                double.TryParse(row.Cells["UnitPrice"]?.Value?.ToString(), out double price))
            {
                double amount = quantity * price;
                row.Cells["Amount"].Value = amount.ToString("0.##");
            }
            else
            {
                row.Cells["Amount"].Value = "0";
            }
        }

        /// <summary>
        /// 計算所有行的總金額
        /// </summary>
        /// <param name="dgv">要計算的 DataGridView</param>
        /// <returns>總金額</returns>
        public double CalculateTotalAmount(DataGridView dgv)
        {
            ArgumentNullException.ThrowIfNull(dgv);

            double total = 0;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;

                if (double.TryParse(row.Cells["Amount"]?.Value?.ToString(), out double amount))
                {
                    total += amount;
                }
            }

            return total;
        }
        #endregion

        #region 表單事件處理
        /// <summary>
        /// 處理 DataGridView 的 CellEndEdit 事件
        /// </summary>
        public async Task HandleCellEndEditAsync(
            DataGridView dgv,
            DataGridViewCellEventArgs e,
            string productColumnHeaderText,
            string quantityColumnHeaderText,
            string priceColumnHeaderText,
            Action<double> onTotalAmountChanged,
            CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) return;

            var row = dgv.Rows[e.RowIndex];
            string columnName = dgv.Columns[e.ColumnIndex].HeaderCell.Value?.ToString() ?? "";

            if (columnName == productColumnHeaderText)
            {
                string? productCode = row.Cells[e.ColumnIndex].Value?.ToString();
                if (!string.IsNullOrWhiteSpace(productCode))
                {
                    bool found = await FetchProductInfoAsync(row, productCode);
                    if (!found)
                    {
                        MessageBox.Show("請輸入正確的編號", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (columnName == quantityColumnHeaderText || columnName == priceColumnHeaderText)
            {
                CalculateRowAmount(row);
                double total = CalculateTotalAmount(dgv);
                onTotalAmountChanged?.Invoke(total);
            }
        }

        /// <summary>
        /// 處理 DataGridView 的 RowsRemoved 事件
        /// </summary>
        public void HandleRowsRemoved(DataGridView dgv, Action<double> onTotalAmountChanged)
        {
            double total = CalculateTotalAmount(dgv);
            onTotalAmountChanged?.Invoke(total);
        }
        #endregion
    }
}
