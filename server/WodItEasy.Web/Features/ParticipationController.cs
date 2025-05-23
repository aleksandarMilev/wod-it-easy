namespace WodItEasy.Web.Features
{
    using Application.Features.Participations.Commands.Cancel;
    using Application.Features.Participations.Commands.Common;
    using Application.Features.Participations.Commands.Create;
    using Application.Features.Participations.Commands.Delete;
    using Application.Features.Participations.Commands.ReJoin;
    using Application.Features.Participations.Queries.GetId;
    using Application.Features.Participations.Queries.Mine;
    using Microsoft.AspNetCore.Mvc;
    using WodItEasy.Common.Application.Models;
    using WodItEasy.Common.Web.Controllers;

    public class ParticipationController : AuthenticatedApiController
    {
        private const string AthleteId = "{athleteId}";
        private const string WorkoutId = "{workoutId}";

        [HttpGet(nameof(this.Mine))]
        public async Task<ActionResult<PaginatedOutputModel<MyParticipationsOutputModel>>> Mine(
            [FromQuery] MyParticipationsQuery query)
            => await this.Send(query);

        [HttpGet(AthleteId + PathSeparator + WorkoutId)]
        public async Task<ActionResult<GetParticipationIdOutputModel>> GetId(
            [FromRoute] GetParticipationIdQuery query)
            => await this.Send(query);

        [HttpPost]
        public async Task<ActionResult<ParticipationOutputModel>> Create(
            [FromBody] CreateParticipationCommand command)
            => await this.Send(command);

        [HttpDelete(Id)]
        public async Task<ActionResult> Delete(
            [FromRoute] DeleteParticipationCommand command)
            => await this.Send(command);

        [HttpPatch(nameof(this.Cancel) + PathSeparator + Id)]
        public async Task<ActionResult> Cancel(
            [FromRoute] CancelParticipationCommand command)
            => await this.Send(command);

        [HttpPatch(nameof(this.ReJoin) + PathSeparator + Id)]
        public async Task<ActionResult<ParticipationOutputModel>> ReJoin(
            [FromRoute] ReJoinParticipationCommand command)
            => await this.Send(command);
    }
}
