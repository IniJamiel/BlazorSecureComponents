using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BEAPIDemo.Migrations
{
    /// <inheritdoc />
    public partial class baru2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Locked",
                table: "userBases",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Locked",
                table: "userBases");
        }
    }
}
