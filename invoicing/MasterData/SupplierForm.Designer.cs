namespace invoicing.MasterData
{
    partial class SupplierForm
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
            dgvSupplierAll = new DataGridView();
            searchPanel.SuspendLayout();
            dataPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSupplierAll).BeginInit();
            SuspendLayout();
            // 
            // searchPanel
            // 
            searchPanel.ColumnCount = 2;
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            searchPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            searchPanel.Controls.Add(lblInput, 0, 0);
            searchPanel.Controls.Add(txtInput, 1, 0);
            searchPanel.Dock = DockStyle.Top;
            searchPanel.Location = new Point(0, 0);
            searchPanel.Name = "searchPanel";
            searchPanel.RowCount = 1;
            searchPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            searchPanel.Size = new Size(782, 125);
            searchPanel.TabIndex = 2;
            // 
            // lblInput
            // 
            lblInput.Anchor = AnchorStyles.Right;
            lblInput.AutoSize = true;
            lblInput.Font = new Font("Microsoft JhengHei UI", 14F);
            lblInput.Location = new Point(11, 47);
            lblInput.Name = "lblInput";
            lblInput.Size = new Size(181, 30);
            lblInput.TabIndex = 0;
            lblInput.Text = "輸入廠商名稱：";
            // 
            // txtInput
            // 
            txtInput.Anchor = AnchorStyles.Left;
            txtInput.BackColor = SystemColors.Control;
            txtInput.BorderStyle = BorderStyle.None;
            txtInput.Font = new Font("Microsoft JhengHei UI", 14F);
            txtInput.Location = new Point(198, 47);
            txtInput.Name = "txtInput";
            txtInput.Size = new Size(581, 30);
            txtInput.TabIndex = 1;
            txtInput.TextChanged += txtInput_TextChanged;
            // 
            // dataPanel
            // 
            dataPanel.Controls.Add(dgvSupplierAll);
            dataPanel.Dock = DockStyle.Fill;
            dataPanel.Location = new Point(0, 125);
            dataPanel.Name = "dataPanel";
            dataPanel.Size = new Size(782, 328);
            dataPanel.TabIndex = 3;
            // 
            // dgvSupplierAll
            // 
            dgvSupplierAll.BackgroundColor = SystemColors.Control;
            dgvSupplierAll.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSupplierAll.Dock = DockStyle.Fill;
            dgvSupplierAll.Location = new Point(0, 0);
            dgvSupplierAll.Name = "dgvSupplierAll";
            dgvSupplierAll.RowHeadersWidth = 51;
            dgvSupplierAll.Size = new Size(782, 328);
            dgvSupplierAll.TabIndex = 0;
            dgvSupplierAll.CellClick += dgvSupplierAll_CellClick;
            // 
            // SupplierForm
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(782, 453);
            Controls.Add(dataPanel);
            Controls.Add(searchPanel);
            Name = "SupplierForm";
            Text = "廠商資料";
            searchPanel.ResumeLayout(false);
            searchPanel.PerformLayout();
            dataPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSupplierAll).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel searchPanel;
        private Label lblInput;
        private TextBox txtInput;
        private Panel dataPanel;
        private DataGridView dgvSupplierAll;
    }
}