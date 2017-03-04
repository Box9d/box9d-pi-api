using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using Box9.Leds.Pi.Core.Config;

namespace Box9.Leds.Pi.Domain.VideoPlayback
{
    public class FadecandyPlaybackService : IPlaybackService
    {
        private readonly ClientWebSocket socket;
        private int estimatedNumberOfBits;

        public FadecandyPlaybackService(IOptions<VideoPlayerOptions> options)
        {
            var uri = new Uri(string.Format("ws://127.0.0.1:7890"));

            socket = new ClientWebSocket();

            try
            {
                socket.ConnectAsync(uri, CancellationToken.None).Wait();

                if (socket.State != WebSocketState.Open)
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
            estimatedNumberOfBits = binaryData.Length;

            var data = new List<byte>
            {
                0,0,0,0
            };

            data.AddRange(binaryData);

            socket.SendAsync(new ArraySegment<byte>(data.ToArray()), WebSocketMessageType.Binary, true, CancellationToken.None).Wait();
        }

        public void Dispose()
        {
            socket.Dispose();
        }
    }
}
