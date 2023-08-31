namespace Taskmaster.Middlewares.Extensions;

public static class ErrorLoggingMiddlewareExtensions
{
	public static IApplicationBuilder UseErrorLogging(this IApplicationBuilder builder)
	{
		return builder.UseMiddleware<ErrorLoggingMiddleware>();
	}
}
