namespace WodItEasy.Domain.Models.Athletes
{
    using Common;
    using Exceptions;

    using static ModelConstants.Athlete;

    public class Athlete : Entity<int>, IAggregateRoot
    {
        public Athlete(
            string name,
            string email,
            string phoneNumber,
            Membership? membership)
        {
            this.Validate(name, email);

            this.Name = name;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.Membership = membership;
        }

        public string Name { get; }

        public string Email { get; }

        public PhoneNumber PhoneNumber { get; }

        public Membership? Membership { get; }

        private void Validate(string name, string email)
        {
            Guard.ForStringLength<InvalidAthleteException>(
                name,
                MinNameLength,
                MaxNameLength,
                nameof(this.Name));

            Guard.ForValidEmail<InvalidAthleteException>(
                email,
                nameof(this.Email));
        }
    }
}
