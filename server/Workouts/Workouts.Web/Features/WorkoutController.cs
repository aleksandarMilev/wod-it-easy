namespace WodItEasy.Workouts.Web.Features
{
    using Application.Features.Workouts.Queries.Details;
    using Application.Features.Workouts.Queries.Search;
    using Common.Application.Models;
    using Common.Web.Controllers;
    using Microsoft.AspNetCore.Mvc;

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
