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
            catch (NotFoundServiceException ex)
            {
                await ConstructResponse(context, HttpStatusCode.Conflict, ex.Message);
            }
            catch (BadGatewayException ex)
            {
                await ConstructResponse(context, HttpStatusCode.BadGateway, ex.Message);
            }
            catch (RequestTimeoutException ex)
            {
                await ConstructResponse(context, HttpStatusCode.RequestTimeout, ex.Message);
            }
            catch (ServiceUnavailableException ex)
            {
                await ConstructResponse(context, HttpStatusCode.ServiceUnavailable, ex.Message);
            }
            catch (TypeMismatchException ex)
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

