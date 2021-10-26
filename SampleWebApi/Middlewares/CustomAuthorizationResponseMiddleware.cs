using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SampleWebApi.DataAccessLayer;
using System.Threading.Tasks;

namespace SampleWebApi.Middlewares
{
    public class CustomAuthorizationResponseMiddleware
    {
        private readonly RequestDelegate next;
        private readonly DataContext dataContext;
        private readonly ILogger<CustomAuthorizationResponseMiddleware> logger;

        public CustomAuthorizationResponseMiddleware(RequestDelegate next, DataContext dataContext, ILogger<CustomAuthorizationResponseMiddleware> logger)
        {
            this.next = next;
            this.dataContext = dataContext;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await next(context);

            if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
        }
    }
}
