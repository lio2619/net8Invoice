namespace invoicing.MasterData
{
    partial class ProductForm
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
            label2 = new Label();
            label1 = new Label();
            txtInput = new TextBox();
            cboSelect = new ComboBox();
            mainPanel = new Panel();
            dgvProductAll = new DataGridView();
            searchPanel.SuspendLayout();
            mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProductAll).BeginInit();
            SuspendLayout();
            // 
            // searchPanel
            // 
            searchPanel.ColumnCount = 4;
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            searchPanel.Controls.Add(label2, 0, 0);
            searchPanel.Controls.Add(label1, 2, 0);
            searchPanel.Controls.Add(txtInput, 1, 0);
            searchPanel.Controls.Add(cboSelect, 3, 0);
            searchPanel.Dock = DockStyle.Top;
            searchPanel.Location = new Point(0, 0);
            searchPanel.Name = "searchPanel";
            searchPanel.RowCount = 1;
            searchPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            searchPanel.Size = new Size(1182, 125);
            searchPanel.TabIndex = 0;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft JhengHei UI", 14F);
            label2.Location = new Point(148, 47);
            label2.Name = "label2";
            label2.Size = new Size(85, 30);
            label2.TabIndex = 1;
            label2.Text = "輸入：";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei UI", 14F);
            label1.Location = new Point(797, 47);
            label1.Name = "label1";
            label1.Size = new Size(85, 30);
            label1.TabIndex = 0;
            label1.Text = "選擇：";
            // 
            // txtInput
            // 
            txtInput.Anchor = AnchorStyles.Left;
            txtInput.BackColor = SystemColors.Control;
            txtInput.BorderStyle = BorderStyle.None;
            txtInput.Font = new Font("Microsoft JhengHei UI", 14F);
            txtInput.Location = new Point(239, 47);
            txtInput.Name = "txtInput";
            txtInput.Size = new Size(407, 30);
            txtInput.TabIndex = 2;
            txtInput.TextChanged += txtInput_TextChanged;
            // 
            // cboSelect
            // 
            cboSelect.Anchor = AnchorStyles.Left;
            cboSelect.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSelect.Font = new Font("Microsoft JhengHei UI", 14F);
            cboSelect.FormattingEnabled = true;
            cboSelect.Items.AddRange(new object[] { "貨品編號", "商品名稱" });
            cboSelect.Location = new Point(888, 43);
            cboSelect.Name = "cboSelect";
            cboSelect.Size = new Size(194, 37);
            cboSelect.TabIndex = 3;
            // 
            // mainPanel
            // 
            mainPanel.Controls.Add(dgvProductAll);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(0, 125);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(1182, 478);
            mainPanel.TabIndex = 1;
            // 
            // dgvProductAll
            // 
            dgvProductAll.BackgroundColor = SystemColors.Control;
            dgvProductAll.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductAll.Dock = DockStyle.Fill;
            dgvProductAll.Location = new Point(0, 0);
            dgvProductAll.Name = "dgvProductAll";
            dgvProductAll.RowHeadersWidth = 51;
            dgvProductAll.Size = new Size(1182, 478);
            dgvProductAll.TabIndex = 0;
            dgvProductAll.CellClick += dgvProductAll_CellClick;
            // 
            // ProductForm
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 603);
            Controls.Add(mainPanel);
            Controls.Add(searchPanel);
            Name = "ProductForm";
            Text = "產品資料";
            searchPanel.ResumeLayout(false);
            searchPanel.PerformLayout();
            mainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProductAll).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel searchPanel;
        private Label label2;
        private Label label1;
        private TextBox txtInput;
        private Panel mainPanel;
        private DataGridView dgvProductAll;
        private ComboBox cboSelect;
    }
}