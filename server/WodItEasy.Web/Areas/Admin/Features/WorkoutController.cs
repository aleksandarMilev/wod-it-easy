namespace WodItEasy.Web.Areas.Admin.Features
{
    using System.Threading.Tasks;
    using Application.Features;
    using Application.Features.Workouts.Commands.Create;
    using Application.Features.Workouts.Commands.Delete;
    using Application.Features.Workouts.Commands.Edit;
    using Microsoft.AspNetCore.Mvc;

    public class WorkoutController : AdminApiController
    {
        [HttpPost]
        public async Task<ActionResult<int>> Create(
            [FromBody] CreateWorkoutCommand command)
                => await this.Send(command);

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(
            [FromRoute] int id,
            [FromBody] EditWorkoutCommand command)
                => await this.Send(command.SetId(id));

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(
            [FromRoute] int id)
                => await this.Send(new DeleteWorkoutCommand(id));
    }
}
