namespace WodItEasy.Domain.Factories.Athlete
{
    using Models.Athletes;

    public interface IAthleteFactory : IFactory<Athlete>
    {
        IAthleteFactory WithName(string name);
        IAthleteFactory WithEmail(string email);
        IAthleteFactory WithPhone(PhoneNumber phone);
        IAthleteFactory WithMembership(Membership membership);
    }
}
