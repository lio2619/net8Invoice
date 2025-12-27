using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invoicing.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderNumberExclusiveConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Supplier_UniqueId",
                table: "Supplier");

            migrationBuilder.DropIndex(
                name: "IX_SuggestedPrice_UniqueId",
                table: "SuggestedPrice");

            migrationBuilder.DropIndex(
                name: "IX_Product_UniqueId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetail_UniqueId",
                table: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_CustomerOrder_OrderNumber",
                table: "CustomerOrder");

            migrationBuilder.DropIndex(
                name: "IX_CustomerOrder_UniqueId",
                table: "CustomerOrder");

            migrationBuilder.DropIndex(
                name: "IX_Customer_UniqueId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "SuggestedPrice");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "SuggestedPrice");

            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "SuggestedPrice");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CustomerOrder");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "CustomerOrder");

            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "CustomerOrder");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "Customer");

            migrationBuilder.AddColumn<string>(
                name: "NewOrderNumber",
                table: "CustomerOrder",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                comment: "新單子編號");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrder_NewOrderNumber",
                table: "CustomerOrder",
                column: "NewOrderNumber",
                unique: true,
                filter: "\"NewOrderNumber\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrder_OrderNumber",
                table: "CustomerOrder",
                column: "OrderNumber",
                unique: true,
                filter: "\"OrderNumber\" IS NOT NULL");

            migrationBuilder.AddCheckConstraint(
                name: "CK_OrderNumber_Exclusive",
                table: "CustomerOrder",
                sql: "(\"OrderNumber\" IS NOT NULL AND \"NewOrderNumber\" IS NULL) OR (\"OrderNumber\" IS NULL AND \"NewOrderNumber\" IS NOT NULL)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomerOrder_NewOrderNumber",
                table: "CustomerOrder");

            migrationBuilder.DropIndex(
                name: "IX_CustomerOrder_OrderNumber",
                table: "CustomerOrder");

            migrationBuilder.DropCheckConstraint(
                name: "CK_OrderNumber_Exclusive",
                table: "CustomerOrder");

            migrationBuilder.DropColumn(
                name: "NewOrderNumber",
                table: "CustomerOrder");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Supplier",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Supplier",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UniqueId",
                table: "Supplier",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "SuggestedPrice",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "SuggestedPrice",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UniqueId",
                table: "SuggestedPrice",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Product",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Product",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UniqueId",
                table: "Product",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "OrderDetail",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "OrderDetail",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UniqueId",
                table: "OrderDetail",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CustomerOrder",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "CustomerOrder",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UniqueId",
                table: "CustomerOrder",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Customer",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "Customer",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UniqueId",
                table: "Customer",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_UniqueId",
                table: "Supplier",
                column: "UniqueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SuggestedPrice_UniqueId",
                table: "SuggestedPrice",
                column: "UniqueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_UniqueId",
                table: "Product",
                column: "UniqueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_UniqueId",
                table: "OrderDetail",
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
                name: "IX_Customer_UniqueId",
                table: "Customer",
                column: "UniqueId",
                unique: true);
        }
    }
}
