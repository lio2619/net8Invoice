namespace invoicing.ReadForm
{
    partial class ReadInvoicesForm
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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            searchPanel = new TableLayoutPanel();
            lblStartText = new Label();
            lblEndText = new Label();
            dtpStart = new DateTimePicker();
            dtpEnd = new DateTimePicker();
            buttonPanel = new TableLayoutPanel();
            btnSearch = new Button();
            btnOK = new Button();
            dgvPanel = new Panel();
            dgvReadInvoicing = new DataGridView();
            rdbOther = new RadioButton();
            rdbNine = new RadioButton();
            cboNine = new ComboBox();
            searchPanel.SuspendLayout();
            buttonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReadInvoicing).BeginInit();
            SuspendLayout();
            // 
            // searchPanel
            // 
            searchPanel.ColumnCount = 7;
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10.1372757F));
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.2185841F));
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 9.926083F));
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.22492F));
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.909853F));
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 9.329141F));
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.8029346F));
            searchPanel.Controls.Add(rdbNine, 5, 0);
            searchPanel.Controls.Add(lblStartText, 0, 0);
            searchPanel.Controls.Add(lblEndText, 2, 0);
            searchPanel.Controls.Add(dtpStart, 1, 0);
            searchPanel.Controls.Add(dtpEnd, 3, 0);
            searchPanel.Controls.Add(rdbOther, 4, 0);
            searchPanel.Controls.Add(cboNine, 6, 0);
            searchPanel.Dock = DockStyle.Top;
            searchPanel.Location = new Point(0, 0);
            searchPanel.Margin = new Padding(2, 2, 2, 2);
            searchPanel.Name = "searchPanel";
            searchPanel.RowCount = 1;
            searchPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            searchPanel.Size = new Size(954, 60);
            searchPanel.TabIndex = 0;
            // 
            // lblStartText
            // 
            lblStartText.Anchor = AnchorStyles.Right;
            lblStartText.AutoSize = true;
            lblStartText.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStartText.Location = new Point(5, 20);
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
            lblEndText.Location = new Point(281, 20);
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
            dtpStart.Location = new Point(98, 16);
            dtpStart.Margin = new Padding(2, 2, 2, 2);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(178, 28);
            dtpStart.TabIndex = 2;
            // 
            // dtpEnd
            // 
            dtpEnd.Anchor = AnchorStyles.Left;
            dtpEnd.CalendarFont = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpEnd.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpEnd.Location = new Point(374, 16);
            dtpEnd.Margin = new Padding(2, 2, 2, 2);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(197, 28);
            dtpEnd.TabIndex = 3;
            // 
            // buttonPanel
            // 
            buttonPanel.ColumnCount = 2;
            buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            buttonPanel.Controls.Add(btnSearch, 0, 0);
            buttonPanel.Controls.Add(btnOK, 1, 0);
            buttonPanel.Dock = DockStyle.Bottom;
            buttonPanel.Location = new Point(0, 399);
            buttonPanel.Margin = new Padding(2, 2, 2, 2);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.RowCount = 1;
            buttonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            buttonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            buttonPanel.Size = new Size(954, 58);
            buttonPanel.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.Dock = DockStyle.Fill;
            btnSearch.Font = new Font("Microsoft JhengHei UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSearch.Location = new Point(2, 2);
            btnSearch.Margin = new Padding(2, 2, 2, 2);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(473, 54);
            btnSearch.TabIndex = 0;
            btnSearch.Text = "查詢";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Dock = DockStyle.Fill;
            btnOK.Font = new Font("Microsoft JhengHei UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnOK.Location = new Point(479, 2);
            btnOK.Margin = new Padding(2, 2, 2, 2);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(473, 54);
            btnOK.TabIndex = 1;
            btnOK.Text = "確定";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // dgvPanel
            // 
            dgvPanel.Dock = DockStyle.Fill;
            dgvPanel.Location = new Point(0, 60);
            dgvPanel.Margin = new Padding(2, 2, 2, 2);
            dgvPanel.Name = "dgvPanel";
            dgvPanel.Size = new Size(954, 339);
            dgvPanel.TabIndex = 2;
            // 
            // dgvReadInvoicing
            // 
            dgvReadInvoicing.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvReadInvoicing.BackgroundColor = SystemColors.Control;
            dgvReadInvoicing.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Microsoft JhengHei UI", 12F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvReadInvoicing.DefaultCellStyle = dataGridViewCellStyle2;
            dgvReadInvoicing.Dock = DockStyle.Fill;
            dgvReadInvoicing.Location = new Point(0, 60);
            dgvReadInvoicing.Margin = new Padding(2, 2, 2, 2);
            dgvReadInvoicing.Name = "dgvReadInvoicing";
            dgvReadInvoicing.RowHeadersWidth = 51;
            dgvReadInvoicing.Size = new Size(954, 339);
            dgvReadInvoicing.TabIndex = 3;
            // 
            // rdbOther
            // 
            rdbOther.Anchor = AnchorStyles.Right;
            rdbOther.AutoSize = true;
            rdbOther.Font = new Font("Microsoft JhengHei UI", 12F);
            rdbOther.Location = new Point(595, 18);
            rdbOther.Name = "rdbOther";
            rdbOther.Size = new Size(59, 24);
            rdbOther.TabIndex = 4;
            rdbOther.TabStop = true;
            rdbOther.Text = "其他";
            rdbOther.UseVisualStyleBackColor = true;
            // 
            // rdbNine
            // 
            rdbNine.Anchor = AnchorStyles.Right;
            rdbNine.AutoSize = true;
            rdbNine.Font = new Font("Microsoft JhengHei UI", 12F);
            rdbNine.Location = new Point(667, 18);
            rdbNine.Name = "rdbNine";
            rdbNine.Size = new Size(75, 24);
            rdbNine.TabIndex = 5;
            rdbNine.TabStop = true;
            rdbNine.Text = "九乘九";
            rdbNine.UseVisualStyleBackColor = true;
            // 
            // cboNine
            // 
            cboNine.Anchor = AnchorStyles.Left;
            cboNine.Font = new Font("Microsoft JhengHei UI", 12F);
            cboNine.FormattingEnabled = true;
            cboNine.Location = new Point(748, 18);
            cboNine.Name = "cboNine";
            cboNine.Size = new Size(195, 28);
            cboNine.TabIndex = 6;
            // 
            // ReadInvoicesForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(954, 457);
            Controls.Add(dgvReadInvoicing);
            Controls.Add(dgvPanel);
            Controls.Add(buttonPanel);
            Controls.Add(searchPanel);
            Margin = new Padding(2, 2, 2, 2);
            Name = "ReadInvoicesForm";
            Text = "讀取單子";
            searchPanel.ResumeLayout(false);
            searchPanel.PerformLayout();
            buttonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvReadInvoicing).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel buttonPanel;
        private TableLayoutPanel searchPanel;
        private Label lblStartText;
        private Label lblEndText;
        private Panel dgvPanel;
        private DateTimePicker dtpStart;
        private DateTimePicker dtpEnd;
        private Button btnSearch;
        private Button btnOK;
        private DataGridView dgvReadInvoicing;
        private RadioButton rdbNine;
        private RadioButton rdbOther;
        private ComboBox cboNine;
    }
}