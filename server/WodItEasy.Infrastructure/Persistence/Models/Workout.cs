namespace WodItEasy.Infrastructure.Persistence.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Workout
    {
        private const int NameMaxLength = 100;
        private const int DescriptionMaxLength = 500;

        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public int MaxParticipantsCount { get; set; }

        public int CurrentParticipantsCount { get; set; }

        public DateTime StartsAtDate { get; set; }

        public TimeSpan StartsAtTime { get; set; }

        public int WorkoutType { get; set; }

        public IEnumerable<AthleteWorkout> AthletesWorkouts { get; set; } = new HashSet<AthleteWorkout>();
    }
}
