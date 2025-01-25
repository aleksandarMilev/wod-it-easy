#nullable disable
namespace WodItEasy.Infrastructure.Persistence.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class AuditInfoAndSoftDeletion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participation_Athletes_AthleteId",
                table: "Participation");

            migrationBuilder.DropForeignKey(
                name: "FK_Participation_Workouts_WorkoutId",
                table: "Participation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Participation",
                table: "Participation");

            migrationBuilder.RenameTable(
                name: "Participation",
                newName: "Participations");

            migrationBuilder.RenameIndex(
                name: "IX_Participation_WorkoutId",
                table: "Participations",
                newName: "IX_Participations_WorkoutId");

            migrationBuilder.RenameIndex(
                name: "IX_Participation_AthleteId",
                table: "Participations",
                newName: "IX_Participations_AthleteId");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Workouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Workouts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Workouts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Workouts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Workouts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Workouts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Workouts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Athletes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Athletes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Athletes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Athletes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Athletes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Athletes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Athletes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Participations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Participations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Participations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Participations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Participations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Participations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Participations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Participations",
                table: "Participations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_Athletes_AthleteId",
                table: "Participations",
                column: "AthleteId",
                principalTable: "Athletes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_Workouts_WorkoutId",
                table: "Participations",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participations_Athletes_AthleteId",
                table: "Participations");

            migrationBuilder.DropForeignKey(
                name: "FK_Participations_Workouts_WorkoutId",
                table: "Participations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Participations",
                table: "Participations");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Athletes");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Athletes");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Athletes");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Athletes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Athletes");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Athletes");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Athletes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Participations");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Participations");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Participations");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Participations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Participations");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Participations");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Participations");

            migrationBuilder.RenameTable(
                name: "Participations",
                newName: "Participation");

            migrationBuilder.RenameIndex(
                name: "IX_Participations_WorkoutId",
                table: "Participation",
                newName: "IX_Participation_WorkoutId");

            migrationBuilder.RenameIndex(
                name: "IX_Participations_AthleteId",
                table: "Participation",
                newName: "IX_Participation_AthleteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Participation",
                table: "Participation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participation_Athletes_AthleteId",
                table: "Participation",
                column: "AthleteId",
                principalTable: "Athletes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Participation_Workouts_WorkoutId",
                table: "Participation",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
