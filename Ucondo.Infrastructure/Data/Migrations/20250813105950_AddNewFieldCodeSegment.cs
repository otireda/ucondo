using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ucondo.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFieldCodeSegment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CodeSegment",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeSegment",
                table: "Accounts");
        }
    }
}
