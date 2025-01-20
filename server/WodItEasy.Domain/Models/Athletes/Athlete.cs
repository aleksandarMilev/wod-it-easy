namespace WodItEasy.Domain.Models.Athletes
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Exceptions;
    using Participation;

    using static ModelConstants.AthleteConstants;

    public class Athlete : Entity<int>, IAggregateRoot
    {
        private readonly ICollection<Participation> participations = new HashSet<Participation>();

        internal Athlete(string name)
        {
            this.Validate(name);

            this.Name = name;
        }

        public string Name { get; private set; }

        public IReadOnlyCollection<Participation> Participations 
            => this.participations.ToList().AsReadOnly();

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
