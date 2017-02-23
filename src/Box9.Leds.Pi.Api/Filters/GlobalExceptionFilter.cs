using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Box9.Leds.Pi.Api.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var statusCode = context.Exception is ArgumentException
                ? HttpStatusCode.BadRequest
                : HttpStatusCode.InternalServerError;

            context.HttpContext.Response.StatusCode = (int)statusCode;
            context.Result = new JsonResult(GlobalJsonResult<EmptyResult>.Error(context.Exception, statusCode));

            base.OnException(context);
        }
    }
}