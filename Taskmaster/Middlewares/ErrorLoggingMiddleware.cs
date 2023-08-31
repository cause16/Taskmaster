namespace Taskmaster.Middlewares;

public class ErrorLoggingMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<ErrorLoggingMiddleware> _logger;

	public ErrorLoggingMiddleware(RequestDelegate next, ILogger<ErrorLoggingMiddleware> logger)
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
			_logger.LogError(ex, "Request processing error. {ErrorMessage}", ex.Message);
			context.Response.StatusCode = 500;
		}
	}
}
