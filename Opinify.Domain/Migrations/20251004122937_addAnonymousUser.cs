using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Opinify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addAnonymousUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnonymousId",
                table: "Votes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnonymousId",
                table: "Votes");
        }
    }
}
