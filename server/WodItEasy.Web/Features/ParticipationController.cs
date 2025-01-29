namespace WodItEasy.Web.Features
{
    using System.Threading.Tasks;
    using Application.Features.Participations.Commands.Create;
    using Application.Features.Participations.Commands.Delete;
    using Application.Features.Participations.Queries.IsParticipant;
    using Common;
    using Microsoft.AspNetCore.Mvc;

    public class ParticipationController : AuthenticatedApiController
    {
        [HttpGet("{athleteId}/{workoutId}")]
        public async Task<ActionResult<bool>> IsParticipant(int athleteId, int workoutId)
            => await this.Send(new IsParticipantQuery(athleteId, workoutId)); 

        [HttpPost]
        public async Task<ActionResult> Join(CreateParticipationCommand command)
            => await this.Send(command);

        [HttpDelete]
        public async Task<ActionResult> Leave(DeleteParticipationCommand command)
            => await this.Send(command);
    }
}
