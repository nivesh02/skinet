using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Error;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _host;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment host)
        {
            _host = host;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try{
                await _next(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType="application/Json"; 
                context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;

                var response=_host.IsDevelopment() 
                                ? new ApiException((int)HttpStatusCode.InternalServerError,ex.Message,ex.StackTrace.ToString())
                                : new ApiException((int)HttpStatusCode.InternalServerError);
                 
                 var option=new JsonSerializerOptions{PropertyNamingPolicy=JsonNamingPolicy.CamelCase};
                 
                 var Json=JsonSerializer.Serialize(response,option);

                await context.Response.WriteAsync(Json);
            }
        }
    }
}