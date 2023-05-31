using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WomensTechForum2._0.Migrations
{
    /// <inheritdoc />
    public partial class Add_PtAnswerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PTAnswerId",
                table: "PostThread",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PTAnswerId",
                table: "PostThread");
        }
    }
}
