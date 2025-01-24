namespace WodItEasy.Domain.Factories.Athlete
{
    using Models.Athletes;

    public class AthleteFactory : IAthleteFactory
    {
        private string name = default!;

        public IAthleteFactory WithName(string name)
        {
            this.name = name;

            return this;
        }

        public Athlete Build()
            => new(this.name);
    }
}
