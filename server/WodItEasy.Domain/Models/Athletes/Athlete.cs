namespace WodItEasy.Domain.Models.Athletes
{
    using Common.Domain.Models;
    using Exceptions;
    using Participation;

    using static ModelConstants.AthleteConstants;

    public class Athlete : DeletableEntity<int>, IAggregateRoot
    {
        private readonly ICollection<Participation> participations = new HashSet<Participation>();

        internal Athlete(
            string name,
            string userId)
        {
            this.Validate(name);

            this.Name = name;
            this.UserId = userId;
        }

        public IReadOnlyCollection<Participation> Participations 
            => this.participations.ToList().AsReadOnly();

        public string Name { get; private set; }

        public string UserId { get; }

        public Athlete UpdateName(string name)
        {
            this.ValidateName(name);
            this.Name = name;

            return this;
        }

        private void Validate(string name) 
            => this.ValidateName(name);

        private void ValidateName(string name)
            => Guard.ForStringLength<InvalidAthleteException>(
                name,
                MinNameLength,
                MaxNameLength,
                nameof(this.Name));
    }
}
