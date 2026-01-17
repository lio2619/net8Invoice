namespace invoicing.MasterData
{
    partial class SupplierManageForm
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
            mainPanel = new TableLayoutPanel();
            btnFormClear = new Button();
            btnSupplierDelete = new Button();
            btnSupplierSearch = new Button();
            btnSupplierModify = new Button();
            lblSupplierId = new Label();
            lblSupplierName = new Label();
            lblSupplierAddress = new Label();
            lblSupplierTel = new Label();
            lblSupplierFax = new Label();
            txtSupplierName = new TextBox();
            txtSupplierAddress = new TextBox();
            txtSupplierTel = new TextBox();
            txtSupplierFax = new TextBox();
            btnSupplierCreate = new Button();
            lblSupplierIdValue = new Label();
            mainPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainPanel
            // 
            mainPanel.ColumnCount = 3;
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            mainPanel.Controls.Add(btnFormClear, 2, 4);
            mainPanel.Controls.Add(btnSupplierDelete, 2, 3);
            mainPanel.Controls.Add(btnSupplierSearch, 2, 2);
            mainPanel.Controls.Add(btnSupplierModify, 2, 1);
            mainPanel.Controls.Add(lblSupplierId, 0, 0);
            mainPanel.Controls.Add(lblSupplierName, 0, 1);
            mainPanel.Controls.Add(lblSupplierAddress, 0, 2);
            mainPanel.Controls.Add(lblSupplierTel, 0, 3);
            mainPanel.Controls.Add(lblSupplierFax, 0, 4);
            mainPanel.Controls.Add(txtSupplierName, 1, 1);
            mainPanel.Controls.Add(txtSupplierAddress, 1, 2);
            mainPanel.Controls.Add(txtSupplierTel, 1, 3);
            mainPanel.Controls.Add(txtSupplierFax, 1, 4);
            mainPanel.Controls.Add(btnSupplierCreate, 2, 0);
            mainPanel.Controls.Add(lblSupplierIdValue, 1, 0);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(0, 0);
            mainPanel.Margin = new Padding(2, 2, 2, 2);
            mainPanel.Name = "mainPanel";
            mainPanel.RowCount = 5;
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 16F));
            mainPanel.Size = new Size(919, 381);
            mainPanel.TabIndex = 2;
            // 
            // btnFormClear
            // 
            btnFormClear.Anchor = AnchorStyles.Left;
            btnFormClear.Font = new Font("Microsoft JhengHei UI", 14F);
            btnFormClear.Location = new Point(782, 321);
            btnFormClear.Margin = new Padding(2, 2, 2, 2);
            btnFormClear.Name = "btnFormClear";
            btnFormClear.Size = new Size(134, 43);
            btnFormClear.TabIndex = 14;
            btnFormClear.Text = "清空";
            btnFormClear.UseVisualStyleBackColor = true;
            btnFormClear.Click += btnFormClear_Click;
            // 
            // btnSupplierDelete
            // 
            btnSupplierDelete.Anchor = AnchorStyles.Left;
            btnSupplierDelete.Font = new Font("Microsoft JhengHei UI", 14F);
            btnSupplierDelete.Location = new Point(782, 244);
            btnSupplierDelete.Margin = new Padding(2, 2, 2, 2);
            btnSupplierDelete.Name = "btnSupplierDelete";
            btnSupplierDelete.Size = new Size(134, 43);
            btnSupplierDelete.TabIndex = 13;
            btnSupplierDelete.Text = "刪除";
            btnSupplierDelete.UseVisualStyleBackColor = true;
            btnSupplierDelete.Click += btnSupplierDelete_Click;
            // 
            // btnSupplierSearch
            // 
            btnSupplierSearch.Anchor = AnchorStyles.Left;
            btnSupplierSearch.Font = new Font("Microsoft JhengHei UI", 14F);
            btnSupplierSearch.Location = new Point(782, 168);
            btnSupplierSearch.Margin = new Padding(2, 2, 2, 2);
            btnSupplierSearch.Name = "btnSupplierSearch";
            btnSupplierSearch.Size = new Size(134, 43);
            btnSupplierSearch.TabIndex = 12;
            btnSupplierSearch.Text = "查詢";
            btnSupplierSearch.UseVisualStyleBackColor = true;
            btnSupplierSearch.Click += btnSupplierSearch_Click;
            // 
            // btnSupplierModify
            // 
            btnSupplierModify.Anchor = AnchorStyles.Left;
            btnSupplierModify.Font = new Font("Microsoft JhengHei UI", 14F);
            btnSupplierModify.Location = new Point(782, 92);
            btnSupplierModify.Margin = new Padding(2, 2, 2, 2);
            btnSupplierModify.Name = "btnSupplierModify";
            btnSupplierModify.Size = new Size(134, 43);
            btnSupplierModify.TabIndex = 11;
            btnSupplierModify.Text = "修改";
            btnSupplierModify.UseVisualStyleBackColor = true;
            btnSupplierModify.Click += btnSupplierModify_Click;
            // 
            // lblSupplierId
            // 
            lblSupplierId.Anchor = AnchorStyles.Right;
            lblSupplierId.AutoSize = true;
            lblSupplierId.Font = new Font("Microsoft JhengHei UI", 14F);
            lblSupplierId.Location = new Point(30, 26);
            lblSupplierId.Margin = new Padding(2, 0, 2, 0);
            lblSupplierId.Name = "lblSupplierId";
            lblSupplierId.Size = new Size(105, 24);
            lblSupplierId.TabIndex = 0;
            lblSupplierId.Text = "廠商編號：";
            // 
            // lblSupplierName
            // 
            lblSupplierName.Anchor = AnchorStyles.Right;
            lblSupplierName.AutoSize = true;
            lblSupplierName.Font = new Font("Microsoft JhengHei UI", 14F);
            lblSupplierName.Location = new Point(30, 102);
            lblSupplierName.Margin = new Padding(2, 0, 2, 0);
            lblSupplierName.Name = "lblSupplierName";
            lblSupplierName.Size = new Size(105, 24);
            lblSupplierName.TabIndex = 1;
            lblSupplierName.Text = "廠商名稱：";
            // 
            // lblSupplierAddress
            // 
            lblSupplierAddress.Anchor = AnchorStyles.Right;
            lblSupplierAddress.AutoSize = true;
            lblSupplierAddress.Font = new Font("Microsoft JhengHei UI", 14F);
            lblSupplierAddress.Location = new Point(30, 178);
            lblSupplierAddress.Margin = new Padding(2, 0, 2, 0);
            lblSupplierAddress.Name = "lblSupplierAddress";
            lblSupplierAddress.Size = new Size(105, 24);
            lblSupplierAddress.TabIndex = 2;
            lblSupplierAddress.Text = "廠商地址：";
            // 
            // lblSupplierTel
            // 
            lblSupplierTel.Anchor = AnchorStyles.Right;
            lblSupplierTel.AutoSize = true;
            lblSupplierTel.Font = new Font("Microsoft JhengHei UI", 14F);
            lblSupplierTel.Location = new Point(30, 254);
            lblSupplierTel.Margin = new Padding(2, 0, 2, 0);
            lblSupplierTel.Name = "lblSupplierTel";
            lblSupplierTel.Size = new Size(105, 24);
            lblSupplierTel.TabIndex = 3;
            lblSupplierTel.Text = "聯絡號碼：";
            // 
            // lblSupplierFax
            // 
            lblSupplierFax.Anchor = AnchorStyles.Right;
            lblSupplierFax.AutoSize = true;
            lblSupplierFax.Font = new Font("Microsoft JhengHei UI", 14F);
            lblSupplierFax.Location = new Point(30, 330);
            lblSupplierFax.Margin = new Padding(2, 0, 2, 0);
            lblSupplierFax.Name = "lblSupplierFax";
            lblSupplierFax.Size = new Size(105, 24);
            lblSupplierFax.TabIndex = 4;
            lblSupplierFax.Text = "傳真號碼：";
            // 
            // txtSupplierName
            // 
            txtSupplierName.Anchor = AnchorStyles.Left;
            txtSupplierName.BackColor = SystemColors.Control;
            txtSupplierName.BorderStyle = BorderStyle.None;
            txtSupplierName.Font = new Font("Microsoft JhengHei UI", 14F);
            txtSupplierName.Location = new Point(139, 102);
            txtSupplierName.Margin = new Padding(2, 2, 2, 2);
            txtSupplierName.Name = "txtSupplierName";
            txtSupplierName.Size = new Size(612, 24);
            txtSupplierName.TabIndex = 6;
            // 
            // txtSupplierAddress
            // 
            txtSupplierAddress.Anchor = AnchorStyles.Left;
            txtSupplierAddress.BackColor = SystemColors.Control;
            txtSupplierAddress.BorderStyle = BorderStyle.None;
            txtSupplierAddress.Font = new Font("Microsoft JhengHei UI", 14F);
            txtSupplierAddress.Location = new Point(139, 178);
            txtSupplierAddress.Margin = new Padding(2, 2, 2, 2);
            txtSupplierAddress.Name = "txtSupplierAddress";
            txtSupplierAddress.Size = new Size(612, 24);
            txtSupplierAddress.TabIndex = 7;
            // 
            // txtSupplierTel
            // 
            txtSupplierTel.Anchor = AnchorStyles.Left;
            txtSupplierTel.BackColor = SystemColors.Control;
            txtSupplierTel.BorderStyle = BorderStyle.None;
            txtSupplierTel.Font = new Font("Microsoft JhengHei UI", 14F);
            txtSupplierTel.Location = new Point(139, 254);
            txtSupplierTel.Margin = new Padding(2, 2, 2, 2);
            txtSupplierTel.Name = "txtSupplierTel";
            txtSupplierTel.Size = new Size(612, 24);
            txtSupplierTel.TabIndex = 8;
            // 
            // txtSupplierFax
            // 
            txtSupplierFax.Anchor = AnchorStyles.Left;
            txtSupplierFax.BackColor = SystemColors.Control;
            txtSupplierFax.BorderStyle = BorderStyle.None;
            txtSupplierFax.Font = new Font("Microsoft JhengHei UI", 14F);
            txtSupplierFax.Location = new Point(139, 330);
            txtSupplierFax.Margin = new Padding(2, 2, 2, 2);
            txtSupplierFax.Name = "txtSupplierFax";
            txtSupplierFax.Size = new Size(612, 24);
            txtSupplierFax.TabIndex = 9;
            // 
            // btnSupplierCreate
            // 
            btnSupplierCreate.Anchor = AnchorStyles.Left;
            btnSupplierCreate.Font = new Font("Microsoft JhengHei UI", 14F);
            btnSupplierCreate.Location = new Point(782, 16);
            btnSupplierCreate.Margin = new Padding(2, 2, 2, 2);
            btnSupplierCreate.Name = "btnSupplierCreate";
            btnSupplierCreate.Size = new Size(134, 43);
            btnSupplierCreate.TabIndex = 10;
            btnSupplierCreate.Text = "新增";
            btnSupplierCreate.UseVisualStyleBackColor = true;
            btnSupplierCreate.Click += btnSupplierCreate_Click;
            // 
            // lblSupplierIdValue
            // 
            lblSupplierIdValue.Anchor = AnchorStyles.Left;
            lblSupplierIdValue.AutoSize = true;
            lblSupplierIdValue.Font = new Font("Microsoft JhengHei UI", 14F);
            lblSupplierIdValue.Location = new Point(139, 26);
            lblSupplierIdValue.Margin = new Padding(2, 0, 2, 0);
            lblSupplierIdValue.Name = "lblSupplierIdValue";
            lblSupplierIdValue.Size = new Size(0, 24);
            lblSupplierIdValue.TabIndex = 15;
            // 
            // SupplierManageForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(919, 381);
            Controls.Add(mainPanel);
            Margin = new Padding(2, 2, 2, 2);
            Name = "SupplierManageForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "廠商資料管理";
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel mainPanel;
        private Button btnFormClear;
        private Button btnSupplierDelete;
        private Button btnSupplierSearch;
        private Button btnSupplierModify;
        private Label lblSupplierId;
        private Label lblSupplierName;
        private Label lblSupplierAddress;
        private Label lblSupplierTel;
        private Label lblSupplierFax;
        private TextBox txtSupplierName;
        private TextBox txtSupplierAddress;
        private TextBox txtSupplierTel;
        private TextBox txtSupplierFax;
        private Button btnSupplierCreate;
        private Label lblSupplierIdValue;
    }
}