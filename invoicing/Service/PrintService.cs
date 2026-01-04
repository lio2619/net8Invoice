using invoicing.Models.DTO;
using invoicing.Service.Interface;
using PdfiumViewer;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Configuration;

namespace invoicing.Service
{
    public class PrintService : IPrintService
    {
        private readonly string _defaultCustomerName = ConfigurationManager.AppSettings["DefaultCustomerName"] ?? "";
        private readonly string _defaultCustomerTel = ConfigurationManager.AppSettings["DefaultCustomerTel"] ?? "";
        private readonly string _defaultCustomerFax = ConfigurationManager.AppSettings["DefaultCustomerFax"] ?? "";

        // 每頁最大筆數
        private const int MaxRowsPerPage = 40;
        // 小於等於此筆數時，表尾位置調整至頁面中間
        private const int SmallPageThreshold = 16;

        static PrintService()
        {
            // 設定 QuestPDF 社群授權
            QuestPDF.Settings.License = LicenseType.Community;
        }

        #region QuestPDF + PdfiumViewer 列印功能

        /// <summary>
        /// 使用 QuestPDF 產生發票 PDF
        /// </summary>
        public byte[] GenerateInvoicePdf(PrintInvoiceRequest request)
        {
            // 計算總資料筆數
            int totalItems = request.OrderType == "採購單"
                ? (request.PurchaseOrderDetails?.Count ?? 0)
                : (request.Details?.Count ?? 0);

            // 計算總頁數
            int totalPages = totalItems <= 0 ? 1 : (int)Math.Ceiling((double)totalItems / MaxRowsPerPage);

            var document = Document.Create(container =>
            {
                int itemIndex = 0;

                for (int pageNum = 1; pageNum <= totalPages; pageNum++)
                {
                    int currentPage = pageNum;
                    int startIndex = itemIndex;
                    int itemsOnThisPage = Math.Min(MaxRowsPerPage, totalItems - startIndex);
                    bool isLastPage = (currentPage == totalPages);

                    // 判斷最後一頁是否需要特殊處理（≤16筆時表尾在中間）
                    bool useMiddleFooter = isLastPage && itemsOnThisPage <= SmallPageThreshold && itemsOnThisPage > 0;

                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(20);
                        page.DefaultTextStyle(x => x.FontFamily("Microsoft JhengHei").FontSize(8));

                        page.Header().Element(c => ComposeHeaderWithPageNumber(c, request, currentPage, totalPages));
                        page.Content().Element(c => ComposeContentPaged(c, request, startIndex, itemsOnThisPage, useMiddleFooter));

                        // 只在最後一頁顯示表尾
                        if (isLastPage)
                        {
                            if (useMiddleFooter)
                            {
                                // ≤16筆：表尾已包含在 Content 中（中間位置）
                                page.Footer().Element(c => { });
                            }
                            else
                            {
                                page.Footer().Element(c => ComposeFooter(c, request));
                            }
                        }
                    });

                    itemIndex += itemsOnThisPage;
                }
            });

            return document.GeneratePdf();
        }

        /// <summary>
        /// 使用 PdfiumViewer 顯示 PDF 預覽視窗並提供列印功能
        /// </summary>
        public void ShowPrintPreviewAndPrint(byte[] pdfBytes, int copies = 1)
        {
            using var stream = new MemoryStream(pdfBytes);
            using var pdfDocument = PdfDocument.Load(stream);

            // 建立預覽表單
            var previewForm = new Form
            {
                Text = "列印預覽",
                Width = 900,
                Height = 700,
                StartPosition = FormStartPosition.CenterScreen
            };

            // PDF 檢視器
            var pdfViewer = new PdfViewer
            {
                Dock = DockStyle.Fill,
                Document = pdfDocument
            };

            // 列印按鈕
            var printButton = new Button
            {
                Text = "列印",
                Dock = DockStyle.Bottom,
                Height = 40
            };

            printButton.Click += (sender, e) =>
            {
                using var printDoc = pdfDocument.CreatePrintDocument();
                printDoc.PrinterSettings.Copies = (short)copies;

                var printDialog = new PrintDialog
                {
                    Document = printDoc,
                    UseEXDialog = true
                };

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDoc.Print();
                }
            };

            previewForm.Controls.Add(pdfViewer);
            previewForm.Controls.Add(printButton);
            previewForm.ShowDialog();
        }

        /// <summary>
        /// 繪製表頭（含頁次）
        /// </summary>
        private void ComposeHeaderWithPageNumber(IContainer container, PrintInvoiceRequest request, int currentPage, int totalPages)
        {
            var clientLabel = request.IsSupplier ? "廠商名稱" : "客戶名稱";

            container.Column(column =>
            {
                column.Spacing(5);

                // 第一行：公司名稱 + 單據類型
                column.Item().Row(row =>
                {
                    row.RelativeItem().Text(_defaultCustomerName).FontSize(18).Bold();
                    row.ConstantItem(150).AlignRight().Text(request.OrderType).FontSize(18).Bold();
                });

                // 第二行：電話、傳真、頁次
                column.Item().Row(row =>
                {
                    row.RelativeItem().Text($"Tel：{_defaultCustomerTel}");
                    row.RelativeItem().Text($"Fax：{_defaultCustomerFax}");
                    row.ConstantItem(120).AlignRight().Text($"頁次：{currentPage}/{totalPages}");
                });

                // 第三行：客戶/廠商名稱、日期
                column.Item().Row(row =>
                {
                    row.RelativeItem().Text($"{clientLabel}：{request.CustomerName}");
                    row.ConstantItem(200).AlignRight().Text($"貨單日期：{request.Date}");
                });

                // 第四行：電話、傳真、編號
                column.Item().Row(row =>
                {
                    row.RelativeItem().Text($"連絡電話：{request.Phone}");
                    row.RelativeItem().Text($"傳真號碼：{request.Fax}");
                    row.ConstantItem(200).AlignRight().Text($"貨單編號：{request.OrderNumber}");
                });

                // 第五行：送貨地址
                column.Item().Text($"送貨地址：{request.Address}");

                // 分隔線
                column.Item().PaddingVertical(5).LineHorizontal(1);
            });
        }

        /// <summary>
        /// 繪製分頁內容表格
        /// </summary>
        private void ComposeContentPaged(IContainer container, PrintInvoiceRequest request, int startIndex, int itemCount, bool includeFooterInMiddle)
        {
            var isPurchaseOrder = request.OrderType == "採購單";
            int columnCount = isPurchaseOrder ? 5 : 7;

            container.Column(column =>
            {
                // 表格部分
                column.Item().Table(table =>
                {
                    // 定義欄位
                    if (isPurchaseOrder)
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(1.5f); // 編號
                            columns.RelativeColumn(4f);   // 品名
                            columns.RelativeColumn(1f);   // 數量
                            columns.RelativeColumn(1f);   // 單位
                            columns.RelativeColumn(2f);   // 備註
                        });
                    }
                    else
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(1.5f); // 編號
                            columns.RelativeColumn(3f);   // 品名
                            columns.RelativeColumn(1f);   // 數量
                            columns.RelativeColumn(1f);   // 單位
                            columns.RelativeColumn(1f);   // 單價
                            columns.RelativeColumn(1f);   // 金額
                            columns.RelativeColumn(1.5f); // 備註
                        });
                    }

                    // 表頭
                    table.Header(header =>
                    {
                        header.Cell().BorderBottom(1).Padding(5).Text("編號").Bold();
                        header.Cell().BorderBottom(1).Padding(5).Text("品名").Bold();
                        header.Cell().BorderBottom(1).Padding(5).AlignRight().Text("數量").Bold();
                        header.Cell().BorderBottom(1).Padding(5).Text("單位").Bold();

                        if (isPurchaseOrder)
                        {
                            header.Cell().BorderBottom(1).Padding(5).Text("備註").Bold();
                        }
                        else
                        {
                            header.Cell().BorderBottom(1).Padding(5).AlignRight().Text("單價").Bold();
                            header.Cell().BorderBottom(1).Padding(5).AlignRight().Text("金額").Bold();
                            header.Cell().BorderBottom(1).Padding(5).Text("備註").Bold();
                        }
                    });

                    // 資料列（分頁）
                    int rowCounter = 0;
                    if (isPurchaseOrder && request.PurchaseOrderDetails != null)
                    {
                        for (int i = startIndex; i < startIndex + itemCount && i < request.PurchaseOrderDetails.Count; i++)
                        {
                            var detail = request.PurchaseOrderDetails[i];
                            bool needSeparator = (rowCounter > 0) && (rowCounter % 5 == 0);

                            // 每 5 筆加一條分隔線
                            table.Cell().BorderTop(needSeparator ? 0.5f : 0).Padding(3).Text(detail.ProductCode ?? "").ClampLines(1);
                            table.Cell().BorderTop(needSeparator ? 0.5f : 0).Padding(3).Text(detail.ProductName ?? "").ClampLines(1);
                            table.Cell().BorderTop(needSeparator ? 0.5f : 0).Padding(3).AlignRight().Text(detail.Quantity ?? "").ClampLines(1);
                            table.Cell().BorderTop(needSeparator ? 0.5f : 0).Padding(3).Text(detail.Unit ?? "").ClampLines(1);
                            table.Cell().BorderTop(needSeparator ? 0.5f : 0).Padding(3).Text(detail.Remark ?? "").ClampLines(1);

                            rowCounter++;
                        }
                    }
                    else if (request.Details != null)
                    {
                        for (int i = startIndex; i < startIndex + itemCount && i < request.Details.Count; i++)
                        {
                            var detail = request.Details[i];
                            bool needSeparator = (rowCounter > 0) && (rowCounter % 5 == 0);

                            // 每 5 筆加一條分隔線
                            table.Cell().BorderTop(needSeparator ? 0.5f : 0).Padding(3).Text(detail.ProductCode ?? "").ClampLines(1);
                            table.Cell().BorderTop(needSeparator ? 0.5f : 0).Padding(3).Text(detail.ProductName ?? "").ClampLines(1);
                            table.Cell().BorderTop(needSeparator ? 0.5f : 0).Padding(3).AlignRight().Text(detail.Quantity ?? "").ClampLines(1);
                            table.Cell().BorderTop(needSeparator ? 0.5f : 0).Padding(3).Text(detail.Unit ?? "").ClampLines(1);
                            table.Cell().BorderTop(needSeparator ? 0.5f : 0).Padding(3).AlignRight().Text(detail.UnitPrice ?? "").ClampLines(1);
                            table.Cell().BorderTop(needSeparator ? 0.5f : 0).Padding(3).AlignRight().Text(detail.Amount ?? "").ClampLines(1);
                            table.Cell().BorderTop(needSeparator ? 0.5f : 0).Padding(3).Text(detail.Remark ?? "").ClampLines(1);

                            rowCounter++;
                        }
                    }
                });

                // 如果需要在中間顯示表尾（≤SmallPageThreshold筆時）
                if (includeFooterInMiddle)
                {
                    // 加入一些垂直間距，讓表尾顯示在表格下方（中間位置）
                    column.Item().PaddingTop(20).Column(footerColumn =>
                    {
                        footerColumn.Item().LineHorizontal(1);
                        footerColumn.Item().PaddingTop(5).Row(row =>
                        {
                            row.RelativeItem().Text($"備註：{request.Remark}");
                            if (!string.IsNullOrEmpty(request.TotalAmount) && request.TotalAmount != "0")
                            {
                                row.ConstantItem(150).AlignRight().Text($"總計：{request.TotalAmount}").Bold();
                            }
                        });
                    });
                }
            });
        }

        /// <summary>
        /// 繪製表尾（固定在頁面底部）
        /// </summary>
        private void ComposeFooter(IContainer container, PrintInvoiceRequest request)
        {
            container.Column(column =>
            {
                column.Item().PaddingTop(10).LineHorizontal(1);

                column.Item().Row(row =>
                {
                    row.RelativeItem().Text($"備註：{request.Remark}");
                    if (!string.IsNullOrEmpty(request.TotalAmount) && request.TotalAmount != "0")
                    {
                        row.ConstantItem(150).AlignRight().Text($"總計：{request.TotalAmount}").Bold();
                    }
                });
            });
        }

        #endregion
    }
}
