namespace WodItEasy.Application.Features.Identity.Commands.Common
{
    public class IdentityOutputModel
    {
        public IdentityOutputModel(string token)
           => this.Token = token;

        public string Token { get; set; }
    }
}
