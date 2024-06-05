using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "app_settings",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    theme = table.Column<string>(type: "TEXT", nullable: true, defaultValue: "System"),
                    keep_screen_on = table.Column<bool>(type: "INTEGER", nullable: true, defaultValue: false),
                    lock_screen_orientation = table.Column<bool>(type: "INTEGER", nullable: true, defaultValue: false),
                    first_day_of_week = table.Column<string>(type: "TEXT", nullable: true, defaultValue: "Sunday"),
                    weight_unit = table.Column<string>(type: "TEXT", nullable: true, defaultValue: "Pound"),
                    distance_unit = table.Column<int>(type: "INTEGER", nullable: true),
                    height_unit = table.Column<string>(type: "TEXT", nullable: true, defaultValue: "Inch"),
                    one_rep_max_strategy = table.Column<string>(type: "TEXT", nullable: true, defaultValue: "Brzycki"),
                    show_assistance_exercises = table.Column<bool>(type: "INTEGER", nullable: true, defaultValue: true),
                    show_reps_to_beat_1rm = table.Column<bool>(type: "INTEGER", nullable: true, defaultValue: true),
                    show_reps_to_beat_pr = table.Column<bool>(type: "INTEGER", nullable: true, defaultValue: true),
                    supination_strategy = table.Column<string>(type: "TEXT", nullable: true, defaultValue: "None")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_settings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "barbells",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    weight_kg = table.Column<decimal>(type: "NUMERIC", nullable: false, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_barbells", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "exercises",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    barbell = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exercises", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "plates",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    weight_kg = table.Column<decimal>(type: "NUMERIC", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "workouts",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    started_at = table.Column<string>(type: "TEXT", nullable: false),
                    stopped_at = table.Column<string>(type: "TEXT", nullable: false),
                    notes = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workouts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SetGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExerciseId = table.Column<int>(type: "INTEGER", nullable: false),
                    WorkoutId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SetGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SetGroup_exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "exercises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SetGroup_workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "workouts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Set",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SetGroupId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Set", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Set_SetGroup_SetGroupId",
                        column: x => x.SetGroupId,
                        principalTable: "SetGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_barbells_name",
                table: "barbells",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_plates_weight_kg",
                table: "plates",
                column: "weight_kg",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Set_SetGroupId",
                table: "Set",
                column: "SetGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SetGroup_ExerciseId",
                table: "SetGroup",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_SetGroup_WorkoutId",
                table: "SetGroup",
                column: "WorkoutId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app_settings");

            migrationBuilder.DropTable(
                name: "barbells");

            migrationBuilder.DropTable(
                name: "plates");

            migrationBuilder.DropTable(
                name: "Set");

            migrationBuilder.DropTable(
                name: "SetGroup");

            migrationBuilder.DropTable(
                name: "exercises");

            migrationBuilder.DropTable(
                name: "workouts");
        }
    }
}
