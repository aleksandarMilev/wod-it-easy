﻿namespace WodItEasy.Web
{
    using System;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using WodItEasy.Application.Common;
    using WodItEasy.Web.Common;

    [ApiController]
    [Route("[controller]")]
    public abstract class ApiController : ControllerBase
    {
        private IMediator? mediator;

        protected IMediator Mediator
            => this.mediator ??= this
                .HttpContext
                .RequestServices
                .GetService<IMediator>()
                ?? throw new InvalidOperationException($"{nameof(IMediator)} service can not be resolved!");

        protected Task<ActionResult<TResult>> Send<TResult>(IRequest<TResult> request) => this.Mediator.Send(request).ToActionResult();

        protected Task<ActionResult> Send(IRequest<Result> request) => this.Mediator.Send(request).ToActionResult();

        protected Task<ActionResult<TResult>> Send<TResult>(IRequest<Result<TResult>> request) => this.Mediator.Send(request).ToActionResult();
    }
}