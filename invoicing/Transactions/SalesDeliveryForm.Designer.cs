namespace invoicing.Transactions
{
    partial class SalesDeliveryForm
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
            UpIDNamePanel = new TableLayoutPanel();
            lblDate = new Label();
            lblCustomer = new Label();
            lblPageNumberText = new Label();
            lblNumber = new Label();
            dtpDate = new DateTimePicker();
            cboCustomer = new ComboBox();
            downAmountPanel = new TableLayoutPanel();
            txtRemark = new TextBox();
            lblRemark = new Label();
            lblTotlaAmountText = new Label();
            lblAmount = new Label();
            mainPanel = new Panel();
            dgvInvoicing = new DataGridView();
            btnCreateExcel = new Button();
            btnDelete = new Button();
            btnLoad = new Button();
            btnRefresh = new Button();
            btnSave = new Button();
            buutonPanel = new TableLayoutPanel();
            btnPrint = new Button();
            UpIDNamePanel.SuspendLayout();
            downAmountPanel.SuspendLayout();
            mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvInvoicing).BeginInit();
            buutonPanel.SuspendLayout();
            SuspendLayout();
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
            UpIDNamePanel.Name = "UpIDNamePanel";
            UpIDNamePanel.RowCount = 2;
            UpIDNamePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            UpIDNamePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            UpIDNamePanel.Size = new Size(1022, 125);
            UpIDNamePanel.TabIndex = 8;
            // 
            // lblDate
            // 
            lblDate.Anchor = AnchorStyles.Right;
            lblDate.AutoSize = true;
            lblDate.Font = new Font("Microsoft JhengHei UI", 14F);
            lblDate.Location = new Point(65, 16);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(85, 30);
            lblDate.TabIndex = 1;
            lblDate.Text = "日期：";
            // 
            // lblCustomer
            // 
            lblCustomer.Anchor = AnchorStyles.Right;
            lblCustomer.AutoSize = true;
            lblCustomer.Font = new Font("Microsoft JhengHei UI", 14F);
            lblCustomer.Location = new Point(65, 78);
            lblCustomer.Name = "lblCustomer";
            lblCustomer.Size = new Size(85, 30);
            lblCustomer.TabIndex = 2;
            lblCustomer.Text = "客戶：";
            // 
            // lblPageNumberText
            // 
            lblPageNumberText.Anchor = AnchorStyles.Right;
            lblPageNumberText.AutoSize = true;
            lblPageNumberText.Font = new Font("Microsoft JhengHei UI", 14F);
            lblPageNumberText.Location = new Point(527, 78);
            lblPageNumberText.Name = "lblPageNumberText";
            lblPageNumberText.Size = new Size(133, 30);
            lblPageNumberText.TabIndex = 3;
            lblPageNumberText.Text = "單子編號：";
            // 
            // lblNumber
            // 
            lblNumber.Anchor = AnchorStyles.Left;
            lblNumber.AutoSize = true;
            lblNumber.Font = new Font("Microsoft JhengHei UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNumber.Location = new Point(666, 79);
            lblNumber.Name = "lblNumber";
            lblNumber.Size = new Size(26, 29);
            lblNumber.TabIndex = 4;
            lblNumber.Text = "0";
            // 
            // dtpDate
            // 
            dtpDate.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtpDate.CalendarFont = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpDate.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpDate.Location = new Point(156, 14);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(351, 33);
            dtpDate.TabIndex = 5;
            // 
            // cboCustomer
            // 
            cboCustomer.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cboCustomer.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cboCustomer.FormattingEnabled = true;
            cboCustomer.Location = new Point(156, 77);
            cboCustomer.Name = "cboCustomer";
            cboCustomer.Size = new Size(351, 33);
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
            downAmountPanel.Location = new Point(0, 673);
            downAmountPanel.Name = "downAmountPanel";
            downAmountPanel.RowCount = 1;
            downAmountPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            downAmountPanel.Size = new Size(1022, 80);
            downAmountPanel.TabIndex = 9;
            // 
            // txtRemark
            // 
            txtRemark.Anchor = AnchorStyles.Left;
            txtRemark.BackColor = SystemColors.Control;
            txtRemark.BorderStyle = BorderStyle.None;
            txtRemark.Font = new Font("Microsoft JhengHei UI", 14F);
            txtRemark.Location = new Point(156, 25);
            txtRemark.Name = "txtRemark";
            txtRemark.Size = new Size(351, 30);
            txtRemark.TabIndex = 6;
            // 
            // lblRemark
            // 
            lblRemark.Anchor = AnchorStyles.Right;
            lblRemark.AutoSize = true;
            lblRemark.Font = new Font("Microsoft JhengHei UI", 14F);
            lblRemark.Location = new Point(65, 25);
            lblRemark.Name = "lblRemark";
            lblRemark.Size = new Size(85, 30);
            lblRemark.TabIndex = 3;
            lblRemark.Text = "備註：";
            // 
            // lblTotlaAmountText
            // 
            lblTotlaAmountText.Anchor = AnchorStyles.Right;
            lblTotlaAmountText.AutoSize = true;
            lblTotlaAmountText.Font = new Font("Microsoft JhengHei UI", 14F);
            lblTotlaAmountText.Location = new Point(575, 25);
            lblTotlaAmountText.Name = "lblTotlaAmountText";
            lblTotlaAmountText.Size = new Size(85, 30);
            lblTotlaAmountText.TabIndex = 4;
            lblTotlaAmountText.Text = "總計：";
            // 
            // lblAmount
            // 
            lblAmount.Anchor = AnchorStyles.Left;
            lblAmount.AutoSize = true;
            lblAmount.Font = new Font("Microsoft JhengHei UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblAmount.Location = new Point(666, 25);
            lblAmount.Name = "lblAmount";
            lblAmount.Size = new Size(26, 29);
            lblAmount.TabIndex = 5;
            lblAmount.Text = "0";
            // 
            // mainPanel
            // 
            mainPanel.Controls.Add(dgvInvoicing);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(0, 125);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(1022, 548);
            mainPanel.TabIndex = 11;
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
            dgvInvoicing.Name = "dgvInvoicing";
            dgvInvoicing.RowHeadersWidth = 51;
            dgvInvoicing.Size = new Size(1022, 548);
            dgvInvoicing.TabIndex = 1;
            // 
            // btnCreateExcel
            // 
            btnCreateExcel.Anchor = AnchorStyles.Left;
            btnCreateExcel.Font = new Font("Microsoft JhengHei UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCreateExcel.Location = new Point(3, 655);
            btnCreateExcel.Name = "btnCreateExcel";
            btnCreateExcel.Size = new Size(154, 58);
            btnCreateExcel.TabIndex = 4;
            btnCreateExcel.Text = "創建Excel";
            btnCreateExcel.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Left;
            btnDelete.Font = new Font("Microsoft JhengHei UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDelete.Location = new Point(3, 526);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(154, 58);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "刪單";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnLoad
            // 
            btnLoad.Anchor = AnchorStyles.Left;
            btnLoad.Font = new Font("Microsoft JhengHei UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLoad.Location = new Point(3, 406);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(154, 58);
            btnLoad.TabIndex = 2;
            btnLoad.Text = "讀檔";
            btnLoad.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Left;
            btnRefresh.Font = new Font("Microsoft JhengHei UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRefresh.Location = new Point(3, 286);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(154, 58);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "刷新";
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Left;
            btnSave.Font = new Font("Microsoft JhengHei UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSave.Location = new Point(3, 166);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(154, 58);
            btnSave.TabIndex = 0;
            btnSave.Text = "儲存";
            btnSave.UseVisualStyleBackColor = true;
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
            buutonPanel.Location = new Point(1022, 125);
            buutonPanel.Name = "buutonPanel";
            buutonPanel.RowCount = 6;
            buutonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66F));
            buutonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66F));
            buutonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66F));
            buutonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66F));
            buutonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66F));
            buutonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66F));
            buutonPanel.Size = new Size(160, 548);
            buutonPanel.TabIndex = 10;
            // 
            // btnPrint
            // 
            btnPrint.Anchor = AnchorStyles.Left;
            btnPrint.Font = new Font("Microsoft JhengHei UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPrint.Location = new Point(3, 38);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(154, 58);
            btnPrint.TabIndex = 5;
            btnPrint.Text = "預覽列印";
            btnPrint.UseVisualStyleBackColor = true;
            // 
            // SalesDeliveryForm
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 753);
            Controls.Add(mainPanel);
            Controls.Add(buutonPanel);
            Controls.Add(downAmountPanel);
            Controls.Add(UpIDNamePanel);
            Name = "SalesDeliveryForm";
            Text = "出貨單";
            UpIDNamePanel.ResumeLayout(false);
            UpIDNamePanel.PerformLayout();
            downAmountPanel.ResumeLayout(false);
            downAmountPanel.PerformLayout();
            mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvInvoicing).EndInit();
            buutonPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel UpIDNamePanel;
        private Label lblDate;
        private Label lblCustomer;
        private Label lblPageNumberText;
        private Label lblNumber;
        private DateTimePicker dtpDate;
        private ComboBox cboCustomer;
        private TableLayoutPanel downAmountPanel;
        private TextBox txtRemark;
        private Label lblRemark;
        private Label lblTotlaAmountText;
        private Label lblAmount;
        private Panel mainPanel;
        private DataGridView dgvInvoicing;
        private Button btnCreateExcel;
        private Button btnDelete;
        private Button btnLoad;
        private Button btnRefresh;
        private Button btnSave;
        private TableLayoutPanel buutonPanel;
        private Button btnPrint;
    }
}