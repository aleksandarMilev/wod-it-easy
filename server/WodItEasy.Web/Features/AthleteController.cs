namespace WodItEasy.Web.Features
{
    using System.Threading.Tasks;
    using Application.Features.Athlete.Commands.Create;
    using Application.Features.Athlete.Commands.Delete;
    using Application.Features.Athlete.Commands.Update;
    using Application.Features.Athlete.Queries.Get;
    using Application.Features.Athlete.Queries.GetId;
    using Common;
    using Microsoft.AspNetCore.Mvc;

    public class AthleteController : AuthenticatedApiController
    {
        [HttpGet]
        public async Task<ActionResult<GetAthleteOutputModel?>> Get(
            [FromRoute] GetAthleteQuery query)
                => await this.Send(query);

        [HttpGet("id")]
        public async Task<ActionResult<int?>> GetId(
            [FromRoute] GetAthleteIdQuery query)
                => await this.Send(query);

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateAthleteCommand command)
            => await this.Send(command);

        [HttpPut]
        public async Task<ActionResult> Update(UpdateAthleteCommand command)
            => await this.Send(command);

        [HttpPut]
        public async Task<ActionResult> Delete(DeleteAthleteCommand command)
            => await this.Send(command);
    }
}
