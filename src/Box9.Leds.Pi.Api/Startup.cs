using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;
using Autofac;
using Autofac.Integration.WebApi;
using Box9.Leds.Pi.Api;
using Box9.Leds.Pi.Api.Autofac;
using Box9.Leds.Pi.Api.Filters;
using Microsoft.Owin;
using NSwag.AspNet.Owin;
using Owin;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System;

[assembly: OwinStartup(typeof(Startup))]
namespace Box9.Leds.Pi.Api
{
    public class Startup
    {
        // Bug: Mono does not like DI with Autofac :(
        public static IContainer Container { get; private set; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(name: "ControllerOnly", routeTemplate: "api/{controller}");
            config.Routes.MapHttpRoute(name: "ControllerAndAction", routeTemplate: "api/{controller}/{action}");
            config.Routes.MapHttpRoute(name: "ControllerActionAndId", routeTemplate: "api/{controller}/{action}/{id}");
            config.Routes.MapHttpRoute(name: "VideoIdControllerAndAction", routeTemplate: "api/Video/{videoId}/{controller}/{action}");
            config.Filters.Add(new GlobalExceptionFilter());
            config.Filters.Add(new GlobalActionFilter());

            if (Type.GetType("Mono.Runtime") != null)
            {
                config.MessageHandlers.Add(new MonoPatchingDelegatingHandler());
            }

            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(typeof(Startup).Assembly);
            builder.RegisterWebApiFilterProvider(config);
            builder.RegisterWebApiModelBinderProvider();
            builder.RegisterModule<ApiModule>();

            var container = builder.Build();
            Container = container;

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly, new SwaggerUiOwinSettings
            {
                DefaultUrlTemplate = "api/{controller}/{action}",
                IsAspNetCore = false
            });

            app.UseCustomFileServer();

            app.MapSignalR();

            // app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);

            config.MapHttpAttributeRoutes();
            config.EnsureInitialized();
        }

        /// <summary>
        /// Work around a bug in mono's implementation of System.Net.Http where calls to HttpRequestMessage.Headers.Host will fail unless we set it explicitly.
        /// This should be transparent and cause no side effects.
        /// </summary>
        private class MonoPatchingDelegatingHandler : DelegatingHandler
        {
            protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                request.Headers.Host = request.Headers.GetValues("Host").FirstOrDefault();
                return await base.SendAsync(request, cancellationToken);
            }
        }
    }
}
