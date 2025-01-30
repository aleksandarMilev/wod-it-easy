namespace WodItEasy.Web.Features
{
    using System.Threading.Tasks;
    using Application.Common;
    using Application.Features.Participations.Commands.Create;
    using Application.Features.Participations.Commands.Delete;
    using Application.Features.Participations.Queries.IsParticipant;
    using Application.Features.Participations.Queries.Mine;
    using Common;
    using Microsoft.AspNetCore.Mvc;

    public class ParticipationController : AuthenticatedApiController
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedOutputModel<MyParticipationsOutputModel>>> Mine(
            [FromQuery] MyParticipationsQuery query)
                => await this.Send(query);

        [HttpGet("{athleteId}/{workoutId}")]
        public async Task<ActionResult<bool>> IsParticipant(int athleteId, int workoutId)
            => await this.Send(new IsParticipantQuery(athleteId, workoutId)); 

        [HttpPost]
        public async Task<ActionResult> Join(CreateParticipationCommand command)
            => await this.Send(command);

        [HttpDelete("{athleteId}/{workoutId}")]
        public async Task<ActionResult<bool>> Leave(int athleteId, int workoutId)
            => await this.Send(new DeleteParticipationCommand(athleteId, workoutId));
    }
}
