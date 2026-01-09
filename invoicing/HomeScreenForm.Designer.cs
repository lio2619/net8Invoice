namespace invoicing
{
    partial class HomeScreenForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            客戶資料ToolStripMenuItem = new ToolStripMenuItem();
            CustomerMangeMenu = new ToolStripMenuItem();
            CustomerMenu = new ToolStripMenuItem();
            商品資料ToolStripMenuItem = new ToolStripMenuItem();
            ProductMangeMenu = new ToolStripMenuItem();
            ProductMenu = new ToolStripMenuItem();
            廠商資料ToolStripMenuItem = new ToolStripMenuItem();
            SupplierMangeMenu = new ToolStripMenuItem();
            SupplierMenu = new ToolStripMenuItem();
            SalesDeliveryMenu = new ToolStripMenuItem();
            PurchaseReceiptMenu = new ToolStripMenuItem();
            PurchaseOrderMenu = new ToolStripMenuItem();
            SalesOrderMenu = new ToolStripMenuItem();
            SalesReturnMenu = new ToolStripMenuItem();
            PurchaseReturnMenu = new ToolStripMenuItem();
            TotalPurchasesMenu = new ToolStripMenuItem();
            TotalSalesMenu = new ToolStripMenuItem();
            AccountsReceivableMenu = new ToolStripMenuItem();
            外掛ToolStripMenuItem = new ToolStripMenuItem();
            唐益ToolStripMenuItem = new ToolStripMenuItem();
            金大ToolStripMenuItem = new ToolStripMenuItem();
            金大新ToolStripMenuItem = new ToolStripMenuItem();
            關貿ToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { 客戶資料ToolStripMenuItem, 商品資料ToolStripMenuItem, 廠商資料ToolStripMenuItem, SalesDeliveryMenu, PurchaseReceiptMenu, PurchaseOrderMenu, SalesOrderMenu, SalesReturnMenu, PurchaseReturnMenu, TotalPurchasesMenu, TotalSalesMenu, AccountsReceivableMenu, 外掛ToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1207, 28);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // 客戶資料ToolStripMenuItem
            // 
            客戶資料ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { CustomerMangeMenu, CustomerMenu });
            客戶資料ToolStripMenuItem.Name = "客戶資料ToolStripMenuItem";
            客戶資料ToolStripMenuItem.Size = new Size(83, 24);
            客戶資料ToolStripMenuItem.Text = "客戶資料";
            // 
            // CustomerMangeMenu
            // 
            CustomerMangeMenu.Name = "CustomerMangeMenu";
            CustomerMangeMenu.Size = new Size(152, 26);
            CustomerMangeMenu.Text = "個別資料";
            CustomerMangeMenu.Click += CustomerMangeMenu_Click;
            // 
            // CustomerMenu
            // 
            CustomerMenu.Name = "CustomerMenu";
            CustomerMenu.Size = new Size(152, 26);
            CustomerMenu.Text = "全部資料";
            CustomerMenu.Click += CustomerMenu_Click;
            // 
            // 商品資料ToolStripMenuItem
            // 
            商品資料ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ProductMangeMenu, ProductMenu });
            商品資料ToolStripMenuItem.Name = "商品資料ToolStripMenuItem";
            商品資料ToolStripMenuItem.Size = new Size(83, 24);
            商品資料ToolStripMenuItem.Text = "商品資料";
            // 
            // ProductMangeMenu
            // 
            ProductMangeMenu.Name = "ProductMangeMenu";
            ProductMangeMenu.Size = new Size(152, 26);
            ProductMangeMenu.Text = "貨品資料";
            ProductMangeMenu.Click += ProductMangeMenu_Click;
            // 
            // ProductMenu
            // 
            ProductMenu.Name = "ProductMenu";
            ProductMenu.Size = new Size(152, 26);
            ProductMenu.Text = "查詢貨品";
            ProductMenu.Click += ProductMenu_Click;
            // 
            // 廠商資料ToolStripMenuItem
            // 
            廠商資料ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { SupplierMangeMenu, SupplierMenu });
            廠商資料ToolStripMenuItem.Name = "廠商資料ToolStripMenuItem";
            廠商資料ToolStripMenuItem.Size = new Size(83, 24);
            廠商資料ToolStripMenuItem.Text = "廠商資料";
            // 
            // SupplierMangeMenu
            // 
            SupplierMangeMenu.Name = "SupplierMangeMenu";
            SupplierMangeMenu.Size = new Size(152, 26);
            SupplierMangeMenu.Text = "個別資料";
            SupplierMangeMenu.Click += SupplierMangeMenu_Click;
            // 
            // SupplierMenu
            // 
            SupplierMenu.Name = "SupplierMenu";
            SupplierMenu.Size = new Size(152, 26);
            SupplierMenu.Text = "全部資料";
            SupplierMenu.Click += SupplierMenu_Click;
            // 
            // SalesDeliveryMenu
            // 
            SalesDeliveryMenu.Name = "SalesDeliveryMenu";
            SalesDeliveryMenu.Size = new Size(68, 24);
            SalesDeliveryMenu.Text = "出貨單";
            SalesDeliveryMenu.Click += SalesDeliveryMenu_Click;
            // 
            // PurchaseReceiptMenu
            // 
            PurchaseReceiptMenu.Name = "PurchaseReceiptMenu";
            PurchaseReceiptMenu.Size = new Size(68, 24);
            PurchaseReceiptMenu.Text = "進貨單";
            PurchaseReceiptMenu.Click += PurchaseReceiptMenu_Click;
            // 
            // PurchaseOrderMenu
            // 
            PurchaseOrderMenu.Name = "PurchaseOrderMenu";
            PurchaseOrderMenu.Size = new Size(68, 24);
            PurchaseOrderMenu.Text = "採購單";
            PurchaseOrderMenu.Click += PurchaseOrderMenu_Click;
            // 
            // SalesOrderMenu
            // 
            SalesOrderMenu.Name = "SalesOrderMenu";
            SalesOrderMenu.Size = new Size(68, 24);
            SalesOrderMenu.Text = "訂貨單";
            SalesOrderMenu.Click += SalesOrderMenu_Click;
            // 
            // SalesReturnMenu
            // 
            SalesReturnMenu.Name = "SalesReturnMenu";
            SalesReturnMenu.Size = new Size(98, 24);
            SalesReturnMenu.Text = "出貨退回單";
            SalesReturnMenu.Click += SalesReturnMenu_Click;
            // 
            // PurchaseReturnMenu
            // 
            PurchaseReturnMenu.Name = "PurchaseReturnMenu";
            PurchaseReturnMenu.Size = new Size(98, 24);
            PurchaseReturnMenu.Text = "進貨退出單";
            PurchaseReturnMenu.Click += PurchaseReturnMenu_Click;
            // 
            // TotalPurchasesMenu
            // 
            TotalPurchasesMenu.Name = "TotalPurchasesMenu";
            TotalPurchasesMenu.Size = new Size(83, 24);
            TotalPurchasesMenu.Text = "總進貨額";
            TotalPurchasesMenu.Click += TotalPurchasesMenu_Click;
            // 
            // TotalSalesMenu
            // 
            TotalSalesMenu.Name = "TotalSalesMenu";
            TotalSalesMenu.Size = new Size(83, 24);
            TotalSalesMenu.Text = "總銷貨額";
            TotalSalesMenu.Click += TotalSalesMenu_Click;
            // 
            // AccountsReceivableMenu
            // 
            AccountsReceivableMenu.Name = "AccountsReceivableMenu";
            AccountsReceivableMenu.Size = new Size(83, 24);
            AccountsReceivableMenu.Text = "應收帳款";
            AccountsReceivableMenu.Click += AccountsReceivableMenu_Click;
            // 
            // 外掛ToolStripMenuItem
            // 
            外掛ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 唐益ToolStripMenuItem, 金大ToolStripMenuItem, 金大新ToolStripMenuItem, 關貿ToolStripMenuItem });
            外掛ToolStripMenuItem.Name = "外掛ToolStripMenuItem";
            外掛ToolStripMenuItem.Size = new Size(53, 24);
            外掛ToolStripMenuItem.Text = "外掛";
            // 
            // 唐益ToolStripMenuItem
            // 
            唐益ToolStripMenuItem.Name = "唐益ToolStripMenuItem";
            唐益ToolStripMenuItem.Size = new Size(147, 26);
            唐益ToolStripMenuItem.Text = "唐詣";
            // 
            // 金大ToolStripMenuItem
            // 
            金大ToolStripMenuItem.Name = "金大ToolStripMenuItem";
            金大ToolStripMenuItem.Size = new Size(147, 26);
            金大ToolStripMenuItem.Text = "金大";
            // 
            // 金大新ToolStripMenuItem
            // 
            金大新ToolStripMenuItem.Name = "金大新ToolStripMenuItem";
            金大新ToolStripMenuItem.Size = new Size(147, 26);
            金大新ToolStripMenuItem.Text = "金大(新)";
            // 
            // 關貿ToolStripMenuItem
            // 
            關貿ToolStripMenuItem.Name = "關貿ToolStripMenuItem";
            關貿ToolStripMenuItem.Size = new Size(147, 26);
            關貿ToolStripMenuItem.Text = "關貿";
            // 
            // HomeScreenForm
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1207, 450);
            Controls.Add(menuStrip1);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            Name = "HomeScreenForm";
            Text = "進銷存";
            WindowState = FormWindowState.Maximized;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem 客戶資料ToolStripMenuItem;
        private ToolStripMenuItem CustomerMangeMenu;
        private ToolStripMenuItem CustomerMenu;
        private ToolStripMenuItem 商品資料ToolStripMenuItem;
        private ToolStripMenuItem ProductMangeMenu;
        private ToolStripMenuItem ProductMenu;
        private ToolStripMenuItem 廠商資料ToolStripMenuItem;
        private ToolStripMenuItem SupplierMangeMenu;
        private ToolStripMenuItem SupplierMenu;
        private ToolStripMenuItem SalesDeliveryMenu;
        private ToolStripMenuItem PurchaseReceiptMenu;
        private ToolStripMenuItem PurchaseOrderMenu;
        private ToolStripMenuItem SalesOrderMenu;
        private ToolStripMenuItem SalesReturnMenu;
        private ToolStripMenuItem PurchaseReturnMenu;
        private ToolStripMenuItem TotalPurchasesMenu;
        private ToolStripMenuItem TotalSalesMenu;
        private ToolStripMenuItem AccountsReceivableMenu;
        private ToolStripMenuItem 外掛ToolStripMenuItem;
        private ToolStripMenuItem 唐益ToolStripMenuItem;
        private ToolStripMenuItem 金大ToolStripMenuItem;
        private ToolStripMenuItem 金大新ToolStripMenuItem;
        private ToolStripMenuItem 關貿ToolStripMenuItem;
    }
}
