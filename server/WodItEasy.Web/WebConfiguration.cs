namespace WodItEasy.Web
{
    using Microsoft.Extensions.DependencyInjection;
    using WodItEasy.Common.Web;

    public static class WebConfiguration
    {
        public static IServiceCollection AddWebComponents(
            this IServiceCollection services)
            => services.AddCommonWebComponents();
    }
}
