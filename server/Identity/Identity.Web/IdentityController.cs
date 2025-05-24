namespace WodItEasy.Identity.Web
{
    using Application.Commands.Login;
    using Application.Commands.Register;
    using Common.Web.Controllers;
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
