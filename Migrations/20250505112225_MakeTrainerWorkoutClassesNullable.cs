using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektNET.Migrations
{
    /// <inheritdoc />
    public partial class MakeTrainerWorkoutClassesNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainerWorkoutClass");

            migrationBuilder.AddColumn<int>(
                name: "WorkoutClassId",
                table: "Trainers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_WorkoutClassId",
                table: "Trainers",
                column: "WorkoutClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainers_WorkoutClasses_WorkoutClassId",
                table: "Trainers",
                column: "WorkoutClassId",
                principalTable: "WorkoutClasses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainers_WorkoutClasses_WorkoutClassId",
                table: "Trainers");

            migrationBuilder.DropIndex(
                name: "IX_Trainers_WorkoutClassId",
                table: "Trainers");

            migrationBuilder.DropColumn(
                name: "WorkoutClassId",
                table: "Trainers");

            migrationBuilder.CreateTable(
                name: "TrainerWorkoutClass",
                columns: table => new
                {
                    TrainersId = table.Column<int>(type: "int", nullable: false),
                    WorkoutClassesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerWorkoutClass", x => new { x.TrainersId, x.WorkoutClassesId });
                    table.ForeignKey(
                        name: "FK_TrainerWorkoutClass_Trainers_TrainersId",
                        column: x => x.TrainersId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainerWorkoutClass_WorkoutClasses_WorkoutClassesId",
                        column: x => x.WorkoutClassesId,
                        principalTable: "WorkoutClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainerWorkoutClass_WorkoutClassesId",
                table: "TrainerWorkoutClass",
                column: "WorkoutClassesId");
        }
    }
}
