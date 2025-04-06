using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbySalto.Mid.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexesForProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "Idx_Price",
                table: "Products",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "Idx_Rating",
                table: "Products",
                column: "Rating");

            migrationBuilder.CreateIndex(
                name: "Idx_Title",
                table: "Products",
                column: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "Idx_Price",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "Idx_Rating",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "Idx_Title",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
