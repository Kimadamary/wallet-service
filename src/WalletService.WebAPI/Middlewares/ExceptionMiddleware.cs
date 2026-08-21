using System.Net;
using System.Text.Json;
using WalletService.Domain.Exceptions;











namespace WalletService.WebAPI.Middlewares

{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;


        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

            }
            catch(DomainException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.BadRequest;

                var response = new { error  = ex.Message };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
            catch (Exception ex)
            {
                context.Response.ContentType= "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                var response = new { error = $"Произошла внутренняя ошибка сервера: {ex.Message}" };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }




    }
}
