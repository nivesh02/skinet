using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c=>
            {
                //c.SwaggerDoc("V1",new OpenApiInfo{Title="SkiNet API",Version="V1"}); Note use v1 instead of V1
                c.SwaggerDoc("v1",new OpenApiInfo{Title="SkiNet API",Version="v1"});
            });
            return services;
        }

        public static IApplicationBuilder AddSwaggerAllicaitonDocument(this IApplicationBuilder app)
        {
           app.UseSwagger();
            app.UseSwaggerUI(c=>{c.SwaggerEndpoint("/swagger/v1/swagger.json","SkiNet API v1");});
            return app;
        }
    }
}