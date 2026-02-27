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
            txtProductBoxCost = new TextBox();
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
            buttonPanel.Location = new Point(648, 0);
            buttonPanel.Margin = new Padding(2);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.RowCount = 5;
            buttonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            buttonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            buttonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            buttonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            buttonPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            buttonPanel.Size = new Size(194, 516);
            buttonPanel.TabIndex = 0;
            // 
            // btnFormClear
            // 
            btnFormClear.Anchor = AnchorStyles.Left;
            btnFormClear.Font = new Font("Microsoft JhengHei UI", 14F);
            btnFormClear.Location = new Point(2, 438);
            btnFormClear.Margin = new Padding(2);
            btnFormClear.Name = "btnFormClear";
            btnFormClear.Size = new Size(190, 51);
            btnFormClear.TabIndex = 4;
            btnFormClear.Text = "清空";
            btnFormClear.UseVisualStyleBackColor = true;
            btnFormClear.Click += btnFormClear_Click;
            // 
            // btnProductDelete
            // 
            btnProductDelete.Anchor = AnchorStyles.Left;
            btnProductDelete.Font = new Font("Microsoft JhengHei UI", 14F);
            btnProductDelete.Location = new Point(2, 335);
            btnProductDelete.Margin = new Padding(2);
            btnProductDelete.Name = "btnProductDelete";
            btnProductDelete.Size = new Size(190, 51);
            btnProductDelete.TabIndex = 3;
            btnProductDelete.Text = "刪除";
            btnProductDelete.UseVisualStyleBackColor = true;
            btnProductDelete.Click += btnProductDelete_Click;
            // 
            // btnProductSearch
            // 
            btnProductSearch.Anchor = AnchorStyles.Left;
            btnProductSearch.Font = new Font("Microsoft JhengHei UI", 14F);
            btnProductSearch.Location = new Point(2, 232);
            btnProductSearch.Margin = new Padding(2);
            btnProductSearch.Name = "btnProductSearch";
            btnProductSearch.Size = new Size(190, 51);
            btnProductSearch.TabIndex = 2;
            btnProductSearch.Text = "搜尋";
            btnProductSearch.UseVisualStyleBackColor = true;
            btnProductSearch.Click += btnProductSearch_Click;
            // 
            // btnProductModify
            // 
            btnProductModify.Anchor = AnchorStyles.Left;
            btnProductModify.Font = new Font("Microsoft JhengHei UI", 14F);
            btnProductModify.Location = new Point(2, 129);
            btnProductModify.Margin = new Padding(2);
            btnProductModify.Name = "btnProductModify";
            btnProductModify.Size = new Size(190, 51);
            btnProductModify.TabIndex = 1;
            btnProductModify.Text = "修改";
            btnProductModify.UseVisualStyleBackColor = true;
            btnProductModify.Click += btnProductModify_Click;
            // 
            // btnProductCreate
            // 
            btnProductCreate.Anchor = AnchorStyles.Left;
            btnProductCreate.Font = new Font("Microsoft JhengHei UI", 14F);
            btnProductCreate.Location = new Point(2, 26);
            btnProductCreate.Margin = new Padding(2);
            btnProductCreate.Name = "btnProductCreate";
            btnProductCreate.Size = new Size(190, 51);
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
            mainPanel.Controls.Add(txtProductBoxCost, 3, 4);
            mainPanel.Controls.Add(txtProductStandardCost, 3, 5);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(0, 0);
            mainPanel.Margin = new Padding(2);
            mainPanel.Name = "mainPanel";
            mainPanel.RowCount = 6;
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6666679F));
            mainPanel.Size = new Size(648, 516);
            mainPanel.TabIndex = 1;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei UI", 14F);
            label1.Location = new Point(22, 31);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(105, 24);
            label1.TabIndex = 0;
            label1.Text = "貨品編號：";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft JhengHei UI", 14F);
            label2.Location = new Point(60, 117);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(67, 24);
            label2.TabIndex = 1;
            label2.Text = "品名：";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft JhengHei UI", 14F);
            label3.Location = new Point(22, 203);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(105, 24);
            label3.TabIndex = 2;
            label3.Text = "基本單位：";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Right;
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft JhengHei UI", 14F);
            label5.Location = new Point(22, 289);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(105, 24);
            label5.TabIndex = 4;
            label5.Text = "標準售價：";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Right;
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft JhengHei UI", 14F);
            label6.Location = new Point(370, 289);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(80, 24);
            label6.TabIndex = 5;
            label6.Text = "售價A：";
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Right;
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft JhengHei UI", 14F);
            label7.Location = new Point(48, 375);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(79, 24);
            label7.TabIndex = 6;
            label7.Text = "售價B：";
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft JhengHei UI", 14F);
            label8.Location = new Point(345, 375);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(105, 24);
            label8.TabIndex = 7;
            label8.Text = "箱購成本：";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft JhengHei UI", 14F);
            label4.Location = new Point(22, 461);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(105, 24);
            label4.TabIndex = 3;
            label4.Text = "標準成本：";
            // 
            // label9
            // 
            label9.Anchor = AnchorStyles.Right;
            label9.AutoSize = true;
            label9.Font = new Font("Microsoft JhengHei UI", 14F);
            label9.Location = new Point(345, 461);
            label9.Margin = new Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new Size(105, 24);
            label9.TabIndex = 8;
            label9.Text = "現行成本：";
            // 
            // txtProductName
            // 
            txtProductName.Anchor = AnchorStyles.Left;
            txtProductName.BackColor = SystemColors.Control;
            txtProductName.BorderStyle = BorderStyle.None;
            mainPanel.SetColumnSpan(txtProductName, 3);
            txtProductName.Font = new Font("Microsoft JhengHei UI", 14F);
            txtProductName.Location = new Point(131, 117);
            txtProductName.Margin = new Padding(2);
            txtProductName.Name = "txtProductName";
            txtProductName.Size = new Size(513, 24);
            txtProductName.TabIndex = 9;
            // 
            // txtProductId
            // 
            txtProductId.Anchor = AnchorStyles.Left;
            txtProductId.BackColor = SystemColors.Control;
            txtProductId.BorderStyle = BorderStyle.None;
            txtProductId.Font = new Font("Microsoft JhengHei UI", 14F);
            txtProductId.Location = new Point(131, 31);
            txtProductId.Margin = new Padding(2);
            txtProductId.Name = "txtProductId";
            txtProductId.Size = new Size(189, 24);
            txtProductId.TabIndex = 10;
            // 
            // txtProductUnit
            // 
            txtProductUnit.Anchor = AnchorStyles.Left;
            txtProductUnit.BackColor = SystemColors.Control;
            txtProductUnit.BorderStyle = BorderStyle.None;
            txtProductUnit.Font = new Font("Microsoft JhengHei UI", 14F);
            txtProductUnit.Location = new Point(131, 203);
            txtProductUnit.Margin = new Padding(2);
            txtProductUnit.Name = "txtProductUnit";
            txtProductUnit.Size = new Size(189, 24);
            txtProductUnit.TabIndex = 11;
            // 
            // txtProductStandardPrice
            // 
            txtProductStandardPrice.Anchor = AnchorStyles.Left;
            txtProductStandardPrice.BackColor = SystemColors.Control;
            txtProductStandardPrice.BorderStyle = BorderStyle.None;
            txtProductStandardPrice.Font = new Font("Microsoft JhengHei UI", 14F);
            txtProductStandardPrice.Location = new Point(131, 289);
            txtProductStandardPrice.Margin = new Padding(2);
            txtProductStandardPrice.Name = "txtProductStandardPrice";
            txtProductStandardPrice.Size = new Size(189, 24);
            txtProductStandardPrice.TabIndex = 12;
            // 
            // txtProductPriceA
            // 
            txtProductPriceA.Anchor = AnchorStyles.Left;
            txtProductPriceA.BackColor = SystemColors.Control;
            txtProductPriceA.BorderStyle = BorderStyle.None;
            txtProductPriceA.Font = new Font("Microsoft JhengHei UI", 14F);
            txtProductPriceA.Location = new Point(454, 289);
            txtProductPriceA.Margin = new Padding(2);
            txtProductPriceA.Name = "txtProductPriceA";
            txtProductPriceA.Size = new Size(189, 24);
            txtProductPriceA.TabIndex = 13;
            // 
            // txtProductPriceB
            // 
            txtProductPriceB.Anchor = AnchorStyles.Left;
            txtProductPriceB.BackColor = SystemColors.Control;
            txtProductPriceB.BorderStyle = BorderStyle.None;
            txtProductPriceB.Font = new Font("Microsoft JhengHei UI", 14F);
            txtProductPriceB.Location = new Point(131, 375);
            txtProductPriceB.Margin = new Padding(2);
            txtProductPriceB.Name = "txtProductPriceB";
            txtProductPriceB.Size = new Size(189, 24);
            txtProductPriceB.TabIndex = 14;
            // 
            // txtProductCurrentCost
            // 
            txtProductCurrentCost.Anchor = AnchorStyles.Left;
            txtProductCurrentCost.BackColor = SystemColors.Control;
            txtProductCurrentCost.BorderStyle = BorderStyle.None;
            txtProductCurrentCost.Font = new Font("Microsoft JhengHei UI", 14F);
            txtProductCurrentCost.Location = new Point(131, 461);
            txtProductCurrentCost.Margin = new Padding(2);
            txtProductCurrentCost.Name = "txtProductCurrentCost";
            txtProductCurrentCost.Size = new Size(189, 24);
            txtProductCurrentCost.TabIndex = 15;
            // 
            // txtProductBoxCost
            // 
            txtProductBoxCost.Anchor = AnchorStyles.Left;
            txtProductBoxCost.BackColor = SystemColors.Control;
            txtProductBoxCost.BorderStyle = BorderStyle.None;
            txtProductBoxCost.Font = new Font("Microsoft JhengHei UI", 14F);
            txtProductBoxCost.Location = new Point(454, 375);
            txtProductBoxCost.Margin = new Padding(2);
            txtProductBoxCost.Name = "txtProductBoxCost";
            txtProductBoxCost.Size = new Size(189, 24);
            txtProductBoxCost.TabIndex = 16;
            // 
            // txtProductStandardCost
            // 
            txtProductStandardCost.Anchor = AnchorStyles.Left;
            txtProductStandardCost.BackColor = SystemColors.Control;
            txtProductStandardCost.BorderStyle = BorderStyle.None;
            txtProductStandardCost.Font = new Font("Microsoft JhengHei UI", 14F);
            txtProductStandardCost.Location = new Point(454, 461);
            txtProductStandardCost.Margin = new Padding(2);
            txtProductStandardCost.Name = "txtProductStandardCost";
            txtProductStandardCost.Size = new Size(189, 24);
            txtProductStandardCost.TabIndex = 17;
            // 
            // ProductManageForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(842, 516);
            Controls.Add(mainPanel);
            Controls.Add(buttonPanel);
            Margin = new Padding(2);
            Name = "ProductManageForm";
            StartPosition = FormStartPosition.CenterScreen;
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
        private TextBox txtProductBoxCost;
        private TextBox txtProductStandardCost;
    }
}