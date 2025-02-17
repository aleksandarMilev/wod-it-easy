namespace WodItEasy.Web.Areas.Admin.Features
{
    using System.Threading.Tasks;
    using Application.Features.Workouts.Commands.Create;
    using Application.Features.Workouts.Commands.Delete;
    using Application.Features.Workouts.Commands.Update;
    using Microsoft.AspNetCore.Mvc;
    using WodItEasy.Application.Common;

    public class WorkoutController : AdminApiController
    {
        [HttpPost]
        public async Task<ActionResult<CreateWorkoutOutputModel>> Create(
            [FromBody] CreateWorkoutCommand command)
        {
            try
            {
                return await this.Send(command);
            }
            catch (System.Exception ex)
            {
                await System.Console.Out.WriteLineAsync(ex.Message);
                await System.Console.Out.WriteLineAsync(ex?.InnerException.Message);
                throw;
            }
        }

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
