using System.Configuration;
using System.Threading;
using Box9.Leds.Pi.Api;
using Microsoft.Owin.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using Box9.Leds.Pi.Domain.VideoPlayback;

namespace Box9.Leds.Pi.VisualizerTestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseUrl = ConfigurationManager.AppSettings["Host"];
            using (var app = WebApp.Start<Startup>(baseUrl))
            {
                Process.Start(baseUrl);
                RunTest().Wait();

                Thread.Sleep(int.MaxValue); // Stop app from executing
            }
        }

        static async Task RunTest()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(5000); // Wait for client page to load

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var playback = new SignalRPlaybackService();

                // Flash black and white for 20 seconds
                while (stopwatch.ElapsedMilliseconds < 20000)
                {
                    playback.DisplayFrame(BlackFrame(100));
                    Thread.Sleep(50);
                    playback.DisplayFrame(WhiteFrame(100));
                    Thread.Sleep(50);
                }
            });
        }

        static byte[] BlackFrame(int numberOfPixels)
        {
            var pixels = new List<byte>();

            for (int i = 0; i < numberOfPixels; i++)
            {
                pixels.Add(0);
                pixels.Add(0);
                pixels.Add(0);
            }

            return pixels.ToArray();
        }

        static byte[] WhiteFrame(int numberOfPixels)
        {
            var pixels = new List<byte>();

            for (int i = 0; i < numberOfPixels; i++)
            {
                pixels.Add(255);
                pixels.Add(255);
                pixels.Add(255);
            }

            return pixels.ToArray();
        }
    }
}
