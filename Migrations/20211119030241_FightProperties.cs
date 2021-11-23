using Microsoft.EntityFrameworkCore.Migrations;

namespace _netCourse.Migrations
{
    public partial class FightProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterSkills_skills_skillId",
                table: "CharacterSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharacterSkills",
                table: "CharacterSkills");

            migrationBuilder.DropIndex(
                name: "IX_CharacterSkills_skillId",
                table: "CharacterSkills");

            migrationBuilder.RenameColumn(
                name: "skillId",
                table: "CharacterSkills",
                newName: "SkillId");

            migrationBuilder.AddColumn<int>(
                name: "Fights",
                table: "characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Victories",
                table: "characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "defeats",
                table: "characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharacterSkills",
                table: "CharacterSkills",
                columns: new[] { "SkillId", "charactersId" });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkills_charactersId",
                table: "CharacterSkills",
                column: "charactersId");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterSkills_skills_SkillId",
                table: "CharacterSkills",
                column: "SkillId",
                principalTable: "skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterSkills_skills_SkillId",
                table: "CharacterSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharacterSkills",
                table: "CharacterSkills");

            migrationBuilder.DropIndex(
                name: "IX_CharacterSkills_charactersId",
                table: "CharacterSkills");

            migrationBuilder.DropColumn(
                name: "Fights",
                table: "characters");

            migrationBuilder.DropColumn(
                name: "Victories",
                table: "characters");

            migrationBuilder.DropColumn(
                name: "defeats",
                table: "characters");

            migrationBuilder.RenameColumn(
                name: "SkillId",
                table: "CharacterSkills",
                newName: "skillId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharacterSkills",
                table: "CharacterSkills",
                columns: new[] { "charactersId", "skillId" });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSkills_skillId",
                table: "CharacterSkills",
                column: "skillId");

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterSkills_skills_skillId",
                table: "CharacterSkills",
                column: "skillId",
                principalTable: "skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
