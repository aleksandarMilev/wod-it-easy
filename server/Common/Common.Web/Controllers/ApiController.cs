﻿namespace WodItEasy.Common.Web.Controllers
{
    using Application;
    using Extensions;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    [ApiController]
    [Route("[controller]")]
    public abstract class ApiController : ControllerBase
    {
        public const string PathSeparator = "/";
        public const string Id = "{id}";

        private IMediator? mediator;

        protected IMediator Mediator
            => this.mediator ??= this
                .HttpContext
                .RequestServices
                .GetService<IMediator>()
                ?? throw new InvalidOperationException(
                    $"{nameof(IMediator)} service can not be resolved!");

        protected Task<ActionResult<TResult>> Send<TResult>(IRequest<TResult> request) 
            => this.Mediator.Send(request).ToActionResult();

        protected Task<ActionResult> Send(IRequest<Result> request) 
            => this.Mediator.Send(request).ToActionResult();

        protected Task<ActionResult<TResult>> Send<TResult>(IRequest<Result<TResult>> request) 
            => this.Mediator.Send(request).ToActionResult();
    }
}
