using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Box9.Leds.Pi.Api.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.Result = new JsonResult(GlobalJsonResult<EmptyResult>.Error(context.Exception));
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            base.OnException(context);
        }
    }
}