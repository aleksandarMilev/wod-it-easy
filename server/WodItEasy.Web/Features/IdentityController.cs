namespace WodItEasy.Web.Features
{
    using System.Threading.Tasks;
    using Application.Features.Identity.Commands.Login;
    using Application.Features.Identity.Commands.Register;
    using Common;
    using Microsoft.AspNetCore.Mvc;

    public class IdentityController : ApiController
    {
        [HttpPost(nameof(this.Register))]
        public async Task<ActionResult<RegisterOutputModel>> Register(
            [FromBody] RegisterCommand command) 
            => await this.Send(command);

        [HttpPost(nameof(this.Login))]
        public async Task<ActionResult<LoginOutputModel>> Login(
            [FromBody] LoginCommand command) 
            => await this.Send(command);
    }
}
