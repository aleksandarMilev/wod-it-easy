namespace WodItEasy.Common.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Constants;

    [ApiController]
    [Area(AdminRoleName)]
    [Route("[area]/[controller]")]
    [Authorize(Roles = AdminRoleName)]
    public abstract class AdminApiController : ApiController
    {
    }
}
