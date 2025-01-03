namespace WodItEasy.Domain.Factories.Athlete
{
    using Models.Athletes;

    public interface IAthleteFactory : IFactory<Athlete>
    {
        IAthleteFactory WithName(string name);

        IAthleteFactory WithMembership(Membership membership);
    }
}
