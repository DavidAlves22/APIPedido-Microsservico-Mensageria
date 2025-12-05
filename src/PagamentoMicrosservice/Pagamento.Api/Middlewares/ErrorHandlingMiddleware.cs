using System.Net;
using System.Text.Json;
using PagamentoMicrosservice.Pagamento.Domain.Exceptions;

namespace PagamentoMicrosservice.Pagamento.Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
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

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "An unexpected error occurred.";
            
            _logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

            switch (exception)
            {
                case DomainException domainException:
                    statusCode = HttpStatusCode.BadRequest; // Ou outro status apropriado para erros de domínio
                    message = domainException.Message;
                    break;
                // Adicione outros casos para tipos específicos de exceções se necessário
                // case UnauthorizedAccessException unauthorizedException:
                //     statusCode = HttpStatusCode.Unauthorized;
                //     message = unauthorizedException.Message;
                //     break;
            }

            context.Response.StatusCode = (int)statusCode;

            var result = JsonSerializer.Serialize(new { error = message });
            await context.Response.WriteAsync(result);
        }
    }

    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}