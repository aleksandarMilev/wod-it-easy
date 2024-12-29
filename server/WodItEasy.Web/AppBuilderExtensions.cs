namespace WodItEasy.Web
{
    using Microsoft.AspNetCore.Builder;

    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerExtension(this IApplicationBuilder appBuilder)
        {
            appBuilder
                .UseSwagger(opt => opt.SerializeAsV2 = true)
                .UseSwaggerUI(opt =>
                {
                    opt.SwaggerEndpoint("swagger/v1/swagger.json", "WodItEasy API");
                    opt.RoutePrefix = string.Empty;
                });

            return appBuilder;
        }
    }
}
