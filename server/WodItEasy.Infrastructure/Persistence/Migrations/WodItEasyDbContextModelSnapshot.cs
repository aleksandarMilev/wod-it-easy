﻿#nullable disable
namespace WodItEasy.Infrastructure.Persistence.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using WodItEasy.Infrastructure.Persistence;

    [DbContext(typeof(WodItEasyDbContext))]
    partial class WodItEasyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WodItEasy.Infrastructure.Persistence.Models.Athlete", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int?>("MembershipId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("MembershipId");

                    b.ToTable("Athletes");
                });

            modelBuilder.Entity("WodItEasy.Infrastructure.Persistence.Models.AthleteWorkout", b =>
                {
                    b.Property<int>("AthleteId")
                        .HasColumnType("int");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("int");

                    b.HasKey("AthleteId", "WorkoutId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("AthletesWorkouts");
                });

            modelBuilder.Entity("WodItEasy.Infrastructure.Persistence.Models.Membership", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("StartsAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int?>("WorkoutsCount")
                        .HasColumnType("int");

                    b.Property<int?>("WorkoutsLeft")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Memberships");
                });

            modelBuilder.Entity("WodItEasy.Infrastructure.Persistence.Models.Workout", b =>
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

                    b.Property<int>("WorkoutType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("WodItEasy.Infrastructure.Persistence.Models.Athlete", b =>
                {
                    b.HasOne("WodItEasy.Infrastructure.Persistence.Models.Membership", "Membership")
                        .WithMany()
                        .HasForeignKey("MembershipId");

                    b.OwnsOne("WodItEasy.Infrastructure.Persistence.Models.PhoneNumber", "PhoneNumber", b1 =>
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

                    b.Navigation("Membership");

                    b.Navigation("PhoneNumber")
                        .IsRequired();
                });

            modelBuilder.Entity("WodItEasy.Infrastructure.Persistence.Models.AthleteWorkout", b =>
                {
                    b.HasOne("WodItEasy.Infrastructure.Persistence.Models.Athlete", "Athlete")
                        .WithMany("AthletesWorkouts")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WodItEasy.Infrastructure.Persistence.Models.Workout", "Workout")
                        .WithMany("AthletesWorkouts")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Athlete");

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("WodItEasy.Infrastructure.Persistence.Models.Athlete", b =>
                {
                    b.Navigation("AthletesWorkouts");
                });

            modelBuilder.Entity("WodItEasy.Infrastructure.Persistence.Models.Workout", b =>
                {
                    b.Navigation("AthletesWorkouts");
                });
#pragma warning restore 612, 618
        }
    }
}