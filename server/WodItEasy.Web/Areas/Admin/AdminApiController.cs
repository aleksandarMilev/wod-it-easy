namespace WodItEasy.Web.Areas.Admin
{
    using Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static Common.Constants;

    [ApiController]
    [Area(AdminRoleName)]
    [Route("[area]/[controller]")]
    [Authorize(Roles = AdminRoleName)]
    public abstract class AdminApiController : ApiController
    {
    }
}
