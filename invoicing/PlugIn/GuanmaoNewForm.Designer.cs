namespace invoicing.PlugIn
{
    partial class GuanmaoNewForm
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            btnLoad = new Button();
            dgvInvoicing = new DataGridView();
            downPanel = new Panel();
            ((System.ComponentModel.ISupportInitialize)dgvInvoicing).BeginInit();
            downPanel.SuspendLayout();
            SuspendLayout();
            // 
            // btnLoad
            // 
            btnLoad.Dock = DockStyle.Fill;
            btnLoad.Font = new Font("Microsoft JhengHei UI", 12F);
            btnLoad.Location = new Point(0, 0);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(800, 89);
            btnLoad.TabIndex = 0;
            btnLoad.Text = "讀取";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // dgvInvoicing
            // 
            dgvInvoicing.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvInvoicing.BackgroundColor = SystemColors.Control;
            dgvInvoicing.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Microsoft JhengHei UI", 12F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvInvoicing.DefaultCellStyle = dataGridViewCellStyle3;
            dgvInvoicing.Dock = DockStyle.Fill;
            dgvInvoicing.Location = new Point(0, 0);
            dgvInvoicing.Name = "dgvInvoicing";
            dgvInvoicing.RowHeadersWidth = 51;
            dgvInvoicing.Size = new Size(800, 361);
            dgvInvoicing.TabIndex = 4;
            // 
            // downPanel
            // 
            downPanel.Controls.Add(btnLoad);
            downPanel.Dock = DockStyle.Bottom;
            downPanel.Location = new Point(0, 361);
            downPanel.Name = "downPanel";
            downPanel.Size = new Size(800, 89);
            downPanel.TabIndex = 3;
            // 
            // GuanmaoNewForm
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvInvoicing);
            Controls.Add(downPanel);
            Name = "GuanmaoNewForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "關貿(新)";
            ((System.ComponentModel.ISupportInitialize)dgvInvoicing).EndInit();
            downPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btnLoad;
        private DataGridView dgvInvoicing;
        private Panel downPanel;
    }
}