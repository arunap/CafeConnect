using System.Net;
using System.Reflection;
using System.Text.Json;
using CafeConnect.Api.Dtos;
using log4net;
using Microsoft.EntityFrameworkCore;

namespace CafeConnect.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private static readonly JsonSerializerOptions JSON_OPTIONS = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        private readonly RequestDelegate _next;
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);


        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Proceed to the next middleware in the pipeline
                await _next(context);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                await HandleConcurencyAsync(context, ex);
            }
            catch (Exception ex)
            {
                // Handle the exception
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleConcurencyAsync(HttpContext context, DbUpdateConcurrencyException exception)
        {
            _logger.Warn("Concurrency exception occurred. Data may have been modified by another user.", exception);

            context.Response.StatusCode = (int)HttpStatusCode.Conflict;
            context.Response.ContentType = "application/json";

            // Create a response object with error details
            var response = new ErrorDto
            {
                StatusCode = context.Response.StatusCode,
                Message = "Concurrency exception occurred. Data may have been modified by another user.",
                Errors = ["The data has been updated by another user. Please reload the data."]
            };

            var responseJson = JsonSerializer.Serialize(response, JSON_OPTIONS);
            return context.Response.WriteAsync(responseJson);
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Log the exception
            _logger.Error($"Something went wrong.", exception);

            // Set the response status code
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            // Create a response object with error details
            var response = new ErrorDto
            {
                StatusCode = context.Response.StatusCode,
                Message = "Error occured in the server!",
                Errors = [exception.Message]
            };

            // Serialize and return the response as JSON
            var responseJson = JsonSerializer.Serialize(response, JSON_OPTIONS);

            return context.Response.WriteAsync(responseJson);
        }
    }
}