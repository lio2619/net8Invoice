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
            mainPanel.Name = "mainPanel";
            mainPanel.RowCount = 5;
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            mainPanel.Size = new Size(1182, 483);
            mainPanel.TabIndex = 2;
            // 
            // btnFormClear
            // 
            btnFormClear.Anchor = AnchorStyles.Left;
            btnFormClear.Font = new Font("Microsoft JhengHei UI", 14F);
            btnFormClear.Location = new Point(1007, 406);
            btnFormClear.Name = "btnFormClear";
            btnFormClear.Size = new Size(172, 55);
            btnFormClear.TabIndex = 14;
            btnFormClear.Text = "清空";
            btnFormClear.UseVisualStyleBackColor = true;
            btnFormClear.Click += btnFormClear_Click;
            // 
            // btnSupplierDelete
            // 
            btnSupplierDelete.Anchor = AnchorStyles.Left;
            btnSupplierDelete.Font = new Font("Microsoft JhengHei UI", 14F);
            btnSupplierDelete.Location = new Point(1007, 308);
            btnSupplierDelete.Name = "btnSupplierDelete";
            btnSupplierDelete.Size = new Size(172, 55);
            btnSupplierDelete.TabIndex = 13;
            btnSupplierDelete.Text = "刪除";
            btnSupplierDelete.UseVisualStyleBackColor = true;
            btnSupplierDelete.Click += btnSupplierDelete_Click;
            // 
            // btnSupplierSearch
            // 
            btnSupplierSearch.Anchor = AnchorStyles.Left;
            btnSupplierSearch.Font = new Font("Microsoft JhengHei UI", 14F);
            btnSupplierSearch.Location = new Point(1007, 212);
            btnSupplierSearch.Name = "btnSupplierSearch";
            btnSupplierSearch.Size = new Size(172, 55);
            btnSupplierSearch.TabIndex = 12;
            btnSupplierSearch.Text = "查詢";
            btnSupplierSearch.UseVisualStyleBackColor = true;
            btnSupplierSearch.Click += btnSupplierSearch_Click;
            // 
            // btnSupplierModify
            // 
            btnSupplierModify.Anchor = AnchorStyles.Left;
            btnSupplierModify.Font = new Font("Microsoft JhengHei UI", 14F);
            btnSupplierModify.Location = new Point(1007, 116);
            btnSupplierModify.Name = "btnSupplierModify";
            btnSupplierModify.Size = new Size(172, 55);
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
            lblSupplierId.Location = new Point(41, 33);
            lblSupplierId.Name = "lblSupplierId";
            lblSupplierId.Size = new Size(133, 30);
            lblSupplierId.TabIndex = 0;
            lblSupplierId.Text = "廠商編號：";
            // 
            // lblSupplierName
            // 
            lblSupplierName.Anchor = AnchorStyles.Right;
            lblSupplierName.AutoSize = true;
            lblSupplierName.Font = new Font("Microsoft JhengHei UI", 14F);
            lblSupplierName.Location = new Point(41, 129);
            lblSupplierName.Name = "lblSupplierName";
            lblSupplierName.Size = new Size(133, 30);
            lblSupplierName.TabIndex = 1;
            lblSupplierName.Text = "廠商名稱：";
            // 
            // lblSupplierAddress
            // 
            lblSupplierAddress.Anchor = AnchorStyles.Right;
            lblSupplierAddress.AutoSize = true;
            lblSupplierAddress.Font = new Font("Microsoft JhengHei UI", 14F);
            lblSupplierAddress.Location = new Point(41, 225);
            lblSupplierAddress.Name = "lblSupplierAddress";
            lblSupplierAddress.Size = new Size(133, 30);
            lblSupplierAddress.TabIndex = 2;
            lblSupplierAddress.Text = "廠商地址：";
            // 
            // lblSupplierTel
            // 
            lblSupplierTel.Anchor = AnchorStyles.Right;
            lblSupplierTel.AutoSize = true;
            lblSupplierTel.Font = new Font("Microsoft JhengHei UI", 14F);
            lblSupplierTel.Location = new Point(41, 321);
            lblSupplierTel.Name = "lblSupplierTel";
            lblSupplierTel.Size = new Size(133, 30);
            lblSupplierTel.TabIndex = 3;
            lblSupplierTel.Text = "聯絡號碼：";
            // 
            // lblSupplierFax
            // 
            lblSupplierFax.Anchor = AnchorStyles.Right;
            lblSupplierFax.AutoSize = true;
            lblSupplierFax.Font = new Font("Microsoft JhengHei UI", 14F);
            lblSupplierFax.Location = new Point(41, 418);
            lblSupplierFax.Name = "lblSupplierFax";
            lblSupplierFax.Size = new Size(133, 30);
            lblSupplierFax.TabIndex = 4;
            lblSupplierFax.Text = "傳真號碼：";
            // 
            // txtSupplierName
            // 
            txtSupplierName.Anchor = AnchorStyles.Left;
            txtSupplierName.BackColor = SystemColors.Control;
            txtSupplierName.BorderStyle = BorderStyle.None;
            txtSupplierName.Font = new Font("Microsoft JhengHei UI", 14F);
            txtSupplierName.Location = new Point(180, 129);
            txtSupplierName.Name = "txtSupplierName";
            txtSupplierName.Size = new Size(787, 30);
            txtSupplierName.TabIndex = 6;
            // 
            // txtSupplierAddress
            // 
            txtSupplierAddress.Anchor = AnchorStyles.Left;
            txtSupplierAddress.BackColor = SystemColors.Control;
            txtSupplierAddress.BorderStyle = BorderStyle.None;
            txtSupplierAddress.Font = new Font("Microsoft JhengHei UI", 14F);
            txtSupplierAddress.Location = new Point(180, 225);
            txtSupplierAddress.Name = "txtSupplierAddress";
            txtSupplierAddress.Size = new Size(787, 30);
            txtSupplierAddress.TabIndex = 7;
            // 
            // txtSupplierTel
            // 
            txtSupplierTel.Anchor = AnchorStyles.Left;
            txtSupplierTel.BackColor = SystemColors.Control;
            txtSupplierTel.BorderStyle = BorderStyle.None;
            txtSupplierTel.Font = new Font("Microsoft JhengHei UI", 14F);
            txtSupplierTel.Location = new Point(180, 321);
            txtSupplierTel.Name = "txtSupplierTel";
            txtSupplierTel.Size = new Size(787, 30);
            txtSupplierTel.TabIndex = 8;
            // 
            // txtSupplierFax
            // 
            txtSupplierFax.Anchor = AnchorStyles.Left;
            txtSupplierFax.BackColor = SystemColors.Control;
            txtSupplierFax.BorderStyle = BorderStyle.None;
            txtSupplierFax.Font = new Font("Microsoft JhengHei UI", 14F);
            txtSupplierFax.Location = new Point(180, 418);
            txtSupplierFax.Name = "txtSupplierFax";
            txtSupplierFax.Size = new Size(787, 30);
            txtSupplierFax.TabIndex = 9;
            // 
            // btnSupplierCreate
            // 
            btnSupplierCreate.Anchor = AnchorStyles.Left;
            btnSupplierCreate.Font = new Font("Microsoft JhengHei UI", 14F);
            btnSupplierCreate.Location = new Point(1007, 20);
            btnSupplierCreate.Name = "btnSupplierCreate";
            btnSupplierCreate.Size = new Size(172, 55);
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
            lblSupplierIdValue.Location = new Point(180, 33);
            lblSupplierIdValue.Name = "lblSupplierIdValue";
            lblSupplierIdValue.Size = new Size(0, 30);
            lblSupplierIdValue.TabIndex = 15;
            // 
            // SupplierManageForm
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 483);
            Controls.Add(mainPanel);
            Name = "SupplierManageForm";
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