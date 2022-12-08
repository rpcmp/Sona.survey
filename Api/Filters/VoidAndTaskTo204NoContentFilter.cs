using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Api.Filters
{
    public class VoidAndTaskTo204NoContentFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.ActionDescriptor is ControllerActionDescriptor actionDescriptor)
            {
                var returnType = actionDescriptor.MethodInfo.ReturnType;
                if (returnType == typeof(void) || returnType == typeof(Task))
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status204NoContent;
                }
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}
