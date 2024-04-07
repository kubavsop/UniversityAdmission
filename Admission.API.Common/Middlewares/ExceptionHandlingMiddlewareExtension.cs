using Microsoft.AspNetCore.Builder;

namespace Admission.API.Common.Middlewares;

public static class ExceptionHandlingMiddlewareExtension
{
    public static void UseExceptionHandlingMiddleware(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}