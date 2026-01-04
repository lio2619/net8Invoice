using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invoicing.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerOrderIdToOrderDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomerOrder_NewOrderNumber",
                table: "CustomerOrder");

            migrationBuilder.AlterColumn<string>(
                name: "OrderNumber",
                table: "OrderDetail",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                comment: "單子編號（舊資料關聯用）",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldComment: "單子編號");

            migrationBuilder.AddColumn<int>(
                name: "CustomerOrderId",
                table: "OrderDetail",
                type: "integer",
                nullable: true,
                comment: "CustomerOrder Id（新資料關聯用）");

            migrationBuilder.AlterColumn<string>(
                name: "OrderNumber",
                table: "CustomerOrder",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                comment: "單子編號",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldComment: "單子編號");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrder_NewOrderNumber",
                table: "CustomerOrder",
                column: "NewOrderNumber",
                filter: "\"NewOrderNumber\" IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomerOrder_NewOrderNumber",
                table: "CustomerOrder");

            migrationBuilder.DropColumn(
                name: "CustomerOrderId",
                table: "OrderDetail");

            migrationBuilder.AlterColumn<string>(
                name: "OrderNumber",
                table: "OrderDetail",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "單子編號",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "單子編號（舊資料關聯用）");

            migrationBuilder.AlterColumn<string>(
                name: "OrderNumber",
                table: "CustomerOrder",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                comment: "單子編號",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "單子編號");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrder_NewOrderNumber",
                table: "CustomerOrder",
                column: "NewOrderNumber",
                unique: true,
                filter: "\"NewOrderNumber\" IS NOT NULL");
        }
    }
}
