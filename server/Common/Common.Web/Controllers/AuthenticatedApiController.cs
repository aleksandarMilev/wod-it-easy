namespace WodItEasy.Common.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public abstract class AuthenticatedApiController : ApiController
    {
    }
}
