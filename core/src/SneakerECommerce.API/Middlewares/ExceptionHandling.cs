using SneakerECommerce.Application.Common;
using System.Net;

namespace SneakerECommerce.API.Middlewares
{
    public class ExceptionHandling
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandling> _logger;
        public ExceptionHandling(RequestDelegate next, ILogger<ExceptionHandling> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Log the exception (optional)
            _logger.LogError(exception, "An unhandled exception occurred.");
            var statusCode = HttpStatusCode.InternalServerError;
            string result;
            switch (exception)
            {
                case KeyNotFoundException _:
                    statusCode = HttpStatusCode.NotFound;
                    result = exception.Message;
                    break;

                case NullReferenceException _:
                    statusCode = HttpStatusCode.BadRequest;
                    result = exception.Message;
                    break;

                default:
                    result = "An unexpected error occurred.";
                    break;
            }
            // Set the status code and return the error response
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsJsonAsync(Result.Failure(exception.Message));
        }
    }
}
