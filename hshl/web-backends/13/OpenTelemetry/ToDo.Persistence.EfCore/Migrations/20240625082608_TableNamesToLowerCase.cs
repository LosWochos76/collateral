using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoManager.Persistence.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class TableNamesToLowerCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToDos",
                table: "ToDos");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "ToDos",
                newName: "todos");

            migrationBuilder.RenameColumn(
                name: "PasswordResetToken",
                table: "users",
                newName: "passwordresettoken");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "users",
                newName: "passwordhash");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "users",
                newName: "lastname");

            migrationBuilder.RenameColumn(
                name: "IsAdmin",
                table: "users",
                newName: "isadmin");

            migrationBuilder.RenameColumn(
                name: "Firstname",
                table: "users",
                newName: "firstname");

            migrationBuilder.RenameColumn(
                name: "EMail",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "todos",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "todos",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Completion",
                table: "todos",
                newName: "completion");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "todos",
                newName: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_users",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_todos",
                table: "todos",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_todos",
                table: "todos");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "todos",
                newName: "ToDos");

            migrationBuilder.RenameColumn(
                name: "passwordresettoken",
                table: "Users",
                newName: "PasswordResetToken");

            migrationBuilder.RenameColumn(
                name: "passwordhash",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "lastname",
                table: "Users",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "isadmin",
                table: "Users",
                newName: "IsAdmin");

            migrationBuilder.RenameColumn(
                name: "firstname",
                table: "Users",
                newName: "Firstname");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "EMail");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "ToDos",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "ToDos",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "completion",
                table: "ToDos",
                newName: "Completion");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ToDos",
                newName: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToDos",
                table: "ToDos",
                column: "ID");
        }
    }
}
