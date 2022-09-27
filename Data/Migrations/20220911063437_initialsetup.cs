using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LensZone.Data.Migrations
{
    public partial class initialsetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<int>(type: "int", nullable: false),
                    LeftEye = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RightEye = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lens", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lens");
        }
    }
}
