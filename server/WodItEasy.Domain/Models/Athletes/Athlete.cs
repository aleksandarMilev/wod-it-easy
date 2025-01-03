namespace WodItEasy.Domain.Models.Athletes
{
    using Common;
    using Exceptions;

    using static ModelConstants.AthleteConstants;

    public class Athlete : Entity<int>, IAggregateRoot
    {
        internal Athlete(string name, Membership? membership = null)
        {
            this.Validate(name);

            this.Name = name;
            this.Membership = membership;
        }

        private Athlete(string name)
        {
            this.Name = name;

            this.Membership = default;
        }

        public string Name { get; private set; }

        public Membership? Membership { get; private set; }

        public Athlete UpdateName(string name)
        {
            this.ValidateName(name);
            this.Name = name;

            return this;
        }

        public bool HasMembership() => this.Membership! != null!;

        public void CreateMembership(Membership membership) => this.Membership = membership;

        public void UpdateMembership(Membership membership) => this.Membership = membership;

        public void DeleteMembership() => this.Membership = null;

        private void Validate(string name) => this.ValidateName(name);

        private void ValidateName(string name)
        {
            Guard.ForStringLength<InvalidAthleteException>(
                name,
                MinNameLength,
                MaxNameLength,
                nameof(this.Name));
        }
    }
}
