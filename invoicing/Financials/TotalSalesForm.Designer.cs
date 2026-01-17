namespace invoicing.Financials
{
    partial class TotalSalesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            searchPanel = new TableLayoutPanel();
            lblStartText = new Label();
            lblEndText = new Label();
            dtpStart = new DateTimePicker();
            dtpEnd = new DateTimePicker();
            rightPanel = new TableLayoutPanel();
            lblTotalNumer = new Label();
            lblTotal = new Label();
            btnSearch = new Button();
            dgvTotalSales = new DataGridView();
            searchPanel.SuspendLayout();
            rightPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTotalSales).BeginInit();
            SuspendLayout();
            // 
            // searchPanel
            // 
            searchPanel.ColumnCount = 4;
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.42362F));
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.2830963F));
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5254583F));
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.09776F));
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.3217926F));
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.7556F));
            searchPanel.Controls.Add(lblStartText, 0, 0);
            searchPanel.Controls.Add(lblEndText, 2, 0);
            searchPanel.Controls.Add(dtpStart, 1, 0);
            searchPanel.Controls.Add(dtpEnd, 3, 0);
            searchPanel.Dock = DockStyle.Top;
            searchPanel.Location = new Point(0, 0);
            searchPanel.Margin = new Padding(2);
            searchPanel.Name = "searchPanel";
            searchPanel.RowCount = 1;
            searchPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            searchPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            searchPanel.Size = new Size(686, 60);
            searchPanel.TabIndex = 6;
            // 
            // lblStartText
            // 
            lblStartText.Anchor = AnchorStyles.Right;
            lblStartText.AutoSize = true;
            lblStartText.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStartText.Location = new Point(33, 20);
            lblStartText.Margin = new Padding(2, 0, 2, 0);
            lblStartText.Name = "lblStartText";
            lblStartText.Size = new Size(89, 20);
            lblStartText.TabIndex = 0;
            lblStartText.Text = "起始時間：";
            // 
            // lblEndText
            // 
            lblEndText.Anchor = AnchorStyles.Right;
            lblEndText.AutoSize = true;
            lblEndText.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEndText.Location = new Point(371, 20);
            lblEndText.Margin = new Padding(2, 0, 2, 0);
            lblEndText.Name = "lblEndText";
            lblEndText.Size = new Size(89, 20);
            lblEndText.TabIndex = 1;
            lblEndText.Text = "結束時間：";
            // 
            // dtpStart
            // 
            dtpStart.Anchor = AnchorStyles.Left;
            dtpStart.CalendarFont = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpStart.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpStart.Location = new Point(126, 16);
            dtpStart.Margin = new Padding(2);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(158, 28);
            dtpStart.TabIndex = 2;
            // 
            // dtpEnd
            // 
            dtpEnd.Anchor = AnchorStyles.Left;
            dtpEnd.CalendarFont = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpEnd.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpEnd.Location = new Point(464, 16);
            dtpEnd.Margin = new Padding(2);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(164, 28);
            dtpEnd.TabIndex = 3;
            // 
            // rightPanel
            // 
            rightPanel.ColumnCount = 2;
            rightPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48.94737F));
            rightPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 51.05263F));
            rightPanel.Controls.Add(lblTotalNumer, 1, 0);
            rightPanel.Controls.Add(lblTotal, 0, 0);
            rightPanel.Controls.Add(btnSearch, 0, 1);
            rightPanel.Dock = DockStyle.Right;
            rightPanel.Location = new Point(496, 60);
            rightPanel.Margin = new Padding(2);
            rightPanel.Name = "rightPanel";
            rightPanel.RowCount = 2;
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50.26738F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 49.73262F));
            rightPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 16F));
            rightPanel.Size = new Size(190, 295);
            rightPanel.TabIndex = 7;
            // 
            // lblTotalNumer
            // 
            lblTotalNumer.Anchor = AnchorStyles.Left;
            lblTotalNumer.AutoSize = true;
            lblTotalNumer.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTotalNumer.Location = new Point(95, 64);
            lblTotalNumer.Margin = new Padding(2, 0, 2, 0);
            lblTotalNumer.Name = "lblTotalNumer";
            lblTotalNumer.Size = new Size(18, 20);
            lblTotalNumer.TabIndex = 5;
            lblTotalNumer.Text = "0";
            // 
            // lblTotal
            // 
            lblTotal.Anchor = AnchorStyles.Right;
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTotal.Location = new Point(18, 64);
            lblTotal.Margin = new Padding(2, 0, 2, 0);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(73, 20);
            lblTotal.TabIndex = 4;
            lblTotal.Text = "總金額：";
            // 
            // btnSearch
            // 
            btnSearch.Anchor = AnchorStyles.Left;
            rightPanel.SetColumnSpan(btnSearch, 2);
            btnSearch.Font = new Font("Microsoft JhengHei UI", 12F);
            btnSearch.Location = new Point(2, 187);
            btnSearch.Margin = new Padding(2);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(186, 68);
            btnSearch.TabIndex = 6;
            btnSearch.Text = "查詢";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // dgvTotalSales
            // 
            dgvTotalSales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvTotalSales.BackgroundColor = SystemColors.Control;
            dgvTotalSales.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Microsoft JhengHei UI", 12F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dgvTotalSales.DefaultCellStyle = dataGridViewCellStyle1;
            dgvTotalSales.Dock = DockStyle.Fill;
            dgvTotalSales.Location = new Point(0, 60);
            dgvTotalSales.Margin = new Padding(2);
            dgvTotalSales.Name = "dgvTotalSales";
            dgvTotalSales.RowHeadersWidth = 51;
            dgvTotalSales.Size = new Size(496, 295);
            dgvTotalSales.TabIndex = 9;
            // 
            // TotalSalesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(686, 355);
            Controls.Add(dgvTotalSales);
            Controls.Add(rightPanel);
            Controls.Add(searchPanel);
            Margin = new Padding(2);
            Name = "TotalSalesForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "總銷貨額";
            searchPanel.ResumeLayout(false);
            searchPanel.PerformLayout();
            rightPanel.ResumeLayout(false);
            rightPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTotalSales).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel searchPanel;
        private Label lblStartText;
        private Label lblEndText;
        private DateTimePicker dtpStart;
        private DateTimePicker dtpEnd;
        private TableLayoutPanel rightPanel;
        private Label lblTotalNumer;
        private Label lblTotal;
        private Button btnSearch;
        private DataGridView dgvTotalSales;
    }
}