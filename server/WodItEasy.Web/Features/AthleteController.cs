namespace WodItEasy.Web.Features
{
    using System.Threading.Tasks;
    using Application.Features.Athlete.Commands.Create;
    using Application.Features.Athlete.Queries.GetId;
    using Common;
    using Microsoft.AspNetCore.Mvc;

    public class AthleteController : AuthenticatedApiController
    {
        [HttpGet("id")]
        public async Task<ActionResult<int?>> GetId(
            [FromRoute] GetAthleteIdQuery query)
                => await this.Send(query);

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateAthleteCommand command)
            => await this.Send(command);
    }
}
