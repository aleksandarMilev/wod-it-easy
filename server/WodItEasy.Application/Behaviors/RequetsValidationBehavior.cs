﻿namespace WodItEasy.Application.Behaviors
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Exceptions;
    using FluentValidation;
    using MediatR;

    public class RequestValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
            where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
            => this.validators = validators;

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
            {
                throw new ModelValidationException(errors);
            }

            return next();
        }
    }
}
