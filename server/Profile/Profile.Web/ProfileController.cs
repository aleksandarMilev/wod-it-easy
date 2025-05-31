namespace WodItEasy.Profile.Web
{
    using Application.Features.Profile.Commands.Create;
    using Application.Features.Profile.Commands.Delete;
    using Application.Features.Profile.Commands.Update;
    using Application.Features.Profile.Queries.Details;
    using Common.Web.Controllers;
    using Microsoft.AspNetCore.Mvc;

    public class ProfileController : ApiController 
    {
        [HttpGet]
        public async Task<ActionResult<ProfileDetailsOutputModel?>> Details(
            [FromRoute] ProfileDetailsQuery query)
            => await this.Send(query);

        [HttpPost]
        public async Task<ActionResult<CreateProfileOutputModel>> Create(
             [FromBody] CreateProfileCommand command)
             => await this.Send(command);

        [HttpPut]
        public async Task<ActionResult> Update(
            [FromBody] UpdateProfileCommand command)
            => await this.Send(command);

        [HttpDelete]
        public async Task<ActionResult> Delete(
            [FromRoute] DeleteProfileCommand command)
            => await this.Send(command);
    }
}
