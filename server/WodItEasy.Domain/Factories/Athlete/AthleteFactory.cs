namespace WodItEasy.Domain.Factories.Athlete
{
    using Models.Athletes;

    public class AthleteFactory : IAthleteFactory
    {
        private string name = default!;
        private string userId = default!;

        public IAthleteFactory WithName(string name)
        {
            this.name = name;

            return this;
        }

        public IAthleteFactory WithUserId(string userId)
        {
            this.userId = userId;

            return this;
        }

        public Athlete Build()
            => new(this.name, this.userId);
    }
}
