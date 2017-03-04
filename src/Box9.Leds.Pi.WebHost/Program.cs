using System;
using System.Configuration;
using Box9.Leds.Pi.Api;
using Microsoft.Owin.Hosting;

namespace Box9.Leds.Pi.WebHost
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseUrl = ConfigurationManager.AppSettings["Host"];
            using (WebApp.Start<Startup>(baseUrl))
            {
                Console.WriteLine("Starting app...");
                Console.WriteLine("Press any key to stop");
                Console.ReadKey();
            }
        }
    }
}
