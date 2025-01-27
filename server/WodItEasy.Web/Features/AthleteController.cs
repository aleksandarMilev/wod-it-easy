namespace WodItEasy.Web.Features
{
    using System.Threading.Tasks;
    using Application.Features.Athlete.Commands.Create;
    using Common;
    using Microsoft.AspNetCore.Mvc;

    public class AthleteController : AuthenticatedApiController
    {
        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateAthleteCommand command)
            => await this.Send(command);
    }
}
