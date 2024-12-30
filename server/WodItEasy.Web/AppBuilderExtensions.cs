namespace WodItEasy.Web
{
    using Microsoft.AspNetCore.Builder;

    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder UseAppEndpoints(this IApplicationBuilder app) => app.UseEndpoints(e => e.MapControllers());

        public static IApplicationBuilder UseAllowedCors(this IApplicationBuilder app)
            => app
                 .UseCors(opt =>
                 {
                     opt.AllowAnyOrigin();
                     opt.AllowAnyHeader();
                     opt.AllowAnyMethod();
                 });
    }
}