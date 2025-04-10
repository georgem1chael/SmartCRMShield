using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartCRMShield.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBlockedField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "Transactions",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "Transactions");
        }
    }
}
