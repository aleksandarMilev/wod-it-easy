namespace WodItEasy.Domain.Factories.Participation
{
    using System;
    using Models.Athletes;
    using Models.Participation;
    using Models.Workouts;

    public class ParticipationFactory : IParticipationFactory
    {
        private Athlete athlete = default!;
        private Workout workout = default!;
        private DateTime joinedAt;
        private ParticipationStatus status = default!;

        public IParticipationFactory ForAthlete(Athlete athlete)
        {
            this.athlete = athlete;

            return this;
        }

        public IParticipationFactory ForWorkout(Workout workout)
        {
            this.workout = workout;

            return this;
        }

        public IParticipationFactory JoinedAt(DateTime joinedAt)
        {
            this.joinedAt = joinedAt;

            return this;
        }

        public IParticipationFactory WithStatus(ParticipationStatus status)
        {
            this.status = status;

            return this;
        }

        public Participation Build()
            => new(
                this.athlete, 
                this.workout,
                this.joinedAt,
                this.status);
    }
}
