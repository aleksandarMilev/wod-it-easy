namespace WodItEasy.Workouts.Web.Features
{
    using Application.Features.Athlete.Commands.Create;
    using Application.Features.Athlete.Commands.Delete;
    using Application.Features.Athlete.Commands.Update;
    using Application.Features.Athlete.Queries.Details;
    using Application.Features.Athlete.Queries.GetId;
    using Common.Web.Controllers;
    using Microsoft.AspNetCore.Mvc;

    public class AthleteController : AuthenticatedApiController
    {
        private const string GetIdRoute = "id";

        [HttpGet(GetIdRoute)]
        public async Task<ActionResult<GetAthleteIdOutputModel?>> GetId(
            [FromRoute] GetAthleteIdQuery query)
            => await this.Send(query);

        [HttpGet]
        public async Task<ActionResult<GetAthleteDetailsOutputModel?>> Details(
            [FromRoute] GetAthleteDetailsQuery query)
            => await this.Send(query);

        [HttpPost]
        public async Task<ActionResult<CreateAthleteOutputModel>> Create(
            [FromBody] CreateAthleteCommand command)
            => await this.Send(command);

        [HttpPut]
        public async Task<ActionResult> Update(
            [FromBody] UpdateAthleteCommand command)
            => await this.Send(command);

        [HttpDelete]
        public async Task<ActionResult> Delete(
            [FromRoute] DeleteAthleteCommand command)
            => await this.Send(command);
    }
}
