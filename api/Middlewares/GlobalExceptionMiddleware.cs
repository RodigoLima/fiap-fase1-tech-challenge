using fiap_fase1_tech_challenge.Exceptions;
using Serilog;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace fiap_fase1_tech_challenge.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(FiapCloudGamesException ex)
            {
                Log.Error(ex, string.Join(" | ", ex.GetErrorMessages()));
                context.Response.StatusCode = (int)ex.GetStatusCode();
                context.Response.ContentType = "application/json";

                var result = JsonSerializer.Serialize(new { errors = ex.GetErrorMessages() });
                await context.Response.WriteAsync(result);
            }
            catch(ValidationException ex)
            {
                Log.Error(ex, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var result = JsonSerializer.Serialize(new { error = ex.Message });
                await context.Response.WriteAsync(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                Log.Error(ex, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.ContentType = "application/json";

                var result = JsonSerializer.Serialize(new { error = ex.Message });
                await context.Response.WriteAsync(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var result = JsonSerializer.Serialize(new { error = "Erro interno no servidor" });
                await context.Response.WriteAsync(result);
            }
        }
    }
}
