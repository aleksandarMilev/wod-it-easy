namespace WodItEasy.Web.Features
{
    using System.Threading.Tasks;
    using Application.Common;
    using Application.Features;
    using Application.Features.Workouts.Commands.Create;
    using Application.Features.Workouts.Commands.Edit;
    using Application.Features.Workouts.Commands.Delete;
    using Application.Features.Workouts.Queries.Details;
    using Application.Features.Workouts.Queries.Search;
    using Microsoft.AspNetCore.Mvc;

    public class WorkoutController : ApiController
    {
        [HttpGet("search")]
        public async Task<ActionResult<PaginatedOutputModel<SearchWorkoutOutputModel>>> Search(
            [FromQuery] SearchWorkoutQuery query)
            => await this.SendAsync(query);

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutDetailsOutputModel?>> Details(
            [FromRoute] int id)
            => await this.SendAsync(new WorkoutDetailsQuery() { Id = id });

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateWorkoutCommand command)
            => await this.SendAsync(command);

        [HttpPut("{id}")]
        public async Task<ActionResult<Result>> Edit(
            [FromRoute]int id, [FromBody]EditWorkoutCommand command)
            => await this.SendAsync(command.SetId(id));

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(
            [FromRoute] int id)
            => await this.SendAsync(new DeleteWorkoutCommand() { Id = id });
    }
}
