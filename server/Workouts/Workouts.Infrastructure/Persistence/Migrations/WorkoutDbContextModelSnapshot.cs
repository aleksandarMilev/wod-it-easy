#nullable disable
namespace WodItEasy.Workouts.Infrastructure.Persistence.Migrations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;

    [DbContext(typeof(WorkoutDbContext))]
    partial class WorkoutDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WodItEasy.Workouts.Domain.Models.Athletes.Athlete", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Athletes");
                });

            modelBuilder.Entity("WodItEasy.Workouts.Domain.Models.Participation.Participation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AthleteId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("JoinedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutId");

                    b.HasIndex("AthleteId", "JoinedAt");

                    b.HasIndex("AthleteId", "WorkoutId");

                    b.ToTable("Participations");
                });

            modelBuilder.Entity("WodItEasy.Workouts.Domain.Models.Workouts.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrentParticipantsCount")
                        .HasColumnType("int");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("MaxParticipantsCount")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("StartsAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("StartsAt");

                    b.HasIndex("StartsAt", "Id");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("WodItEasy.Workouts.Domain.Models.Participation.Participation", b =>
                {
                    b.HasOne("WodItEasy.Workouts.Domain.Models.Athletes.Athlete", "Athlete")
                        .WithMany("Participations")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WodItEasy.Workouts.Domain.Models.Workouts.Workout", "Workout")
                        .WithMany("Participations")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.OwnsOne("WodItEasy.Workouts.Domain.Models.Participation.ParticipationStatus", "Status", b1 =>
                        {
                            b1.Property<int>("ParticipationId")
                                .HasColumnType("int");

                            b1.Property<int>("Value")
                                .HasColumnType("int");

                            b1.HasKey("ParticipationId");

                            b1.ToTable("Participations");

                            b1.WithOwner()
                                .HasForeignKey("ParticipationId");
                        });

                    b.Navigation("Athlete");

                    b.Navigation("Status")
                        .IsRequired();

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("WodItEasy.Workouts.Domain.Models.Workouts.Workout", b =>
                {
                    b.OwnsOne("WodItEasy.Workouts.Domain.Models.Workouts.WorkoutType", "Type", b1 =>
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

                    b.Navigation("Type");
                });

            modelBuilder.Entity("WodItEasy.Workouts.Domain.Models.Athletes.Athlete", b =>
                {
                    b.Navigation("Participations");
                });

            modelBuilder.Entity("WodItEasy.Workouts.Domain.Models.Workouts.Workout", b =>
                {
                    b.Navigation("Participations");
                });
#pragma warning restore 612, 618
        }
    }
}
