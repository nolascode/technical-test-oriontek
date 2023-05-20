using Contracts;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using Entities.Exceptions;
using Shared.DataTransferObjects.ErrorDtos;

namespace ClientsManagementApi.Middlewares
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager loggerManager)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            // ApiKeyException => StatusCodes.Status401Unauthorized,
                            // NotFoundException => StatusCodes.Status404NotFound,
                            //UnprocessableException => StatusCodes.Status422UnprocessableEntity,
                            //UnauthorizedException => StatusCodes.Status401Unauthorized,
                            //ForbiddenException => StatusCodes.Status403Forbidden,
                            NotFoundException => StatusCodes.Status404NotFound,
                            UnprocessableEntity => StatusCodes.Status422UnprocessableEntity,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        loggerManager.LogError($"Something went wrong: {contextFeature.Error}");

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message
                        }.ToString());
                    }

                });
            });
        }
    }
}
