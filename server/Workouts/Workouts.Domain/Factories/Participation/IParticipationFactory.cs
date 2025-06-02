namespace WodItEasy.Workouts.Domain.Factories.Participation
{
    using Common.Domain;
    using Models.Athletes;
    using Models.Participation;
    using Models.Workouts;

    public interface IParticipationFactory : IFactory<Participation>
    {
        IParticipationFactory ForAthlete(Athlete athlete);

        IParticipationFactory ForWorkout(Workout workout);

        IParticipationFactory JoinedAt(DateTime joinedAt);

        IParticipationFactory WithStatus(ParticipationStatus status);
    }
}
