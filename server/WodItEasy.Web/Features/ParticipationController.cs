namespace WodItEasy.Web.Features
{
    using System.Threading.Tasks;
    using Application.Common;
    using Application.Features.Participations.Commands.Cancel;
    using Application.Features.Participations.Commands.Create;
    using Application.Features.Participations.Commands.Delete;
    using Application.Features.Participations.Commands.ReJoin;
    using Application.Features.Participations.Queries.GetId;
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
        public async Task<ActionResult<int>> GetId(int athleteId, int workoutId)
            => await this.Send(new GetParticipationIdQuery(athleteId, workoutId)); 

        [HttpPost]
        public async Task<ActionResult<int>> Join(CreateParticipationCommand command)
            => await this.Send(command);

        [HttpDelete("{athleteId}/{workoutId}")]
        public async Task<ActionResult<bool>> Leave(int athleteId, int workoutId)
            => await this.Send(new DeleteParticipationCommand(athleteId, workoutId));

        [HttpPatch("cancel/{id}")]
        public async Task<ActionResult> Cancel(int id)
            => await this.Send(new CancelParticipationCommand(id));

        [HttpPatch("re-join/{id}")]
        public async Task<ActionResult<int>> ReJoin(int id)
            => await this.Send(new ReJoinParticipationCommand(id));
    }
}
