namespace WodItEasy.Infrastructure.Persistence.Models
{
    using System;

    public class Membership
    {
        public int Id { get; set; }

        public int Type { get; set; }

        public int? WorkoutsCount { get; set; }

        public int? WorkoutsLeft { get; set; }

        public DateTime StartsAt { get; set; }
    }
}
