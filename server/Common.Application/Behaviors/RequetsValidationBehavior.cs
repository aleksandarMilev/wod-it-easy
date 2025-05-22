namespace WodItEasy.Common.Application.Behaviors
{
    using Exceptions;
    using FluentValidation;
    using MediatR;

    public class RequestValidationBehavior<TRequest, TResponse>(
        IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators = validators;

        public Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var errors = this.validators
                .SelectMany(v => v.Validate(request).Errors)
                .Where(f => f is not null)
                .ToList();

            if (errors.Count > 0)
                throw new ModelValidationException(errors);

            return next(cancellationToken);
        }
    }
}
