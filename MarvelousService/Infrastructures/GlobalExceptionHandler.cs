using MarvelousService.BusinessLayer.Exceptions;
using System.Data.SqlClient;
using System.Net;
using System.Text.Json;

namespace MarvelousService.API.Infrastructure
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ServiceException ex)
            {
                await ConstructResponse(context, HttpStatusCode.NotFound, ex.Message);
            }
            catch (NotFoundServiceException ex)
            {
                await ConstructResponse(context, HttpStatusCode.Conflict, ex.Message);
            }

            catch (ValidationException ex)
            {
                await ConstructResponse(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (SqlException ex)
            {
                await ConstructResponse(context, HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                await ConstructResponse(context, HttpStatusCode.BadRequest, ex.Message);
            }
        }

        private async Task ConstructResponse(HttpContext context, HttpStatusCode code, string message)
        {
            context.Response.ContentType = "applications/json";
            context.Response.StatusCode = (int)code;
            var result = JsonSerializer.Serialize(new { message = message });
            await context.Response.WriteAsync(result);
        }
    }
}

