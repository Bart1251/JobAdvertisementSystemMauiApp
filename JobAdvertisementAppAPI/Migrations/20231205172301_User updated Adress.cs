using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobAdvertisementAppAPI.Migrations
{
    /// <inheritdoc />
    public partial class UserupdatedAdress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "User");
        }
    }
}
