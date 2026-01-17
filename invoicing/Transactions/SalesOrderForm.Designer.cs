namespace invoicing.Transactions
{
    partial class SalesOrderForm
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
            dgvInvoicing = new DataGridView();
            btnCreateExcel = new Button();
            btnDelete = new Button();
            btnLoad = new Button();
            btnRefresh = new Button();
            btnSave = new Button();
            mainPanel = new Panel();
            buutonPanel = new TableLayoutPanel();
            btnPrint = new Button();
            txtRemark = new TextBox();
            lblRemark = new Label();
            lblTotlaAmountText = new Label();
            lblAmount = new Label();
            lblDate = new Label();
            lblCustomer = new Label();
            lblPageNumberText = new Label();
            lblNumber = new Label();
            dtpDate = new DateTimePicker();
            cboCustomer = new ComboBox();
            downAmountPanel = new TableLayoutPanel();
            UpIDNamePanel = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)dgvInvoicing).BeginInit();
            mainPanel.SuspendLayout();
            buutonPanel.SuspendLayout();
            downAmountPanel.SuspendLayout();
            UpIDNamePanel.SuspendLayout();
            SuspendLayout();
            // 
            // dgvInvoicing
            // 
            dgvInvoicing.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvInvoicing.BackgroundColor = SystemColors.Control;
            dgvInvoicing.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Microsoft JhengHei UI", 12F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dgvInvoicing.DefaultCellStyle = dataGridViewCellStyle1;
            dgvInvoicing.Dock = DockStyle.Fill;
            dgvInvoicing.Location = new Point(0, 0);
            dgvInvoicing.Margin = new Padding(2, 2, 2, 2);
            dgvInvoicing.Name = "dgvInvoicing";
            dgvInvoicing.RowHeadersWidth = 51;
            dgvInvoicing.Size = new Size(795, 432);
            dgvInvoicing.TabIndex = 1;
            // 
            // btnCreateExcel
            // 
            btnCreateExcel.Anchor = AnchorStyles.Left;
            btnCreateExcel.Font = new Font("Microsoft JhengHei UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCreateExcel.Location = new Point(2, 369);
            btnCreateExcel.Margin = new Padding(2, 2, 2, 2);
            btnCreateExcel.Name = "btnCreateExcel";
            btnCreateExcel.Size = new Size(120, 46);
            btnCreateExcel.TabIndex = 4;
            btnCreateExcel.Text = "創建Excel";
            btnCreateExcel.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Left;
            btnDelete.Font = new Font("Microsoft JhengHei UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDelete.Location = new Point(2, 295);
            btnDelete.Margin = new Padding(2, 2, 2, 2);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(120, 46);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "刪單";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnLoad
            // 
            btnLoad.Anchor = AnchorStyles.Left;
            btnLoad.Font = new Font("Microsoft JhengHei UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLoad.Location = new Point(2, 226);
            btnLoad.Margin = new Padding(2, 2, 2, 2);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(120, 46);
            btnLoad.TabIndex = 2;
            btnLoad.Text = "讀檔";
            btnLoad.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Left;
            btnRefresh.Font = new Font("Microsoft JhengHei UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRefresh.Location = new Point(2, 157);
            btnRefresh.Margin = new Padding(2, 2, 2, 2);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(120, 46);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "刷新";
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Left;
            btnSave.Font = new Font("Microsoft JhengHei UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSave.Location = new Point(2, 88);
            btnSave.Margin = new Padding(2, 2, 2, 2);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 46);
            btnSave.TabIndex = 0;
            btnSave.Text = "儲存";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // mainPanel
            // 
            mainPanel.Controls.Add(dgvInvoicing);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(0, 99);
            mainPanel.Margin = new Padding(2, 2, 2, 2);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(795, 432);
            mainPanel.TabIndex = 7;
            // 
            // buutonPanel
            // 
            buutonPanel.ColumnCount = 1;
            buutonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            buutonPanel.Controls.Add(btnPrint, 0, 0);
            buutonPanel.Controls.Add(btnCreateExcel, 0, 5);
            buutonPanel.Controls.Add(btnDelete, 0, 4);
            buutonPanel.Controls.Add(btnLoad, 0, 3);
            buutonPanel.Controls.Add(btnRefresh, 0, 2);
            buutonPanel.Controls.Add(btnSave, 0, 1);
            buutonPanel.Dock = DockStyle.Right;
            buutonPanel.Location = new Point(795, 99);
            buutonPanel.Margin = new Padding(2, 2, 2, 2);
            buutonPanel.Name = "buutonPanel";
            buutonPanel.RowCount = 6;
            buutonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 18F));
            buutonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16F));
            buutonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16F));
            buutonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16F));
            buutonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16F));
            buutonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 18F));
            buutonPanel.Size = new Size(124, 432);
            buutonPanel.TabIndex = 6;
            // 
            // btnPrint
            // 
            btnPrint.Anchor = AnchorStyles.Left;
            btnPrint.Font = new Font("Microsoft JhengHei UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPrint.Location = new Point(2, 15);
            btnPrint.Margin = new Padding(2, 2, 2, 2);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(120, 46);
            btnPrint.TabIndex = 5;
            btnPrint.Text = "預覽列印";
            btnPrint.UseVisualStyleBackColor = true;
            // 
            // txtRemark
            // 
            txtRemark.Anchor = AnchorStyles.Left;
            txtRemark.BackColor = SystemColors.Control;
            txtRemark.BorderStyle = BorderStyle.None;
            txtRemark.Font = new Font("Microsoft JhengHei UI", 14F);
            txtRemark.Location = new Point(139, 19);
            txtRemark.Margin = new Padding(2, 2, 2, 2);
            txtRemark.Name = "txtRemark";
            txtRemark.Size = new Size(317, 24);
            txtRemark.TabIndex = 6;
            // 
            // lblRemark
            // 
            lblRemark.Anchor = AnchorStyles.Right;
            lblRemark.AutoSize = true;
            lblRemark.Font = new Font("Microsoft JhengHei UI", 14F);
            lblRemark.Location = new Point(68, 19);
            lblRemark.Margin = new Padding(2, 0, 2, 0);
            lblRemark.Name = "lblRemark";
            lblRemark.Size = new Size(67, 24);
            lblRemark.TabIndex = 3;
            lblRemark.Text = "備註：";
            // 
            // lblTotlaAmountText
            // 
            lblTotlaAmountText.Anchor = AnchorStyles.Right;
            lblTotlaAmountText.AutoSize = true;
            lblTotlaAmountText.Font = new Font("Microsoft JhengHei UI", 14F);
            lblTotlaAmountText.Location = new Point(526, 19);
            lblTotlaAmountText.Margin = new Padding(2, 0, 2, 0);
            lblTotlaAmountText.Name = "lblTotlaAmountText";
            lblTotlaAmountText.Size = new Size(67, 24);
            lblTotlaAmountText.TabIndex = 4;
            lblTotlaAmountText.Text = "總計：";
            // 
            // lblAmount
            // 
            lblAmount.Anchor = AnchorStyles.Left;
            lblAmount.AutoSize = true;
            lblAmount.Font = new Font("Microsoft JhengHei UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAmount.Location = new Point(597, 19);
            lblAmount.Margin = new Padding(2, 0, 2, 0);
            lblAmount.Name = "lblAmount";
            lblAmount.Size = new Size(21, 24);
            lblAmount.TabIndex = 5;
            lblAmount.Text = "0";
            // 
            // lblDate
            // 
            lblDate.Anchor = AnchorStyles.Right;
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Microsoft JhengHei UI", 14F);
            lblDate.Location = new Point(68, 12);
            lblDate.Margin = new Padding(2, 0, 2, 0);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(67, 24);
            lblDate.TabIndex = 1;
            lblDate.Text = "日期：";
            // 
            // lblCustomer
            // 
            lblCustomer.Anchor = AnchorStyles.Right;
            lblCustomer.AutoSize = true;
            lblCustomer.Font = new Font("Microsoft JhengHei UI", 14F);
            lblCustomer.Location = new Point(68, 62);
            lblCustomer.Margin = new Padding(2, 0, 2, 0);
            lblCustomer.Name = "lblCustomer";
            lblCustomer.Size = new Size(67, 24);
            lblCustomer.TabIndex = 2;
            lblCustomer.Text = "客戶：";
            // 
            // lblPageNumberText
            // 
            lblPageNumberText.Anchor = AnchorStyles.Right;
            lblPageNumberText.AutoSize = true;
            lblPageNumberText.Font = new Font("Microsoft JhengHei UI", 14F);
            lblPageNumberText.Location = new Point(488, 62);
            lblPageNumberText.Margin = new Padding(2, 0, 2, 0);
            lblPageNumberText.Name = "lblPageNumberText";
            lblPageNumberText.Size = new Size(105, 24);
            lblPageNumberText.TabIndex = 3;
            lblPageNumberText.Text = "單子編號：";
            // 
            // lblNumber
            // 
            lblNumber.Anchor = AnchorStyles.Left;
            lblNumber.AutoSize = true;
            lblNumber.Font = new Font("Microsoft JhengHei UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNumber.Location = new Point(597, 62);
            lblNumber.Margin = new Padding(2, 0, 2, 0);
            lblNumber.Name = "lblNumber";
            lblNumber.Size = new Size(21, 24);
            lblNumber.TabIndex = 4;
            lblNumber.Text = "0";
            // 
            // dtpDate
            // 
            dtpDate.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtpDate.CalendarFont = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpDate.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpDate.Location = new Point(139, 10);
            dtpDate.Margin = new Padding(2, 2, 2, 2);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(317, 28);
            dtpDate.TabIndex = 5;
            // 
            // cboCustomer
            // 
            cboCustomer.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cboCustomer.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboCustomer.FormattingEnabled = true;
            cboCustomer.Location = new Point(139, 60);
            cboCustomer.Margin = new Padding(2, 2, 2, 2);
            cboCustomer.Name = "cboCustomer";
            cboCustomer.Size = new Size(317, 28);
            cboCustomer.TabIndex = 6;
            // 
            // downAmountPanel
            // 
            downAmountPanel.ColumnCount = 4;
            downAmountPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            downAmountPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            downAmountPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            downAmountPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            downAmountPanel.Controls.Add(txtRemark, 1, 0);
            downAmountPanel.Controls.Add(lblRemark, 0, 0);
            downAmountPanel.Controls.Add(lblTotlaAmountText, 2, 0);
            downAmountPanel.Controls.Add(lblAmount, 3, 0);
            downAmountPanel.Dock = DockStyle.Bottom;
            downAmountPanel.Location = new Point(0, 531);
            downAmountPanel.Margin = new Padding(2, 2, 2, 2);
            downAmountPanel.Name = "downAmountPanel";
            downAmountPanel.RowCount = 1;
            downAmountPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            downAmountPanel.Size = new Size(919, 63);
            downAmountPanel.TabIndex = 5;
            // 
            // UpIDNamePanel
            // 
            UpIDNamePanel.ColumnCount = 4;
            UpIDNamePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            UpIDNamePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            UpIDNamePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            UpIDNamePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            UpIDNamePanel.Controls.Add(lblDate, 0, 0);
            UpIDNamePanel.Controls.Add(lblCustomer, 0, 1);
            UpIDNamePanel.Controls.Add(lblPageNumberText, 2, 1);
            UpIDNamePanel.Controls.Add(lblNumber, 3, 1);
            UpIDNamePanel.Controls.Add(dtpDate, 1, 0);
            UpIDNamePanel.Controls.Add(cboCustomer, 1, 1);
            UpIDNamePanel.Dock = DockStyle.Top;
            UpIDNamePanel.Location = new Point(0, 0);
            UpIDNamePanel.Margin = new Padding(2, 2, 2, 2);
            UpIDNamePanel.Name = "UpIDNamePanel";
            UpIDNamePanel.RowCount = 2;
            UpIDNamePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            UpIDNamePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            UpIDNamePanel.Size = new Size(919, 99);
            UpIDNamePanel.TabIndex = 4;
            // 
            // SalesOrderForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(919, 594);
            Controls.Add(mainPanel);
            Controls.Add(buutonPanel);
            Controls.Add(downAmountPanel);
            Controls.Add(UpIDNamePanel);
            Margin = new Padding(2, 2, 2, 2);
            Name = "SalesOrderForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "訂貨單";
            ((System.ComponentModel.ISupportInitialize)dgvInvoicing).EndInit();
            mainPanel.ResumeLayout(false);
            buutonPanel.ResumeLayout(false);
            downAmountPanel.ResumeLayout(false);
            downAmountPanel.PerformLayout();
            UpIDNamePanel.ResumeLayout(false);
            UpIDNamePanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvInvoicing;
        private Button btnCreateExcel;
        private Button btnDelete;
        private Button btnLoad;
        private Button btnRefresh;
        private Button btnSave;
        private Panel mainPanel;
        private TableLayoutPanel buutonPanel;
        private TextBox txtRemark;
        private Label lblRemark;
        private Label lblTotlaAmountText;
        private Label lblAmount;
        private Label lblDate;
        private Label lblCustomer;
        private Label lblPageNumberText;
        private Label lblNumber;
        private DateTimePicker dtpDate;
        private ComboBox cboCustomer;
        private TableLayoutPanel downAmountPanel;
        private TableLayoutPanel UpIDNamePanel;
        private Button btnPrint;
    }
}