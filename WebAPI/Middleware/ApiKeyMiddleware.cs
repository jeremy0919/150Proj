namespace WebAPI.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private const string ApiKey = "X-API-KEY";

        public ApiKeyMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {

            if(!context.Request.Headers.TryGetValue(ApiKey, out var value))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key not found.");
                return;
            }

            var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetValue<string>(ApiKey);
            if(!apiKey.Equals(value))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized.");
                return;
            }

            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                // Log the exception here
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Internal Server Error.");
            }
        }
    }
}
