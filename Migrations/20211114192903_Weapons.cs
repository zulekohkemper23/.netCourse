using Microsoft.EntityFrameworkCore.Migrations;

namespace _netCourse.Migrations
{
    public partial class Weapons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "weapons",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Damage = table.Column<int>(type: "int", nullable: false),
                    CharacterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weapons", x => x.id);
                    table.ForeignKey(
                        name: "FK_weapons_characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_weapons_CharacterId",
                table: "weapons",
                column: "CharacterId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "weapons");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "users",
                newName: "usernam");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "users",
                newName: "Password");
        }
    }
}
