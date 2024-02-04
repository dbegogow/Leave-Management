namespace LeaveManagement.Api.Middlewares;

using System.Net;

using LeaveManagement.Api.Models;
using LeaveManagement.Application.Exceptions;

public class ExceptionMiddleware
{
    private RequestDelegate next;

    public ExceptionMiddleware(RequestDelegate next)
        => this.next = next;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await this.next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        var statusCode = HttpStatusCode.InternalServerError;

        CustomProblemDetails problem = new();

        switch (ex)
        {
            case BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;

                problem.Title = badRequestException.Message;
                problem.Status = (int)statusCode;
                problem.Detail = badRequestException.InnerException?.Message;
                problem.Type = nameof(BadRequestException);
                problem.Errors = badRequestException.ValidationErrors;

                break;
            case NotFoundException notFound:
                statusCode = HttpStatusCode.NotFound;

                problem.Title = notFound.Message;
                problem.Status = (int)statusCode;
                problem.Detail = notFound.InnerException?.Message;
                problem.Type = nameof(NotFoundException);

                break;
            default:
                problem.Title = ex.Message;
                problem.Status = (int)statusCode;
                problem.Detail = ex.StackTrace;
                problem.Type = nameof(HttpStatusCode.InternalServerError);

                break;
        }

        httpContext.Response.StatusCode = (int)statusCode;

        await httpContext.Response.WriteAsJsonAsync(problem);
    }
}
