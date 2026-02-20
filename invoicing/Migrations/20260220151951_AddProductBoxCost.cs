using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invoicing.Migrations
{
    /// <inheritdoc />
    public partial class AddProductBoxCost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BoxCost",
                table: "Product",
                type: "numeric",
                nullable: true,
                comment: "箱購成本");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoxCost",
                table: "Product");
        }
    }
}
