﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WodItEasy.Infrastructure.Persistence;

#nullable disable

namespace WodItEasy.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(WodItEasyDbContext))]
    [Migration("20241225181109_InitMigration")]
    partial class InitMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AthleteWorkouts", b =>
                {
                    b.Property<int>("AthleteId")
                        .HasColumnType("int");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("int");

                    b.HasKey("AthleteId", "WorkoutId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("AthletesWorkouts", (string)null);
                });

            modelBuilder.Entity("WodItEasy.Domain.Models.Athletes.Athlete", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Athletes");
                });

            modelBuilder.Entity("WodItEasy.Domain.Models.Athletes.Membership", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AthleteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartsAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("WorkoutsCount")
                        .HasColumnType("int");

                    b.Property<int>("WorkoutsLeft")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AthleteId")
                        .IsUnique()
                        .HasFilter("[AthleteId] IS NOT NULL");

                    b.ToTable("Memberships");
                });

            modelBuilder.Entity("WodItEasy.Domain.Models.Workouts.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CurrentParticipantsCount")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("MaxParticipantsCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("StartsAtDate")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("StartsAtTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("AthleteWorkouts", b =>
                {
                    b.HasOne("WodItEasy.Domain.Models.Athletes.Athlete", null)
                        .WithMany()
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WodItEasy.Domain.Models.Workouts.Workout", null)
                        .WithMany()
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WodItEasy.Domain.Models.Athletes.Athlete", b =>
                {
                    b.OwnsOne("WodItEasy.Domain.Models.Athletes.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<int>("AthleteId")
                                .HasColumnType("int");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar(20)");

                            b1.HasKey("AthleteId");

                            b1.ToTable("Athletes");

                            b1.WithOwner()
                                .HasForeignKey("AthleteId");
                        });

                    b.Navigation("PhoneNumber")
                        .IsRequired();
                });

            modelBuilder.Entity("WodItEasy.Domain.Models.Athletes.Membership", b =>
                {
                    b.HasOne("WodItEasy.Domain.Models.Athletes.Athlete", null)
                        .WithOne("Membership")
                        .HasForeignKey("WodItEasy.Domain.Models.Athletes.Membership", "AthleteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("WodItEasy.Domain.Models.Athletes.MembershipType", "Type", b1 =>
                        {
                            b1.Property<int>("MembershipId")
                                .HasColumnType("int");

                            b1.Property<int>("Value")
                                .HasColumnType("int");

                            b1.HasKey("MembershipId");

                            b1.ToTable("Memberships");

                            b1.WithOwner()
                                .HasForeignKey("MembershipId");
                        });

                    b.Navigation("Type")
                        .IsRequired();
                });

            modelBuilder.Entity("WodItEasy.Domain.Models.Workouts.Workout", b =>
                {
                    b.OwnsOne("WodItEasy.Domain.Models.Workouts.WorkoutType", "Type", b1 =>
                        {
                            b1.Property<int>("WorkoutId")
                                .HasColumnType("int");

                            b1.Property<int>("Value")
                                .HasColumnType("int");

                            b1.HasKey("WorkoutId");

                            b1.ToTable("Workouts");

                            b1.WithOwner()
                                .HasForeignKey("WorkoutId");
                        });

                    b.Navigation("Type")
                        .IsRequired();
                });

            modelBuilder.Entity("WodItEasy.Domain.Models.Athletes.Athlete", b =>
                {
                    b.Navigation("Membership");
                });
#pragma warning restore 612, 618
        }
    }
}
