namespace WodItEasy.Infrastructure.Persistence.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.EntityFrameworkCore;

    [PrimaryKey(nameof(AthleteId), nameof(WorkoutId))]
    public class AthleteWorkout
    {
        [Required]
        [ForeignKey(nameof(Athlete))]
        public int AthleteId { get; set; }

        public Athlete Athlete { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Workout))]
        public int WorkoutId { get; set; }

        public Workout Workout { get; set; } = null!;
    }
}
