namespace WodItEasy.Web.Features
{
    using System.Threading.Tasks;
    using Application.Common;
    using Application.Features;
    using Application.Features.Workouts.Commands.Create;
    using Application.Features.Workouts.Commands.Delete;
    using Application.Features.Workouts.Commands.Edit;
    using Application.Features.Workouts.Commands.Join;
    using Application.Features.Workouts.Commands.Leave;
    using Application.Features.Workouts.Queries.Details;
    using Application.Features.Workouts.Queries.Search;
    using Areas.Admin;
    using Microsoft.AspNetCore.Mvc;

    public class WorkoutController : AdminApiController
    {
        [HttpGet("search")]
        public async Task<ActionResult<PaginatedOutputModel<SearchWorkoutOutputModel>>> Search(
            [FromQuery] SearchWorkoutQuery query)
                => await this.Send(query);

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutDetailsOutputModel?>> Details(
            [FromRoute] int id)
                => await this.Send(new WorkoutDetailsQuery(id));

        [HttpPost]
        public async Task<ActionResult<int>> Create(
            [FromBody] CreateWorkoutCommand command)
                => await this.Send(command);

        [HttpPost("join")]
        public async Task<ActionResult> Join(JoinWorkoutCommand command)
            => await this.Send(command);

        [HttpPost("leave")]
        public async Task<ActionResult> Leave(LeaveWorkoutCommand command)
            => await this.Send(command);

        [HttpPut("{id}")]
        public async Task<ActionResult<Result>> Edit(
            [FromRoute]int id, 
            [FromBody]EditWorkoutCommand command)
                => await this.Send(command.SetId(id));

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> Delete(
            [FromRoute] int id)
                => await this.Send(new DeleteWorkoutCommand(id));
    }
}
