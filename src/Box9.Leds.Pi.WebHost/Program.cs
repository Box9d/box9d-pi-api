using System;
using System.Configuration;
using System.Threading;
using Box9.Leds.Pi.Api;
using Microsoft.Owin.Hosting;

namespace Box9.Leds.Pi.WebHost
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseUrl = ConfigurationManager.AppSettings["Host"];
            var app = WebApp.Start<Startup>(baseUrl);

            Thread.Sleep(int.MaxValue); // Stop app from executing
        }
    }
}
