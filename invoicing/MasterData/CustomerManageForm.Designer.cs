namespace invoicing.MasterData
{
    partial class CustomerManageForm
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
            btnCustomerDelete = new Button();
            btnCustomerSearch = new Button();
            btnCustomerModify = new Button();
            lblCustomerId = new Label();
            lblCustomerName = new Label();
            lblCustomerAddress = new Label();
            lblCustomerTel = new Label();
            lblCustomerFax = new Label();
            txtCustomerName = new TextBox();
            txtCustomerAddress = new TextBox();
            txtCustomerTel = new TextBox();
            txtCustomerFax = new TextBox();
            btnCustomerCreate = new Button();
            lblCustomerIdValue = new Label();
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
            mainPanel.Controls.Add(btnCustomerDelete, 2, 3);
            mainPanel.Controls.Add(btnCustomerSearch, 2, 2);
            mainPanel.Controls.Add(btnCustomerModify, 2, 1);
            mainPanel.Controls.Add(lblCustomerId, 0, 0);
            mainPanel.Controls.Add(lblCustomerName, 0, 1);
            mainPanel.Controls.Add(lblCustomerAddress, 0, 2);
            mainPanel.Controls.Add(lblCustomerTel, 0, 3);
            mainPanel.Controls.Add(lblCustomerFax, 0, 4);
            mainPanel.Controls.Add(txtCustomerName, 1, 1);
            mainPanel.Controls.Add(txtCustomerAddress, 1, 2);
            mainPanel.Controls.Add(txtCustomerTel, 1, 3);
            mainPanel.Controls.Add(txtCustomerFax, 1, 4);
            mainPanel.Controls.Add(btnCustomerCreate, 2, 0);
            mainPanel.Controls.Add(lblCustomerIdValue, 1, 0);
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
            mainPanel.TabIndex = 1;
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
            // btnCustomerDelete
            // 
            btnCustomerDelete.Anchor = AnchorStyles.Left;
            btnCustomerDelete.Font = new Font("Microsoft JhengHei UI", 14F);
            btnCustomerDelete.Location = new Point(1007, 308);
            btnCustomerDelete.Name = "btnCustomerDelete";
            btnCustomerDelete.Size = new Size(172, 55);
            btnCustomerDelete.TabIndex = 13;
            btnCustomerDelete.Text = "刪除";
            btnCustomerDelete.UseVisualStyleBackColor = true;
            btnCustomerDelete.Click += btnCustomerDelete_Click;
            // 
            // btnCustomerSearch
            // 
            btnCustomerSearch.Anchor = AnchorStyles.Left;
            btnCustomerSearch.Font = new Font("Microsoft JhengHei UI", 14F);
            btnCustomerSearch.Location = new Point(1007, 212);
            btnCustomerSearch.Name = "btnCustomerSearch";
            btnCustomerSearch.Size = new Size(172, 55);
            btnCustomerSearch.TabIndex = 12;
            btnCustomerSearch.Text = "查詢";
            btnCustomerSearch.UseVisualStyleBackColor = true;
            btnCustomerSearch.Click += btnCustomerSearch_Click;
            // 
            // btnCustomerModify
            // 
            btnCustomerModify.Anchor = AnchorStyles.Left;
            btnCustomerModify.Font = new Font("Microsoft JhengHei UI", 14F);
            btnCustomerModify.Location = new Point(1007, 116);
            btnCustomerModify.Name = "btnCustomerModify";
            btnCustomerModify.Size = new Size(172, 55);
            btnCustomerModify.TabIndex = 11;
            btnCustomerModify.Text = "修改";
            btnCustomerModify.UseVisualStyleBackColor = true;
            btnCustomerModify.Click += btnCustomerModify_Click;
            // 
            // lblCustomerId
            // 
            lblCustomerId.Anchor = AnchorStyles.Right;
            lblCustomerId.AutoSize = true;
            lblCustomerId.Font = new Font("Microsoft JhengHei UI", 14F);
            lblCustomerId.Location = new Point(41, 33);
            lblCustomerId.Name = "lblCustomerId";
            lblCustomerId.Size = new Size(133, 30);
            lblCustomerId.TabIndex = 0;
            lblCustomerId.Text = "客戶編號：";
            // 
            // lblCustomerName
            // 
            lblCustomerName.Anchor = AnchorStyles.Right;
            lblCustomerName.AutoSize = true;
            lblCustomerName.Font = new Font("Microsoft JhengHei UI", 14F);
            lblCustomerName.Location = new Point(41, 129);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new Size(133, 30);
            lblCustomerName.TabIndex = 1;
            lblCustomerName.Text = "客戶名稱：";
            // 
            // lblCustomerAddress
            // 
            lblCustomerAddress.Anchor = AnchorStyles.Right;
            lblCustomerAddress.AutoSize = true;
            lblCustomerAddress.Font = new Font("Microsoft JhengHei UI", 14F);
            lblCustomerAddress.Location = new Point(41, 225);
            lblCustomerAddress.Name = "lblCustomerAddress";
            lblCustomerAddress.Size = new Size(133, 30);
            lblCustomerAddress.TabIndex = 2;
            lblCustomerAddress.Text = "客戶地址：";
            // 
            // lblCustomerTel
            // 
            lblCustomerTel.Anchor = AnchorStyles.Right;
            lblCustomerTel.AutoSize = true;
            lblCustomerTel.Font = new Font("Microsoft JhengHei UI", 14F);
            lblCustomerTel.Location = new Point(41, 321);
            lblCustomerTel.Name = "lblCustomerTel";
            lblCustomerTel.Size = new Size(133, 30);
            lblCustomerTel.TabIndex = 3;
            lblCustomerTel.Text = "聯絡號碼：";
            // 
            // lblCustomerFax
            // 
            lblCustomerFax.Anchor = AnchorStyles.Right;
            lblCustomerFax.AutoSize = true;
            lblCustomerFax.Font = new Font("Microsoft JhengHei UI", 14F);
            lblCustomerFax.Location = new Point(41, 418);
            lblCustomerFax.Name = "lblCustomerFax";
            lblCustomerFax.Size = new Size(133, 30);
            lblCustomerFax.TabIndex = 4;
            lblCustomerFax.Text = "傳真號碼：";
            // 
            // txtCustomerName
            // 
            txtCustomerName.Anchor = AnchorStyles.Left;
            txtCustomerName.BackColor = SystemColors.Control;
            txtCustomerName.BorderStyle = BorderStyle.None;
            txtCustomerName.Font = new Font("Microsoft JhengHei UI", 14F);
            txtCustomerName.Location = new Point(180, 129);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.Size = new Size(787, 30);
            txtCustomerName.TabIndex = 6;
            // 
            // txtCustomerAddress
            // 
            txtCustomerAddress.Anchor = AnchorStyles.Left;
            txtCustomerAddress.BackColor = SystemColors.Control;
            txtCustomerAddress.BorderStyle = BorderStyle.None;
            txtCustomerAddress.Font = new Font("Microsoft JhengHei UI", 14F);
            txtCustomerAddress.Location = new Point(180, 225);
            txtCustomerAddress.Name = "txtCustomerAddress";
            txtCustomerAddress.Size = new Size(787, 30);
            txtCustomerAddress.TabIndex = 7;
            // 
            // txtCustomerTel
            // 
            txtCustomerTel.Anchor = AnchorStyles.Left;
            txtCustomerTel.BackColor = SystemColors.Control;
            txtCustomerTel.BorderStyle = BorderStyle.None;
            txtCustomerTel.Font = new Font("Microsoft JhengHei UI", 14F);
            txtCustomerTel.Location = new Point(180, 321);
            txtCustomerTel.Name = "txtCustomerTel";
            txtCustomerTel.Size = new Size(787, 30);
            txtCustomerTel.TabIndex = 8;
            // 
            // txtCustomerFax
            // 
            txtCustomerFax.Anchor = AnchorStyles.Left;
            txtCustomerFax.BackColor = SystemColors.Control;
            txtCustomerFax.BorderStyle = BorderStyle.None;
            txtCustomerFax.Font = new Font("Microsoft JhengHei UI", 14F);
            txtCustomerFax.Location = new Point(180, 418);
            txtCustomerFax.Name = "txtCustomerFax";
            txtCustomerFax.Size = new Size(787, 30);
            txtCustomerFax.TabIndex = 9;
            // 
            // btnCustomerCreate
            // 
            btnCustomerCreate.Anchor = AnchorStyles.Left;
            btnCustomerCreate.Font = new Font("Microsoft JhengHei UI", 14F);
            btnCustomerCreate.Location = new Point(1007, 20);
            btnCustomerCreate.Name = "btnCustomerCreate";
            btnCustomerCreate.Size = new Size(172, 55);
            btnCustomerCreate.TabIndex = 10;
            btnCustomerCreate.Text = "新增";
            btnCustomerCreate.UseVisualStyleBackColor = true;
            btnCustomerCreate.Click += btnCustomerCreate_Click;
            // 
            // lblCustomerIdValue
            // 
            lblCustomerIdValue.Anchor = AnchorStyles.Left;
            lblCustomerIdValue.AutoSize = true;
            lblCustomerIdValue.Font = new Font("Microsoft JhengHei UI", 14F);
            lblCustomerIdValue.Location = new Point(180, 33);
            lblCustomerIdValue.Name = "lblCustomerIdValue";
            lblCustomerIdValue.Size = new Size(0, 30);
            lblCustomerIdValue.TabIndex = 15;
            // 
            // CustomerManageForm
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 483);
            Controls.Add(mainPanel);
            Name = "CustomerManageForm";
            Text = "客戶資料管理";
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private TableLayoutPanel mainPanel;
        private Label lblCustomerId;
        private Label lblCustomerName;
        private Label lblCustomerAddress;
        private Label lblCustomerTel;
        private Label lblCustomerFax;
        private TextBox txtCustomerName;
        private TextBox txtCustomerAddress;
        private TextBox txtCustomerTel;
        private TextBox txtCustomerFax;
        private Button btnFormClear;
        private Button btnCustomerDelete;
        private Button btnCustomerSearch;
        private Button btnCustomerModify;
        private Button btnCustomerCreate;
        private Label lblCustomerIdValue;
    }
}