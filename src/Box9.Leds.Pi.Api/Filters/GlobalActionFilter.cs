using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Box9.Leds.Pi.Api.Filters
{
    public class GlobalActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result != null && context.Result.GetType() == typeof(ObjectResult))
            {
                var resultValue = ((ObjectResult)context.Result).Value;

                if (resultValue.GetType().GetGenericTypeDefinition() == typeof(GlobalJsonResult<>))
                {
                    context.HttpContext.Response.StatusCode = (int)((dynamic)resultValue).StatusCode;
                }
            }

            base.OnActionExecuted(context);
        }
    }
}