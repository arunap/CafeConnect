namespace CafeConnect.Api.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder) => builder.UseMiddleware<ExceptionMiddleware>();

    }
}