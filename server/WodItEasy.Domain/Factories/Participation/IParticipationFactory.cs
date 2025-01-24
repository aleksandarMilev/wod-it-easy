namespace WodItEasy.Domain.Factories.Participation
{
    using Models.Participation;

    public interface IParticipationFactory : IFactory<Participation>
    {
        IParticipationFactory WithAthleteId(int athleteId);

        IParticipationFactory WithWorkoutId(int workoutId);
    }
}
