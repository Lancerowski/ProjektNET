using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektNET.Migrations
{
    /// <inheritdoc />
    public partial class SyncModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWorkoutClass");

            migrationBuilder.AddColumn<int>(
                name: "WorkoutClassId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_WorkoutClassId",
                table: "Users",
                column: "WorkoutClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_WorkoutClasses_WorkoutClassId",
                table: "Users",
                column: "WorkoutClassId",
                principalTable: "WorkoutClasses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_WorkoutClasses_WorkoutClassId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_WorkoutClassId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WorkoutClassId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "UserWorkoutClass",
                columns: table => new
                {
                    UsersId = table.Column<int>(type: "int", nullable: false),
                    WorkoutClassesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorkoutClass", x => new { x.UsersId, x.WorkoutClassesId });
                    table.ForeignKey(
                        name: "FK_UserWorkoutClass_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWorkoutClass_WorkoutClasses_WorkoutClassesId",
                        column: x => x.WorkoutClassesId,
                        principalTable: "WorkoutClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkoutClass_WorkoutClassesId",
                table: "UserWorkoutClass",
                column: "WorkoutClassesId");
        }
    }
}
