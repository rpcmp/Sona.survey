using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Api.Filters
{
    public class ExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is NotFoundException notFound)
            {
                context.Result = new ObjectResult(notFound.Message)
                {
                    StatusCode = (int)HttpStatusCode.NotFound
                };

                context.ExceptionHandled = true;
            }

            if (context.Exception is BadRequestException badRequest)
            {
                context.Result = new ObjectResult(badRequest.Message)
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };

                context.ExceptionHandled = true;
            }

            if (context.Exception is ConflictException conflict)
            {
                context.Result = new ObjectResult(conflict.Message)
                {
                    StatusCode = (int)HttpStatusCode.Conflict
                };

                context.ExceptionHandled = true;
            }
        }
    }
}
