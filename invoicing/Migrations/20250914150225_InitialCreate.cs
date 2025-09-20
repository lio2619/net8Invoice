using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace invoicing.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyFullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "公司全名"),
                    FaxNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true, comment: "傳真號碼"),
                    CompanyCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true, comment: "公司編號"),
                    RegisteredAddress = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, comment: "登記地址"),
                    InvoiceTitle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, comment: "發票抬頭"),
                    TaxId = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true, comment: "統一編號"),
                    ContactPerson1 = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true, comment: "聯絡人一"),
                    Phone1 = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true, comment: "聯絡電話一"),
                    Phone2 = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true, comment: "聯絡電話二"),
                    DeliveryAddress = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, comment: "送貨地址"),
                    DeliveryZipCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true, comment: "送貨地郵遞區號"),
                    UniqueId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, comment: "單子編號"),
                    Remark = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true, comment: "備註"),
                    Deleted = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true, comment: "刪除"),
                    OrderName = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true, comment: "單子"),
                    Customer = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true, comment: "客戶"),
                    Date = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true, comment: "日期"),
                    Time = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: true, comment: "時間"),
                    TotalAmount = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, comment: "總金額"),
                    Customs = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true, comment: "關貿"),
                    UniqueId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Remark = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true, comment: "備註"),
                    ProductName = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true, comment: "品名"),
                    UnitPrice = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, comment: "單價"),
                    OrderNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, comment: "單子編號"),
                    Unit = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true, comment: "基本單位"),
                    Quantity = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true, comment: "數量"),
                    ProductCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true, comment: "貨品編號"),
                    Amount = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, comment: "金額"),
                    UniqueId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, comment: "貨品編號"),
                    ProductName = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true, comment: "品名"),
                    PriceA = table.Column<decimal>(type: "numeric", nullable: true, comment: "售價A"),
                    PriceB = table.Column<decimal>(type: "numeric", nullable: true, comment: "售價B"),
                    PriceC = table.Column<decimal>(type: "numeric", nullable: true, comment: "售價C"),
                    PriceD = table.Column<decimal>(type: "numeric", nullable: true, comment: "售價D"),
                    PriceE = table.Column<decimal>(type: "numeric", nullable: true, comment: "售價E"),
                    Unit = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true, comment: "基本單位"),
                    StandardPrice = table.Column<decimal>(type: "numeric", nullable: true, comment: "標準售價"),
                    StandardCost = table.Column<decimal>(type: "numeric", nullable: true, comment: "標準成本"),
                    CurrentCost = table.Column<decimal>(type: "numeric", nullable: true, comment: "現行成本"),
                    UniqueId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuggestedPrice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StandardPrice = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, comment: "標準售價"),
                    SuggestedSalePrice = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, comment: "建議售價"),
                    UniqueId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuggestedPrice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyFullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: "公司全名"),
                    Remark = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, comment: "備註"),
                    FaxNumber = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true, comment: "傳真號碼"),
                    CompanyShortName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true, comment: "公司簡稱"),
                    CompanyCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true, comment: "公司編號"),
                    BillingAddress = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, comment: "帳單地址"),
                    RegisteredAddress = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, comment: "登記地址"),
                    InvoiceTitle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, comment: "發票抬頭"),
                    TaxId = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true, comment: "統一編號"),
                    ContactPerson1 = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true, comment: "聯絡人一"),
                    Phone1 = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true, comment: "聯絡電話一"),
                    ResponsiblePerson = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true, comment: "負責人"),
                    DeliveryAddress = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true, comment: "送貨地址"),
                    UniqueId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UniqueId",
                table: "Customer",
                column: "UniqueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrder_OrderNumber",
                table: "CustomerOrder",
                column: "OrderNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrder_UniqueId",
                table: "CustomerOrder",
                column: "UniqueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_UniqueId",
                table: "OrderDetail",
                column: "UniqueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductCode",
                table: "Product",
                column: "ProductCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_UniqueId",
                table: "Product",
                column: "UniqueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SuggestedPrice_UniqueId",
                table: "SuggestedPrice",
                column: "UniqueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_UniqueId",
                table: "Supplier",
                column: "UniqueId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "CustomerOrder");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "SuggestedPrice");

            migrationBuilder.DropTable(
                name: "Supplier");
        }
    }
}
