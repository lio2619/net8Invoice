namespace invoicing.Financials
{
    partial class AccountsReceivableForm
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
            lblCustomer = new Label();
            lblStartText = new Label();
            lblEndText = new Label();
            dtpStart = new DateTimePicker();
            dtpEnd = new DateTimePicker();
            cboCustomer = new ComboBox();
            buttonPanel = new TableLayoutPanel();
            lblNTotalumber = new Label();
            lblTotal = new Label();
            lblTax = new Label();
            btnPrint = new Button();
            txtTax = new TextBox();
            mainPanel = new Panel();
            dgvAccountsReceivable = new DataGridView();
            searchPanel.SuspendLayout();
            buttonPanel.SuspendLayout();
            mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAccountsReceivable).BeginInit();
            SuspendLayout();
            // 
            // searchPanel
            // 
            searchPanel.ColumnCount = 6;
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.42362F));
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.2830963F));
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5254583F));
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22.09776F));
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.452139F));
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.6252537F));
            searchPanel.Controls.Add(lblCustomer, 4, 0);
            searchPanel.Controls.Add(lblStartText, 0, 0);
            searchPanel.Controls.Add(lblEndText, 2, 0);
            searchPanel.Controls.Add(dtpStart, 1, 0);
            searchPanel.Controls.Add(dtpEnd, 3, 0);
            searchPanel.Controls.Add(cboCustomer, 5, 0);
            searchPanel.Dock = DockStyle.Top;
            searchPanel.Location = new Point(0, 0);
            searchPanel.Name = "searchPanel";
            searchPanel.RowCount = 1;
            searchPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            searchPanel.Size = new Size(982, 76);
            searchPanel.TabIndex = 4;
            // 
            // lblCustomer
            // 
            lblCustomer.Anchor = AnchorStyles.Right;
            lblCustomer.AutoSize = true;
            lblCustomer.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCustomer.Location = new Point(674, 25);
            lblCustomer.Name = "lblCustomer";
            lblCustomer.Size = new Size(72, 25);
            lblCustomer.TabIndex = 4;
            lblCustomer.Text = "客戶：";
            // 
            // lblStartText
            // 
            lblStartText.Anchor = AnchorStyles.Right;
            lblStartText.AutoSize = true;
            lblStartText.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStartText.Location = new Point(6, 25);
            lblStartText.Name = "lblStartText";
            lblStartText.Size = new Size(112, 25);
            lblStartText.TabIndex = 0;
            lblStartText.Text = "起始時間：";
            // 
            // lblEndText
            // 
            lblEndText.Anchor = AnchorStyles.Right;
            lblEndText.AutoSize = true;
            lblEndText.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblEndText.Location = new Point(336, 25);
            lblEndText.Name = "lblEndText";
            lblEndText.Size = new Size(112, 25);
            lblEndText.TabIndex = 1;
            lblEndText.Text = "結束時間：";
            // 
            // dtpStart
            // 
            dtpStart.Anchor = AnchorStyles.Left;
            dtpStart.CalendarFont = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpStart.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpStart.Location = new Point(124, 21);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(202, 33);
            dtpStart.TabIndex = 2;
            // 
            // dtpEnd
            // 
            dtpEnd.Anchor = AnchorStyles.Left;
            dtpEnd.CalendarFont = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpEnd.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpEnd.Location = new Point(454, 21);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(210, 33);
            dtpEnd.TabIndex = 3;
            // 
            // cboCustomer
            // 
            cboCustomer.Anchor = AnchorStyles.Left;
            cboCustomer.Font = new Font("Microsoft JhengHei UI", 12F);
            cboCustomer.FormattingEnabled = true;
            cboCustomer.Location = new Point(752, 21);
            cboCustomer.Name = "cboCustomer";
            cboCustomer.Size = new Size(227, 33);
            cboCustomer.TabIndex = 5;
            // 
            // buttonPanel
            // 
            buttonPanel.ColumnCount = 5;
            buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13F));
            buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13F));
            buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.0509167F));
            buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17.2097759F));
            buttonPanel.Controls.Add(lblNTotalumber, 1, 0);
            buttonPanel.Controls.Add(lblTotal, 0, 0);
            buttonPanel.Controls.Add(lblTax, 2, 0);
            buttonPanel.Controls.Add(btnPrint, 4, 0);
            buttonPanel.Controls.Add(txtTax, 3, 0);
            buttonPanel.Dock = DockStyle.Bottom;
            buttonPanel.Location = new Point(0, 430);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.RowCount = 1;
            buttonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            buttonPanel.Size = new Size(982, 73);
            buttonPanel.TabIndex = 5;
            // 
            // lblNTotalumber
            // 
            lblNTotalumber.Anchor = AnchorStyles.Left;
            lblNTotalumber.AutoSize = true;
            lblNTotalumber.Font = new Font("Microsoft JhengHei UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblNTotalumber.Location = new Point(130, 22);
            lblNTotalumber.Name = "lblNTotalumber";
            lblNTotalumber.Size = new Size(26, 29);
            lblNTotalumber.TabIndex = 8;
            lblNTotalumber.Text = "0";
            // 
            // lblTotal
            // 
            lblTotal.Anchor = AnchorStyles.Right;
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Microsoft JhengHei UI", 12F);
            lblTotal.Location = new Point(12, 24);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(112, 25);
            lblTotal.TabIndex = 0;
            lblTotal.Text = "本期合計：";
            // 
            // lblTax
            // 
            lblTax.Anchor = AnchorStyles.Right;
            lblTax.AutoSize = true;
            lblTax.Font = new Font("Microsoft JhengHei UI", 12F);
            lblTax.Location = new Point(472, 24);
            lblTax.Name = "lblTax";
            lblTax.Size = new Size(92, 25);
            lblTax.TabIndex = 1;
            lblTax.Text = "營業稅：";
            // 
            // btnPrint
            // 
            btnPrint.Dock = DockStyle.Fill;
            btnPrint.Font = new Font("Microsoft JhengHei UI", 12F);
            btnPrint.Location = new Point(815, 3);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(164, 67);
            btnPrint.TabIndex = 2;
            btnPrint.Text = "列印";
            btnPrint.UseVisualStyleBackColor = true;
            // 
            // txtTax
            // 
            txtTax.Anchor = AnchorStyles.Left;
            txtTax.BackColor = SystemColors.Control;
            txtTax.BorderStyle = BorderStyle.None;
            txtTax.Font = new Font("Microsoft JhengHei UI", 14F);
            txtTax.Location = new Point(570, 21);
            txtTax.Name = "txtTax";
            txtTax.Size = new Size(239, 30);
            txtTax.TabIndex = 7;
            // 
            // mainPanel
            // 
            mainPanel.Controls.Add(dgvAccountsReceivable);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(0, 76);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(982, 354);
            mainPanel.TabIndex = 6;
            // 
            // dgvAccountsReceivable
            // 
            dgvAccountsReceivable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvAccountsReceivable.BackgroundColor = SystemColors.Control;
            dgvAccountsReceivable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Microsoft JhengHei UI", 12F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dgvAccountsReceivable.DefaultCellStyle = dataGridViewCellStyle1;
            dgvAccountsReceivable.Dock = DockStyle.Fill;
            dgvAccountsReceivable.Location = new Point(0, 0);
            dgvAccountsReceivable.Name = "dgvAccountsReceivable";
            dgvAccountsReceivable.RowHeadersWidth = 51;
            dgvAccountsReceivable.Size = new Size(982, 354);
            dgvAccountsReceivable.TabIndex = 8;
            // 
            // AccountsReceivableForm
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 503);
            Controls.Add(mainPanel);
            Controls.Add(searchPanel);
            Controls.Add(buttonPanel);
            Name = "AccountsReceivableForm";
            Text = "應收帳款";
            searchPanel.ResumeLayout(false);
            searchPanel.PerformLayout();
            buttonPanel.ResumeLayout(false);
            buttonPanel.PerformLayout();
            mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAccountsReceivable).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel searchPanel;
        private Label lblStartText;
        private Label lblEndText;
        private DateTimePicker dtpStart;
        private DateTimePicker dtpEnd;
        private TableLayoutPanel buttonPanel;
        private Label lblCustomer;
        private ComboBox cboCustomer;
        private Label lblTotal;
        private Label lblTax;
        private Button btnPrint;
        private TextBox txtTax;
        private Label lblNTotalumber;
        private Panel mainPanel;
        private DataGridView dgvAccountsReceivable;
    }
}