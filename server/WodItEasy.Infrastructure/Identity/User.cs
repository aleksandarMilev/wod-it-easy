namespace WodItEasy.Infrastructure.Identity
{
    using Microsoft.AspNetCore.Identity;
    using Domain.Models.Athletes;
    using Domain.Exceptions;

    public class User : IdentityUser
    {
        internal User(string email) 
            : base(email) => this.Email = email;

        public Athlete? Athlete { get; private set; }

        public void BecomeAthlete(Athlete athlete)
        {
            if (this.Athlete != null)
            {
                throw new InvalidAthleteException($"User '{this.UserName}' is already a dealer.");
            }

            this.Athlete = athlete;
        }
    }
}
