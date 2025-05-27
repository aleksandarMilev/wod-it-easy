namespace WodItEasy.Profile.Web
{
    using Application.Features.Profile.Commands.Create;
    using Application.Features.Profile.Commands.Delete;
    using Application.Features.Profile.Commands.Update;
    using Common.Application.Commands;
    using Common.Web.Controllers;
    using Microsoft.AspNetCore.Mvc;

    public class ProfileController : ApiController 
    {
        [HttpPost]
        public async Task<ActionResult<CreateProfileOutputModel>> Create(
             [FromBody] CreateProfileCommand command)
             => await this.Send(command);

        [HttpPut(Id)]
        public async Task<ActionResult> Update(
            [FromRoute] int id,
            [FromBody] UpdateProfileCommand command)
            => await this.Send(command.SetId(id));

        [HttpDelete(Id)]
        public async Task<ActionResult> Delete(
            [FromRoute] DeleteProfileCommand command)
            => await this.Send(command);
    }
}
