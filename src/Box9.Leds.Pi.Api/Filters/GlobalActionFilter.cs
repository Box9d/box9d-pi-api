using System.Diagnostics;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Box9.Leds.Pi.Api.Filters
{
    public class GlobalActionFilter : ActionFilterAttribute
    {
        private readonly Stopwatch stopwatch;

        public GlobalActionFilter()
        {
            stopwatch = new Stopwatch();
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            stopwatch.Start();

            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext context)
        {
            var content = (dynamic)(context.Response?.Content as ObjectContent)?.Value;

            if (content != null)
            {
                context.Response.StatusCode = content.StatusCode;
                stopwatch.Stop();
                ((dynamic)(context.Response?.Content as ObjectContent).Value).ResponseTime = stopwatch.ElapsedMilliseconds;
            }

            stopwatch.Stop();
            stopwatch.Reset();

            base.OnActionExecuted(context);
        }
    }
}