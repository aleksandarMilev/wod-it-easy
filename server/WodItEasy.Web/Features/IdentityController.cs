namespace WodItEasy.Web.Features
{
    using System.Threading.Tasks;
    using Application.Features.Identity.Commands.Common;
    using Application.Features.Identity.Commands.CreateUser;
    using Application.Features.Identity.Commands.LoginUser;
    using Common;
    using Microsoft.AspNetCore.Mvc;

    public class IdentityController : ApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult<IdentityOutputModel>> Register(RegisterCommand command) 
            => await this.Send(command);

        [HttpPost("login")]
        public async Task<ActionResult<IdentityOutputModel>> Login(LoginCommand command) 
            => await this.Send(command);
    }
}
