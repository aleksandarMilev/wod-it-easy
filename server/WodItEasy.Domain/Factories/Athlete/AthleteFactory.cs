namespace WodItEasy.Domain.Factories.Athlete
{
    using Models.Athletes;

    public class AthleteFactory : IAthleteFactory
    {
        private string name = default!;
        private Membership? membership;

        public IAthleteFactory WithName(string name)
        {
            this.name = name;
            return this;
        }

        public IAthleteFactory WithMembership(Membership? membership)
        {
            this.membership = membership;
            return this;
        }

        public Athlete Build() => new(this.name, this.membership);
    }
}
