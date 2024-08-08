using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSystem.API
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustMiddleware> _logger;
        public CustMiddleware(RequestDelegate next, ILogger<CustMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public Task Invoke(HttpContext httpContext)
        {
            try
            {
                return _next(httpContext);
            }
            catch (Exception ex)
            {
                var traceId = Guid.NewGuid();
                _logger.LogError($"Error occure while processing the request, TraceId : ${traceId}," +
                    $" Message : ${ex.Message}, StackTrace: ${ex.StackTrace}");
                return null;
            }
           
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustMiddleware>();
        }
    }
}
