using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SampleWebApi.Middlewares
{
    public class EnableRequestRewindMiddleware
    {
        private readonly RequestDelegate next;

        public EnableRequestRewindMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();
            return next(context);
        }
    }
}
