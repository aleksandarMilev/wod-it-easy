namespace WodItEasy.Web.Features
{
    using Application.Features.Workouts.Queries.Details;
    using Application.Features.Workouts.Queries.Search;
    using Microsoft.AspNetCore.Mvc;
    using WodItEasy.Common.Application.Models;
    using WodItEasy.Common.Web.Controllers;

    public class WorkoutController : ApiController
    {
        [HttpGet(nameof(this.Search))]
        public async Task<ActionResult<PaginatedOutputModel<SearchWorkoutOutputModel>>> Search(
            [FromQuery] SearchWorkoutQuery query)
            => await this.Send(query);

        [HttpGet(Id)]
        public async Task<ActionResult<WorkoutDetailsOutputModel?>> Details(
            [FromRoute] WorkoutDetailsQuery query)
            => await this.Send(query);
    }
}
