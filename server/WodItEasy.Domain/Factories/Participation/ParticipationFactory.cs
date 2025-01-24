namespace WodItEasy.Domain.Factories.Participation
{
    using Models.Participation;

    public class ParticipationFactory : IParticipationFactory
    {
        private int athleteId;
        private int workoutId;

        public IParticipationFactory WithAthleteId(int athleteId)
        {
            this.athleteId = athleteId;

            return this;
        }

        public IParticipationFactory WithWorkoutId(int workoutId)
        {
            this.workoutId = workoutId;

            return this;
        }

        public Participation Build()
            => new(this.athleteId, this.workoutId);
    }
}
