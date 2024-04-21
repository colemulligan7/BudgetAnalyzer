using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetAnalyzer.Migrations
{
    /// <inheritdoc />
    public partial class NameForMappings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TransactionFileMappings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "TransactionFileMappings");
        }
    }
}
