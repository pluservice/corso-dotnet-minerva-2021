using Microsoft.AspNetCore.Http;
using Serilog;
using System.IO;

namespace SampleWebApi.Logging
{
    public static class LogHelper
    {
        public static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            var userName = httpContext.User.Identity?.Name;
            diagnosticContext.Set("UserName", userName);

            httpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(httpContext.Request.Body);
            var requestBody = reader.ReadToEndAsync().GetAwaiter().GetResult();
            diagnosticContext.Set("Request", !string.IsNullOrWhiteSpace(requestBody) ? requestBody : null);
        }
    }
}
