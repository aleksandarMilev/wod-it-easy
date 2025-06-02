namespace WodItEasy.EmailSender.Web
{
    using Common.Web;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    public static class WebConfiguration
    {
        public static IServiceCollection AddWebComponents(
            this IServiceCollection services,
            IWebHostEnvironment env)
            => services.AddCommonWebComponents(env);
    }
}
