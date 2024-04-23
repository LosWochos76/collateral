public class CustomMiddleware
{
    private RequestDelegate next;
    private ILogger<CustomMiddleware> logger;

    public CustomMiddleware(RequestDelegate next, ILogger<CustomMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Do something before the next element in the request pipeline
        logger.LogInformation("Before");
        await next(context);
        logger.LogInformation("After");
        // Do something after the next element in the request pipeline
    }
}