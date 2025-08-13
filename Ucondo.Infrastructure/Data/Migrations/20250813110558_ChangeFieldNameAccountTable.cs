using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ucondo.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFieldNameAccountTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CodeSegment",
                table: "Accounts",
                newName: "Depth");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Depth",
                table: "Accounts",
                newName: "CodeSegment");
        }
    }
}
