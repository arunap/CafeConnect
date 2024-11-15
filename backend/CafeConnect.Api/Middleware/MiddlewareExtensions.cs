using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeConnect.Api.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder) => builder.UseMiddleware<ExceptionMiddleware>();

    }
}