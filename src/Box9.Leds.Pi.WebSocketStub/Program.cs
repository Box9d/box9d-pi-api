using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Box9.Leds.Pi.WebSocketStub
{
    class Program
    {
        static void Main(string[] args)
        {
            const int timeout = 20000;
            const int cyclicPeriod = 500;

            var whiteFrame = GenerateFrame(100, 255, 255, 255);
            var blackFrame = GenerateFrame(100, 0, 0, 0);

            using (var playback = new FadecandyPlaybackService())
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                while (stopwatch.ElapsedMilliseconds < timeout)
                {
                    playback.DisplayFrame(whiteFrame);
                    Thread.Sleep(cyclicPeriod / 2);
                    playback.DisplayFrame(blackFrame);
                    Thread.Sleep(cyclicPeriod / 2);
                }
            }
        }

        static byte[] GenerateFrame(int numberOfPixels, byte r, byte g, byte b)
        {
            var data = new List<byte>
            {
                0,
                0,
                0
            };

            for (int i = 0; i < numberOfPixels; i++)
            {
                data.Add(r);
                data.Add(g);
                data.Add(b);
            }

            return data.ToArray();
        }
    }
}