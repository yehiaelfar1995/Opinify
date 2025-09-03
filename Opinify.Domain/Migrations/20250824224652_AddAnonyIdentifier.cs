using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Opinify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAnonyIdentifier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnonymousIdentifier",
                table: "Polls",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnonymousIdentifier",
                table: "Polls");
        }
    }
}
