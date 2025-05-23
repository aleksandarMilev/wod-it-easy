namespace WodItEasy.Common.Application.Exceptions
{ 
    using FluentValidation.Results;

    public class ModelValidationException : Exception
    {
        public ModelValidationException()
            : base("One or more validation errors have occurred.")
            => this.Errors = new Dictionary<string, string[]>();

        public ModelValidationException(IEnumerable<ValidationFailure> errors)
            : this()
            => errors
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToList()
                .ForEach(failureGroup =>
                {
                    var propertyName = failureGroup.Key;
                    var propertyFailures = failureGroup.ToArray();

                    this.Errors.Add(propertyName, propertyFailures);
                });

        public IDictionary<string, string[]> Errors { get; }
    }
}
