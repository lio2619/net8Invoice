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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
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
            searchPanel.SuspendLayout();
            buttonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvReadInvoicing).BeginInit();
            SuspendLayout();
            // 
            // searchPanel
            // 
            searchPanel.ColumnCount = 4;
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            searchPanel.Controls.Add(lblStartText, 0, 0);
            searchPanel.Controls.Add(lblEndText, 2, 0);
            searchPanel.Controls.Add(dtpStart, 1, 0);
            searchPanel.Controls.Add(dtpEnd, 3, 0);
            searchPanel.Dock = DockStyle.Top;
            searchPanel.Location = new Point(0, 0);
            searchPanel.Name = "searchPanel";
            searchPanel.RowCount = 1;
            searchPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            searchPanel.Size = new Size(875, 76);
            searchPanel.TabIndex = 0;
            // 
            // lblStartText
            // 
            lblStartText.Anchor = AnchorStyles.Right;
            lblStartText.AutoSize = true;
            lblStartText.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStartText.Location = new Point(16, 25);
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
            lblEndText.Location = new Point(453, 25);
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
            dtpStart.Location = new Point(134, 21);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(300, 33);
            dtpStart.TabIndex = 2;
            // 
            // dtpEnd
            // 
            dtpEnd.Anchor = AnchorStyles.Left;
            dtpEnd.CalendarFont = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpEnd.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpEnd.Location = new Point(571, 21);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(301, 33);
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
            buttonPanel.Location = new Point(0, 423);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.RowCount = 1;
            buttonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            buttonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            buttonPanel.Size = new Size(875, 73);
            buttonPanel.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.Dock = DockStyle.Fill;
            btnSearch.Font = new Font("Microsoft JhengHei UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSearch.Location = new Point(3, 3);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(431, 67);
            btnSearch.TabIndex = 0;
            btnSearch.Text = "查詢";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Dock = DockStyle.Fill;
            btnOK.Font = new Font("Microsoft JhengHei UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnOK.Location = new Point(440, 3);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(432, 67);
            btnOK.TabIndex = 1;
            btnOK.Text = "確定";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // dgvPanel
            // 
            dgvPanel.Dock = DockStyle.Fill;
            dgvPanel.Location = new Point(0, 76);
            dgvPanel.Name = "dgvPanel";
            dgvPanel.Size = new Size(875, 347);
            dgvPanel.TabIndex = 2;
            // 
            // dgvReadInvoicing
            // 
            dgvReadInvoicing.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvReadInvoicing.BackgroundColor = SystemColors.Control;
            dgvReadInvoicing.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Microsoft JhengHei UI", 12F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dgvReadInvoicing.DefaultCellStyle = dataGridViewCellStyle1;
            dgvReadInvoicing.Dock = DockStyle.Fill;
            dgvReadInvoicing.Location = new Point(0, 76);
            dgvReadInvoicing.Name = "dgvReadInvoicing";
            dgvReadInvoicing.RowHeadersWidth = 51;
            dgvReadInvoicing.Size = new Size(875, 347);
            dgvReadInvoicing.TabIndex = 3;
            // 
            // ReadInvoicesForm
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(875, 496);
            Controls.Add(dgvReadInvoicing);
            Controls.Add(dgvPanel);
            Controls.Add(buttonPanel);
            Controls.Add(searchPanel);
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
    }
}