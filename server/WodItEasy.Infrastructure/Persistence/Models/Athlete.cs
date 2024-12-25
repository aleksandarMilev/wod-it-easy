namespace WodItEasy.Infrastructure.Persistence.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Athlete 
    {
        private const int NameMaxLength = 100;
        private const int EmailMaxLength = 256;

        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; } = null!;

        [Required]
        public PhoneNumber PhoneNumber { get; set; } = null!;

        public Membership? Membership { get; set; }

        public IEnumerable<AthleteWorkout> AthletesWorkouts { get; set; } = new HashSet<AthleteWorkout>();
    }
}
