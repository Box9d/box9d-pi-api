using System;
using Box9.Leds.Pi.Api;
using Microsoft.Owin.Hosting;

namespace Box9.Leds.Pi.WebHost
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseUrl = "http://localhost:8001";
            using (WebApp.Start<Startup>(baseUrl))
            {
                Console.WriteLine("Starting app...");
                Console.WriteLine("Press any key to stop");
                Console.ReadKey();
            }
        }
    }
}
