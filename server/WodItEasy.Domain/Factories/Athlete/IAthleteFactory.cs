namespace WodItEasy.Domain.Factories.Athlete
{
    using Common.Domain;
    using Models.Athletes;

    public interface IAthleteFactory : IFactory<Athlete>
    {
        IAthleteFactory WithName(string name);

        IAthleteFactory WithUserId(string userId);
    }
}
