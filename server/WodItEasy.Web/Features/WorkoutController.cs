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
        {
            try
            {
                return await this.Send(query);
            }
            catch (System.Exception ex)
            {
                await System.Console.Out.WriteLineAsync(ex.Message);
                await System.Console.Out.WriteLineAsync(ex.InnerException?.Message);

                throw new System.Exception();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutDetailsOutputModel?>> Details(
            [FromRoute] WorkoutDetailsQuery query)
            => await this.Send(query);
    }
}
