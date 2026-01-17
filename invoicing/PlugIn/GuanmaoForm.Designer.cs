namespace invoicing.PlugIn
{
    partial class GuanmaoForm
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
            downPanel = new Panel();
            btnLoad = new Button();
            dgvInvoicing = new DataGridView();
            downPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvInvoicing).BeginInit();
            SuspendLayout();
            // 
            // downPanel
            // 
            downPanel.Controls.Add(btnLoad);
            downPanel.Dock = DockStyle.Bottom;
            downPanel.Location = new Point(0, 285);
            downPanel.Margin = new Padding(2, 2, 2, 2);
            downPanel.Name = "downPanel";
            downPanel.Size = new Size(622, 70);
            downPanel.TabIndex = 0;
            // 
            // btnLoad
            // 
            btnLoad.Dock = DockStyle.Fill;
            btnLoad.Font = new Font("Microsoft JhengHei UI", 12F);
            btnLoad.Location = new Point(0, 0);
            btnLoad.Margin = new Padding(2, 2, 2, 2);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(622, 70);
            btnLoad.TabIndex = 0;
            btnLoad.Text = "讀取";
            btnLoad.UseVisualStyleBackColor = true;
            // 
            // dgvInvoicing
            // 
            dgvInvoicing.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvInvoicing.BackgroundColor = SystemColors.Control;
            dgvInvoicing.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Microsoft JhengHei UI", 12F);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dgvInvoicing.DefaultCellStyle = dataGridViewCellStyle1;
            dgvInvoicing.Dock = DockStyle.Fill;
            dgvInvoicing.Location = new Point(0, 0);
            dgvInvoicing.Margin = new Padding(2, 2, 2, 2);
            dgvInvoicing.Name = "dgvInvoicing";
            dgvInvoicing.RowHeadersWidth = 51;
            dgvInvoicing.Size = new Size(622, 285);
            dgvInvoicing.TabIndex = 2;
            // 
            // GuanmaoForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(622, 355);
            Controls.Add(dgvInvoicing);
            Controls.Add(downPanel);
            Margin = new Padding(2, 2, 2, 2);
            Name = "GuanmaoForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "關貿";
            downPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvInvoicing).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel downPanel;
        private Button btnLoad;
        private DataGridView dgvInvoicing;
    }
}