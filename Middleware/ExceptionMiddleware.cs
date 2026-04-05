using TestAPI.Middleware;

namespace TestAPI.Middleware

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
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }
        private static Task HandleException(HttpContext context, Exception ex)
        {
            int statusCode = 500;
            string message = "Internal Exception Error";
            if (ex is ApiException apiEx)
            {
                statusCode = apiEx.statusCode;
                message = apiEx.Message;
            }

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "apllication/json";

            return context.Response.WriteAsJsonAsync(new
            {
                status = statusCode,
                message = message
            });
        }
    }
}
