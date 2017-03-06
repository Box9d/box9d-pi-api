using System;
using System.Configuration;
using System.ServiceProcess;
using Box9.Leds.Pi.Api;
using Microsoft.Owin.Hosting;

namespace Box9.Leds.Pi.MonoServiceHost
{
    public partial class WebApiService : ServiceBase
    {
        private IDisposable webApp;

        public WebApiService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            string baseUrl = ConfigurationManager.AppSettings["Host"];
            webApp = WebApp.Start<Startup>(baseUrl);
        }

        protected override void OnStop()
        {
            if (webApp != null)
            {
                webApp.Dispose();
            }
        }
    }
}
