using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SampleWebApi.DataAccessLayer;
using System.Threading.Tasks;

namespace SampleWebApi.Middlewares
{
    public class IpFilteringMiddleware
    {
        private readonly RequestDelegate next;
        private readonly DataContext dataContext;
        private readonly ILogger<IpFilteringMiddleware> logger;

        public IpFilteringMiddleware(RequestDelegate next, DataContext dataContext, ILogger<IpFilteringMiddleware> logger)
        {
            this.next = next;
            this.dataContext = dataContext;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var remoteIpAddress = context.Connection.RemoteIpAddress;

            var isAllowed = true; //await dataContext.IPAddresses.AnyAsync(ip => ip.Value == isAllowed);
            if (isAllowed)
            {
                await next(context);
            }

            logger.LogWarning("Tentativo di accesso non autorizzato da parte dell'IP {IpAddress}", remoteIpAddress);
            context.Response.StatusCode = StatusCodes.Status423Locked;
        }
    }
}
