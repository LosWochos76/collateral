using Microsoft.EntityFrameworkCore.Migrations;

namespace SeminarManager.EF.Migrations
{
    public partial class RelationsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Seminars",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "PersonID",
                table: "Seminars",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeacherID",
                table: "Seminars",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Persons",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "Attendees",
                columns: table => new
                {
                    SeminarID = table.Column<int>(type: "integer", nullable: false),
                    PersonID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Attendees_Persons_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Persons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendees_Seminars_SeminarID",
                        column: x => x.SeminarID,
                        principalTable: "Seminars",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seminars_PersonID",
                table: "Seminars",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Seminars_TeacherID",
                table: "Seminars",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_PersonID",
                table: "Attendees",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_Attendees_SeminarID",
                table: "Attendees",
                column: "SeminarID");

            migrationBuilder.AddForeignKey(
                name: "FK_Seminars_Persons_PersonID",
                table: "Seminars",
                column: "PersonID",
                principalTable: "Persons",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Seminars_Persons_TeacherID",
                table: "Seminars",
                column: "TeacherID",
                principalTable: "Persons",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seminars_Persons_PersonID",
                table: "Seminars");

            migrationBuilder.DropForeignKey(
                name: "FK_Seminars_Persons_TeacherID",
                table: "Seminars");

            migrationBuilder.DropTable(
                name: "Attendees");

            migrationBuilder.DropIndex(
                name: "IX_Seminars_PersonID",
                table: "Seminars");

            migrationBuilder.DropIndex(
                name: "IX_Seminars_TeacherID",
                table: "Seminars");

            migrationBuilder.DropColumn(
                name: "PersonID",
                table: "Seminars");

            migrationBuilder.DropColumn(
                name: "TeacherID",
                table: "Seminars");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Seminars",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Persons",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
