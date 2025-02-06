namespace WodItEasy.Web.Features
{
    using System.Threading.Tasks;
    using Application.Common;
    using Application.Features.Participations.Commands.Cancel;
    using Application.Features.Participations.Commands.Common;
    using Application.Features.Participations.Commands.Create;
    using Application.Features.Participations.Commands.Delete;
    using Application.Features.Participations.Commands.ReJoin;
    using Application.Features.Participations.Queries.GetId;
    using Application.Features.Participations.Queries.Mine;
    using Microsoft.AspNetCore.Mvc;
    using Web.Common;

    public class ParticipationController : AuthenticatedApiController
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedOutputModel<MyParticipationsOutputModel>>> Mine(
            [FromQuery] MyParticipationsQuery query)
            => await this.Send(query);

        [HttpGet("{athleteId}/{workoutId}")]
        public async Task<ActionResult<GetParticipationIdOutputModel>> GetId(
            [FromRoute] GetParticipationIdQuery query)
            => await this.Send(query);

        [HttpPost]
        public async Task<ActionResult<ParticipationOutputModel>> Create(
            [FromBody] CreateParticipationCommand command)
            => await this.Send(command);

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(
            [FromRoute] DeleteParticipationCommand command)
            => await this.Send(command);

        [HttpPatch("cancel/{id}")]
        public async Task<ActionResult> Cancel(
            [FromRoute] CancelParticipationCommand command)
            => await this.Send(command);

        [HttpPatch("re-join/{id}")]
        public async Task<ActionResult<ParticipationOutputModel>> ReJoin(
            [FromRoute] ReJoinParticipationCommand command)
            => await this.Send(command);
    }
}
