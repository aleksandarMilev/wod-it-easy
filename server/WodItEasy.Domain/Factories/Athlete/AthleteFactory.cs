namespace WodItEasy.Domain.Factories.Athlete
{
    using Models.Athletes;

    public class AthleteFactory : IAthleteFactory
    {
        private string name = default!;
        private string email = default!;
        private PhoneNumber phone = default!;
        private Membership? membership;

        public IAthleteFactory WithName(string name)
        {
            this.name = name;
            return this;
        }

        public IAthleteFactory WithEmail(string email)
        {
            this.email = email;
            return this;
        }

        public IAthleteFactory WithPhone(PhoneNumber phone)
        {
            this.phone = phone;
            return this;
        }

        public IAthleteFactory WithMembership(Membership? membership)
        {
            this.membership = membership;
            return this;
        }

        public Athlete Build() => new(this.name, this.email, this.phone, this.membership);
    }
}
