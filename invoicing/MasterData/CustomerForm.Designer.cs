namespace invoicing.MasterData
{
    partial class CustomerForm
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
            searchPanel = new TableLayoutPanel();
            lblInput = new Label();
            txtInput = new TextBox();
            dataPanel = new Panel();
            dgvCustomerAll = new DataGridView();
            searchPanel.SuspendLayout();
            dataPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCustomerAll).BeginInit();
            SuspendLayout();
            // 
            // searchPanel
            // 
            searchPanel.ColumnCount = 2;
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.0476189F));
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80.95238F));
            searchPanel.Controls.Add(lblInput, 0, 0);
            searchPanel.Controls.Add(txtInput, 1, 0);
            searchPanel.Dock = DockStyle.Top;
            searchPanel.Location = new Point(0, 0);
            searchPanel.Name = "searchPanel";
            searchPanel.RowCount = 1;
            searchPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            searchPanel.Size = new Size(1182, 125);
            searchPanel.TabIndex = 0;
            // 
            // lblInput
            // 
            lblInput.Anchor = AnchorStyles.Right;
            lblInput.AutoSize = true;
            lblInput.Font = new Font("Microsoft JhengHei UI", 14F);
            lblInput.Location = new Point(41, 47);
            lblInput.Name = "lblInput";
            lblInput.Size = new Size(181, 30);
            lblInput.TabIndex = 0;
            lblInput.Text = "輸入客戶名稱：";
            // 
            // txtInput
            // 
            txtInput.Anchor = AnchorStyles.Left;
            txtInput.BackColor = SystemColors.Control;
            txtInput.BorderStyle = BorderStyle.None;
            txtInput.Font = new Font("Microsoft JhengHei UI", 14F);
            txtInput.Location = new Point(228, 47);
            txtInput.Name = "txtInput";
            txtInput.Size = new Size(951, 30);
            txtInput.TabIndex = 1;
            txtInput.TextChanged += txtInput_TextChanged;
            // 
            // dataPanel
            // 
            dataPanel.Controls.Add(dgvCustomerAll);
            dataPanel.Dock = DockStyle.Fill;
            dataPanel.Location = new Point(0, 125);
            dataPanel.Name = "dataPanel";
            dataPanel.Size = new Size(1182, 378);
            dataPanel.TabIndex = 1;
            // 
            // dgvCustomerAll
            // 
            dgvCustomerAll.BackgroundColor = SystemColors.Control;
            dgvCustomerAll.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCustomerAll.Dock = DockStyle.Fill;
            dgvCustomerAll.Location = new Point(0, 0);
            dgvCustomerAll.Name = "dgvCustomerAll";
            dgvCustomerAll.RowHeadersWidth = 51;
            dgvCustomerAll.Size = new Size(1182, 378);
            dgvCustomerAll.TabIndex = 0;
            dgvCustomerAll.CellClick += dgvCustomerAll_CellClick;
            // 
            // CustomerForm
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 503);
            Controls.Add(dataPanel);
            Controls.Add(searchPanel);
            Name = "CustomerForm";
            Text = "客戶資料";
            searchPanel.ResumeLayout(false);
            searchPanel.PerformLayout();
            dataPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCustomerAll).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel searchPanel;
        private Panel dataPanel;
        private DataGridView dgvCustomerAll;
        private Label lblInput;
        private TextBox txtInput;
    }
}