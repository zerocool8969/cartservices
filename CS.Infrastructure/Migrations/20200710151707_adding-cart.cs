using Microsoft.EntityFrameworkCore.Migrations;

namespace CS.Infrastructure.Migrations
{
    public partial class addingcart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckOutId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    NewQuantity = table.Column<int>(nullable: false),
                    TotalAmount = table.Column<int>(nullable: false),
                    OldQuantity = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CheckOuts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZipCode = table.Column<string>(nullable: false),
                    Mobile = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckOuts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "CheckOuts");
        }
    }
}
