namespace invoicing.MasterData
{
    partial class ProductManageForm
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
            buttonPanel = new TableLayoutPanel();
            btnFormClear = new Button();
            btnProductDelete = new Button();
            btnProductSearch = new Button();
            btnProductModify = new Button();
            btnProductCreate = new Button();
            mainPanel = new TableLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label4 = new Label();
            label9 = new Label();
            txtProductName = new TextBox();
            txtProductId = new TextBox();
            txtProductUnit = new TextBox();
            txtProductStandardPrice = new TextBox();
            txtProductPriceA = new TextBox();
            txtProductPriceB = new TextBox();
            txtProductCurrentCost = new TextBox();
            txtProductPriceC = new TextBox();
            txtProductStandardCost = new TextBox();
            buttonPanel.SuspendLayout();
            mainPanel.SuspendLayout();
            SuspendLayout();
            // 
            // buttonPanel
            // 
            buttonPanel.ColumnCount = 1;
            buttonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            buttonPanel.Controls.Add(btnFormClear, 0, 4);
            buttonPanel.Controls.Add(btnProductDelete, 0, 3);
            buttonPanel.Controls.Add(btnProductSearch, 0, 2);
            buttonPanel.Controls.Add(btnProductModify, 0, 1);
            buttonPanel.Controls.Add(btnProductCreate, 0, 0);
            buttonPanel.Dock = DockStyle.Right;
            buttonPanel.Location = new Point(832, 0);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.RowCount = 5;
            buttonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            buttonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            buttonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            buttonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            buttonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            buttonPanel.Size = new Size(250, 653);
            buttonPanel.TabIndex = 0;
            // 
            // btnFormClear
            // 
            btnFormClear.Anchor = AnchorStyles.Left;
            btnFormClear.Font = new Font("Microsoft JhengHei UI", 14F);
            btnFormClear.Location = new Point(3, 554);
            btnFormClear.Name = "btnFormClear";
            btnFormClear.Size = new Size(244, 64);
            btnFormClear.TabIndex = 4;
            btnFormClear.Text = "清空";
            btnFormClear.UseVisualStyleBackColor = true;
            btnFormClear.Click += btnFormClear_Click;
            // 
            // btnProductDelete
            // 
            btnProductDelete.Anchor = AnchorStyles.Left;
            btnProductDelete.Font = new Font("Microsoft JhengHei UI", 14F);
            btnProductDelete.Location = new Point(3, 423);
            btnProductDelete.Name = "btnProductDelete";
            btnProductDelete.Size = new Size(244, 64);
            btnProductDelete.TabIndex = 3;
            btnProductDelete.Text = "刪除";
            btnProductDelete.UseVisualStyleBackColor = true;
            btnProductDelete.Click += btnProductDelete_Click;
            // 
            // btnProductSearch
            // 
            btnProductSearch.Anchor = AnchorStyles.Left;
            btnProductSearch.Font = new Font("Microsoft JhengHei UI", 14F);
            btnProductSearch.Location = new Point(3, 293);
            btnProductSearch.Name = "btnProductSearch";
            btnProductSearch.Size = new Size(244, 64);
            btnProductSearch.TabIndex = 2;
            btnProductSearch.Text = "搜尋";
            btnProductSearch.UseVisualStyleBackColor = true;
            btnProductSearch.Click += btnProductSearch_Click;
            // 
            // btnProductModify
            // 
            btnProductModify.Anchor = AnchorStyles.Left;
            btnProductModify.Font = new Font("Microsoft JhengHei UI", 14F);
            btnProductModify.Location = new Point(3, 163);
            btnProductModify.Name = "btnProductModify";
            btnProductModify.Size = new Size(244, 64);
            btnProductModify.TabIndex = 1;
            btnProductModify.Text = "修改";
            btnProductModify.UseVisualStyleBackColor = true;
            btnProductModify.Click += btnProductModify_Click;
            // 
            // btnProductCreate
            // 
            btnProductCreate.Anchor = AnchorStyles.Left;
            btnProductCreate.Font = new Font("Microsoft JhengHei UI", 14F);
            btnProductCreate.Location = new Point(3, 33);
            btnProductCreate.Name = "btnProductCreate";
            btnProductCreate.Size = new Size(244, 64);
            btnProductCreate.TabIndex = 0;
            btnProductCreate.Text = "新增";
            btnProductCreate.UseVisualStyleBackColor = true;
            btnProductCreate.Click += btnProductCreate_Click;
            // 
            // mainPanel
            // 
            mainPanel.ColumnCount = 4;
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            mainPanel.Controls.Add(label1, 0, 0);
            mainPanel.Controls.Add(label2, 0, 1);
            mainPanel.Controls.Add(label3, 0, 2);
            mainPanel.Controls.Add(label5, 0, 3);
            mainPanel.Controls.Add(label6, 2, 3);
            mainPanel.Controls.Add(label7, 0, 4);
            mainPanel.Controls.Add(label8, 2, 4);
            mainPanel.Controls.Add(label4, 0, 5);
            mainPanel.Controls.Add(label9, 2, 5);
            mainPanel.Controls.Add(txtProductName, 1, 1);
            mainPanel.Controls.Add(txtProductId, 1, 0);
            mainPanel.Controls.Add(txtProductUnit, 1, 2);
            mainPanel.Controls.Add(txtProductStandardPrice, 1, 3);
            mainPanel.Controls.Add(txtProductPriceA, 3, 3);
            mainPanel.Controls.Add(txtProductPriceB, 1, 4);
            mainPanel.Controls.Add(txtProductCurrentCost, 1, 5);
            mainPanel.Controls.Add(txtProductPriceC, 3, 4);
            mainPanel.Controls.Add(txtProductStandardCost, 3, 5);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.RowCount = 6;
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            mainPanel.Size = new Size(832, 653);
            mainPanel.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei UI", 14F);
            label1.Location = new Point(30, 39);
            label1.Name = "label1";
            label1.Size = new Size(133, 30);
            label1.TabIndex = 0;
            label1.Text = "貨品編號：";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft JhengHei UI", 14F);
            label2.Location = new Point(78, 147);
            label2.Name = "label2";
            label2.Size = new Size(85, 30);
            label2.TabIndex = 1;
            label2.Text = "品名：";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft JhengHei UI", 14F);
            label3.Location = new Point(30, 255);
            label3.Name = "label3";
            label3.Size = new Size(133, 30);
            label3.TabIndex = 2;
            label3.Text = "基本單位：";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft JhengHei UI", 14F);
            label5.Location = new Point(30, 363);
            label5.Name = "label5";
            label5.Size = new Size(133, 30);
            label5.TabIndex = 4;
            label5.Text = "標準售價：";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft JhengHei UI", 14F);
            label6.Location = new Point(476, 363);
            label6.Name = "label6";
            label6.Size = new Size(102, 30);
            label6.TabIndex = 5;
            label6.Text = "售價A：";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft JhengHei UI", 14F);
            label7.Location = new Point(63, 471);
            label7.Name = "label7";
            label7.Size = new Size(100, 30);
            label7.TabIndex = 6;
            label7.Text = "售價B：";
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft JhengHei UI", 14F);
            label8.Location = new Point(477, 471);
            label8.Name = "label8";
            label8.Size = new Size(101, 30);
            label8.TabIndex = 7;
            label8.Text = "售價C：";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft JhengHei UI", 14F);
            label4.Location = new Point(30, 581);
            label4.Name = "label4";
            label4.Size = new Size(133, 30);
            label4.TabIndex = 3;
            label4.Text = "現行成本：";
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Right;
            label9.AutoSize = true;
            label9.Font = new Font("Microsoft JhengHei UI", 14F);
            label9.Location = new Point(445, 581);
            label9.Name = "label9";
            label9.Size = new Size(133, 30);
            label9.TabIndex = 8;
            label9.Text = "標準成本：";
            // 
            // txtProductName
            // 
            txtProductName.Anchor = AnchorStyles.Left;
            txtProductName.BackColor = SystemColors.Control;
            txtProductName.BorderStyle = BorderStyle.None;
            mainPanel.SetColumnSpan(txtProductName, 3);
            txtProductName.Font = new Font("Microsoft JhengHei UI", 14F);
            txtProductName.Location = new Point(169, 143);
            txtProductName.Name = "txtProductName";
            txtProductName.Size = new Size(660, 30);
            txtProductName.TabIndex = 9;
            // 
            // txtProductId
            // 
            txtProductId.Anchor = AnchorStyles.Left;
            txtProductId.BackColor = SystemColors.Control;
            txtProductId.BorderStyle = BorderStyle.None;
            txtProductId.Font = new Font("Microsoft JhengHei UI", 14F);
            txtProductId.Location = new Point(169, 35);
            txtProductId.Name = "txtProductId";
            txtProductId.Size = new Size(243, 30);
            txtProductId.TabIndex = 10;
            // 
            // txtProductUnit
            // 
            txtProductUnit.Anchor = AnchorStyles.Left;
            txtProductUnit.BackColor = SystemColors.Control;
            txtProductUnit.BorderStyle = BorderStyle.None;
            txtProductUnit.Font = new Font("Microsoft JhengHei UI", 14F);
            txtProductUnit.Location = new Point(169, 251);
            txtProductUnit.Name = "txtProductUnit";
            txtProductUnit.Size = new Size(243, 30);
            txtProductUnit.TabIndex = 11;
            // 
            // txtProductStandardPrice
            // 
            txtProductStandardPrice.Anchor = AnchorStyles.Left;
            txtProductStandardPrice.BackColor = SystemColors.Control;
            txtProductStandardPrice.BorderStyle = BorderStyle.None;
            txtProductStandardPrice.Font = new Font("Microsoft JhengHei UI", 14F);
            txtProductStandardPrice.Location = new Point(169, 359);
            txtProductStandardPrice.Name = "txtProductStandardPrice";
            txtProductStandardPrice.Size = new Size(243, 30);
            txtProductStandardPrice.TabIndex = 12;
            // 
            // txtProductPriceA
            // 
            txtProductPriceA.Anchor = AnchorStyles.Left;
            txtProductPriceA.BackColor = SystemColors.Control;
            txtProductPriceA.BorderStyle = BorderStyle.None;
            txtProductPriceA.Font = new Font("Microsoft JhengHei UI", 14F);
            txtProductPriceA.Location = new Point(584, 359);
            txtProductPriceA.Name = "txtProductPriceA";
            txtProductPriceA.Size = new Size(243, 30);
            txtProductPriceA.TabIndex = 13;
            // 
            // txtProductPriceB
            // 
            txtProductPriceB.Anchor = AnchorStyles.Left;
            txtProductPriceB.BackColor = SystemColors.Control;
            txtProductPriceB.BorderStyle = BorderStyle.None;
            txtProductPriceB.Font = new Font("Microsoft JhengHei UI", 14F);
            txtProductPriceB.Location = new Point(169, 467);
            txtProductPriceB.Name = "txtProductPriceB";
            txtProductPriceB.Size = new Size(243, 30);
            txtProductPriceB.TabIndex = 14;
            // 
            // txtProductCurrentCost
            // 
            txtProductCurrentCost.Anchor = AnchorStyles.Left;
            txtProductCurrentCost.BackColor = SystemColors.Control;
            txtProductCurrentCost.BorderStyle = BorderStyle.None;
            txtProductCurrentCost.Font = new Font("Microsoft JhengHei UI", 14F);
            txtProductCurrentCost.Location = new Point(169, 578);
            txtProductCurrentCost.Name = "txtProductCurrentCost";
            txtProductCurrentCost.Size = new Size(243, 30);
            txtProductCurrentCost.TabIndex = 15;
            // 
            // txtProductPriceC
            // 
            txtProductPriceC.Anchor = AnchorStyles.Left;
            txtProductPriceC.BackColor = SystemColors.Control;
            txtProductPriceC.BorderStyle = BorderStyle.None;
            txtProductPriceC.Font = new Font("Microsoft JhengHei UI", 14F);
            txtProductPriceC.Location = new Point(584, 467);
            txtProductPriceC.Name = "txtProductPriceC";
            txtProductPriceC.Size = new Size(243, 30);
            txtProductPriceC.TabIndex = 16;
            // 
            // txtProductStandardCost
            // 
            txtProductStandardCost.Anchor = AnchorStyles.Left;
            txtProductStandardCost.BackColor = SystemColors.Control;
            txtProductStandardCost.BorderStyle = BorderStyle.None;
            txtProductStandardCost.Font = new Font("Microsoft JhengHei UI", 14F);
            txtProductStandardCost.Location = new Point(584, 578);
            txtProductStandardCost.Name = "txtProductStandardCost";
            txtProductStandardCost.Size = new Size(243, 30);
            txtProductStandardCost.TabIndex = 17;
            // 
            // ProductManageForm
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1082, 653);
            Controls.Add(mainPanel);
            Controls.Add(buttonPanel);
            Name = "ProductManageForm";
            Text = "產品資料管理";
            buttonPanel.ResumeLayout(false);
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel buttonPanel;
        private TableLayoutPanel mainPanel;
        private Button btnProductCreate;
        private Button btnFormClear;
        private Button btnProductDelete;
        private Button btnProductSearch;
        private Button btnProductModify;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label4;
        private Label label9;
        private TextBox txtProductName;
        private TextBox txtProductId;
        private TextBox txtProductUnit;
        private TextBox txtProductStandardPrice;
        private TextBox txtProductPriceA;
        private TextBox txtProductPriceB;
        private TextBox txtProductCurrentCost;
        private TextBox txtProductPriceC;
        private TextBox txtProductStandardCost;
    }
}