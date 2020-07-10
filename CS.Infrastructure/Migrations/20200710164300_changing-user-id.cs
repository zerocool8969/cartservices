using Microsoft.EntityFrameworkCore.Migrations;

namespace CS.Infrastructure.Migrations
{
    public partial class changinguserid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CheckOuts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CheckOuts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
