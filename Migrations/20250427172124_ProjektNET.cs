using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektNET.Migrations
{
    /// <inheritdoc />
    public partial class ProjektNET : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Schedule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutClasses", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "TrainerWorkoutClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainerId = table.Column<int>(type: "int", nullable: false),
                    WorkoutClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerWorkoutClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerWorkoutClasses_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainerWorkoutClasses_WorkoutClasses_WorkoutClassId",
                        column: x => x.WorkoutClassId,
                        principalTable: "WorkoutClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "UserWorkoutClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WorkoutClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorkoutClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWorkoutClasses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWorkoutClasses_WorkoutClasses_WorkoutClassId",
                        column: x => x.WorkoutClassId,
                        principalTable: "WorkoutClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainerWorkoutClass_WorkoutClassesId",
                table: "TrainerWorkoutClass",
                column: "WorkoutClassesId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerWorkoutClasses_TrainerId",
                table: "TrainerWorkoutClasses",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerWorkoutClasses_WorkoutClassId",
                table: "TrainerWorkoutClasses",
                column: "WorkoutClassId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkoutClass_WorkoutClassesId",
                table: "UserWorkoutClass",
                column: "WorkoutClassesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkoutClasses_UserId",
                table: "UserWorkoutClasses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkoutClasses_WorkoutClassId",
                table: "UserWorkoutClasses",
                column: "WorkoutClassId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainerWorkoutClass");

            migrationBuilder.DropTable(
                name: "TrainerWorkoutClasses");

            migrationBuilder.DropTable(
                name: "UserWorkoutClass");

            migrationBuilder.DropTable(
                name: "UserWorkoutClasses");

            migrationBuilder.DropTable(
                name: "Trainers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WorkoutClasses");
        }
    }
}
