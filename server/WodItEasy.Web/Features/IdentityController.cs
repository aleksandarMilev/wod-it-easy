namespace WodItEasy.Web.Features
{
    using System.Threading.Tasks;
    using Application.Contracts;
    using Application.Features.Identity;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentity identity;

        public IdentityController(IIdentity identity) => this.identity = identity;

        [HttpPost]
        [Route(nameof(this.Register))]
        public async Task<ActionResult> Register(UserInputModel model)
        {
            var result = await this.identity.Register(model);

            if (result.Succeeded)
            {
                return this.Ok();
            }

            return this.BadRequest(result.Errors);
        }

        [HttpPost]
        [Route(nameof(this.Login))]
        public async Task<ActionResult<LoginOutputModel>> Login(UserInputModel model)
        {
            var result = await this.identity.Login(model);

            if (result.Succeeded)
            {
                return result.Data;
            }

            return this.BadRequest(result.Errors);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get() => this.Ok(this.User?.Identity?.Name);
    }
}
