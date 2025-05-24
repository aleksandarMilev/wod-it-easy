namespace WodItEasy.Workouts.Web
{
    using Common.Web;
    using Microsoft.Extensions.DependencyInjection;

    public static class WebConfiguration
    {
        public static IServiceCollection AddWebComponents(
            this IServiceCollection services)
            => services.AddCommonWebComponents();
    }
}
