namespace WodItEasy.Web.Common
{
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public abstract class AuthenticatedApiController : ApiController
    {
    }
}
