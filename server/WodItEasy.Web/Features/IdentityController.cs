namespace WodItEasy.Web.Features
{
    using System.Threading.Tasks;
    using Application.Features.Identity.Commands.Login;
    using Application.Features.Identity.Commands.Register;
    using Common;
    using Microsoft.AspNetCore.Mvc;

    public class IdentityController : ApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult<RegisterOutputModel>> Register(
            [FromBody] RegisterCommand command) 
            => await this.Send(command);

        [HttpPost("login")]
        public async Task<ActionResult<LoginOutputModel>> Login(
            [FromBody] LoginCommand command) 
            => await this.Send(command);
    }
}
