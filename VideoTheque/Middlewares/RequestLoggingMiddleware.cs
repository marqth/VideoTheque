using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using NLog;

namespace VideoTheque.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly NLog.ILogger Logger = LogManager.GetCurrentClassLogger();

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Logger.Info($"Handling request: {context.Request.Method} {context.Request.Path}");
            await _next(context);
            Logger.Info($"Finished handling request: {context.Request.Method} {context.Request.Path}");
        }
    }
}