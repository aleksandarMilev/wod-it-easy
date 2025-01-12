namespace WodItEasy.Web.Service
{
    using System.Security.Claims;
    using Application.Contracts;
    using Microsoft.AspNetCore.Http;

    using static Common.Constants;

    public class CurrentUserService : ICurrentUserService
    {
        private readonly ClaimsPrincipal? user;

        public CurrentUserService(IHttpContextAccessor httpContext) => this.user = httpContext.HttpContext?.User;

        public string? UserId => this.user?.FindFirstValue(ClaimTypes.NameIdentifier);

        public string? Username => this.user?.Identity?.Name;

        public bool IsAdmin => this.user?.IsInRole(AdminRoleName) ?? false;
    }
}
