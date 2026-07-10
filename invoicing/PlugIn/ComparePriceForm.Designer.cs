namespace invoicing.PlugIn
{
    partial class ComparePriceForm
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
            tableLayoutPanel1 = new TableLayoutPanel();
            btnRead = new Button();
            rtbErrorShow = new RichTextBox();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(btnRead, 0, 1);
            tableLayoutPanel1.Controls.Add(rtbErrorShow, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 83.8649139F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 16.1350842F));
            tableLayoutPanel1.Size = new Size(800, 533);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // btnRead
            // 
            btnRead.Dock = DockStyle.Fill;
            btnRead.Font = new Font("Microsoft JhengHei UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 136);
            btnRead.Location = new Point(3, 450);
            btnRead.Name = "btnRead";
            btnRead.Size = new Size(794, 80);
            btnRead.TabIndex = 0;
            btnRead.Text = "讀取單子";
            btnRead.UseVisualStyleBackColor = true;
            // 
            // rtbErrorShow
            // 
            rtbErrorShow.Dock = DockStyle.Fill;
            rtbErrorShow.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 136);
            rtbErrorShow.Location = new Point(3, 3);
            rtbErrorShow.Name = "rtbErrorShow";
            rtbErrorShow.Size = new Size(794, 441);
            rtbErrorShow.TabIndex = 1;
            rtbErrorShow.Text = "";
            // 
            // ComparePriceForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 533);
            Controls.Add(tableLayoutPanel1);
            Name = "ComparePriceForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "比價";
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button btnRead;
        private RichTextBox rtbErrorShow;
    }
}