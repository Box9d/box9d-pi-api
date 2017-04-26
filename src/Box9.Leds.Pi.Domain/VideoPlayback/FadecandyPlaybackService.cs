using System;
using System.Collections.Generic;
using Box9.Leds.Pi.Core.Config;
using System.Net;
using WebSocketSharp;

namespace Box9.Leds.Pi.Domain.VideoPlayback
{
    public class FadecandyPlaybackService : IPlaybackService
    {
        private const int preDataLength = 4;
        private const int bytesPerPixel = 3;

        private readonly WebSocket socket;
        private int estimatedNumberOfBits;

        public FadecandyPlaybackService(IOptions<VideoPlayerOptions> options)
        {
            socket = new WebSocket("ws://localhost:7890");
            socket.OnClose += (s, args) =>
            {
                try
                {
                    socket.Connect();
                }
                finally
                {
                }
            };

            try
            {
                socket.Connect();

                if (!socket.IsAlive)
                {
                    throw new Exception("Could not open websocket");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Could not open websocket", ex);
            }
        }

        public void Blackout()
        {
            var data = new List<byte>();
            for (int i = 0; i < estimatedNumberOfBits; i++)
            {
                data.Add(0);
                data.Add(0);
                data.Add(0);
            }

            DisplayFrame(data.ToArray());
        }

        public void DisplayFrame(byte[] binaryData)
        {
            estimatedNumberOfBits = ((binaryData.Length - preDataLength) / 3) + 1;

            socket.Send(binaryData);
        }

        public void Dispose()
        {
            // socket.Dispose();
        }
    }
}
