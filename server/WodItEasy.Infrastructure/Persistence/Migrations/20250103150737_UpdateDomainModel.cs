#nullable disable
namespace WodItEasy.Infrastructure.Persistence.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class UpdateDomainModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_Athletes_AthleteId",
                table: "Memberships");

            migrationBuilder.DropTable(
                name: "AthletesWorkouts");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Athletes");

            migrationBuilder.DropColumn(
                name: "PhoneNumber_Number",
                table: "Athletes");

            migrationBuilder.AddColumn<int>(
                name: "WorkoutId",
                table: "Athletes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Athletes_WorkoutId",
                table: "Athletes",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Athletes_Workouts_WorkoutId",
                table: "Athletes",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_Athletes_AthleteId",
                table: "Memberships",
                column: "AthleteId",
                principalTable: "Athletes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Athletes_Workouts_WorkoutId",
                table: "Athletes");

            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_Athletes_AthleteId",
                table: "Memberships");

            migrationBuilder.DropIndex(
                name: "IX_Athletes_WorkoutId",
                table: "Athletes");

            migrationBuilder.DropColumn(
                name: "WorkoutId",
                table: "Athletes");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Athletes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber_Number",
                table: "Athletes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_Athletes_AthleteId",
                table: "Memberships",
                column: "AthleteId",
                principalTable: "Athletes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
