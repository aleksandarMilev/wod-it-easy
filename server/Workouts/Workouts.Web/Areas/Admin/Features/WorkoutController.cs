namespace WodItEasy.Workouts.Web.Areas.Admin.Features
{
    using Application.Features.Workouts.Commands.Create;
    using Application.Features.Workouts.Commands.Delete;
    using Application.Features.Workouts.Commands.Update;
    using Common.Application.Commands;
    using Common.Web.Controllers;
    using Microsoft.AspNetCore.Mvc;

    public class WorkoutController : AdminApiController
    {
        [HttpPost]
        public async Task<ActionResult<CreateWorkoutOutputModel>> Create(
            [FromBody] CreateWorkoutCommand command)
            => await this.Send(command);

        [HttpPut(Id)]
        public async Task<ActionResult> Update(
            [FromRoute] int id,
            [FromBody] UpdateWorkoutCommand command)
            => await this.Send(command.SetId(id));

        [HttpDelete(Id)]
        public async Task<ActionResult> Delete(
            [FromRoute] DeleteWorkoutCommand command)
            => await this.Send(command);
    }
}
