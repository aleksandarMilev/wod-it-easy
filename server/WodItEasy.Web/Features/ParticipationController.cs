namespace WodItEasy.Web.Features
{
    using System.Threading.Tasks;
    using Application.Features.Participations.Commands.Create;
    using Application.Features.Participations.Commands.Delete;
    using Common;
    using Microsoft.AspNetCore.Mvc;

    public class ParticipationController : AuthenticatedApiController
    {

        [HttpPost]
        public async Task<ActionResult> Join(CreateParticipationCommand command)
            => await this.Send(command);

        [HttpDelete]
        public async Task<ActionResult> Leave(DeleteParticipationCommand command)
            => await this.Send(command);
    }
}
