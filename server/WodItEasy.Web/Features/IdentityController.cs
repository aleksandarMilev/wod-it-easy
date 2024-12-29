namespace WodItEasy.Web.Features
{
    using System.Threading.Tasks;
    using Application.Features.Identity.Commands.CreateUser;
    using Application.Features.Identity.Commands.LoginUser;
    using Microsoft.AspNetCore.Mvc;

    public class IdentityController : ApiController
    {
        [HttpPost("/register")]
        public async Task<ActionResult> Register(CreateUserCommand command) => await this.Send(command);

        [HttpPost("/login")]
        public async Task<ActionResult<LoginOutputModel>> Login(LoginUserCommand command) => await this.Send(command);
    }
}
