#nullable disable
namespace WodItEasy.Infrastructure.Persistence.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Athletes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PhoneNumber_Number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Athletes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MaxParticipantsCount = table.Column<int>(type: "int", nullable: false),
                    CurrentParticipantsCount = table.Column<int>(type: "int", nullable: false),
                    StartsAtDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartsAtTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Type_Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Memberships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type_Value = table.Column<int>(type: "int", nullable: false),
                    WorkoutsCount = table.Column<int>(type: "int", nullable: false),
                    WorkoutsLeft = table.Column<int>(type: "int", nullable: false),
                    StartsAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AthleteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Memberships_Athletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Athletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AthletesWorkouts",
                columns: table => new
                {
                    AthleteId = table.Column<int>(type: "int", nullable: false),
                    WorkoutId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AthletesWorkouts", x => new { x.AthleteId, x.WorkoutId });
                    table.ForeignKey(
                        name: "FK_AthletesWorkouts_Athletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Athletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AthletesWorkouts_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AthletesWorkouts_WorkoutId",
                table: "AthletesWorkouts",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_AthleteId",
                table: "Memberships",
                column: "AthleteId",
                unique: true,
                filter: "[AthleteId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AthletesWorkouts");

            migrationBuilder.DropTable(
                name: "Memberships");

            migrationBuilder.DropTable(
                name: "Workouts");

            migrationBuilder.DropTable(
                name: "Athletes");
        }
    }
}
