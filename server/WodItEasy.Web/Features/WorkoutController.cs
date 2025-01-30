namespace WodItEasy.Web.Features
{
    using System.Threading.Tasks;
    using Application.Common;
    using Application.Features.Workouts.Queries.Details;
    using Application.Features.Workouts.Queries.Search;
    using Common;
    using Microsoft.AspNetCore.Mvc;

    public class WorkoutController : ApiController
    {
        [HttpGet("search")]
        public async Task<ActionResult<PaginatedOutputModel<SearchWorkoutOutputModel>>> Search(
            [FromQuery] SearchWorkoutQuery query)
                => await this.Send(query);

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutDetailsOutputModel?>> Details(
            [FromRoute] int id)
                => await this.Send(new WorkoutDetailsQuery(id));
    }
}
