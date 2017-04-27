using System;
using System.Collections.Generic;
using System.Net;
using System.Net.WebSockets;
using System.Threading;

namespace Box9.Leds.Pi.WebSocketStub
{
    public class FadecandyPlaybackService : IPlaybackService
    {
        private const int preDataLength = 4;
        private const int bytesPerPixel = 3;

        private readonly ClientWebSocket socket;
        private int estimatedNumberOfBits;

        public FadecandyPlaybackService()
        {
            socket = new ClientWebSocket();

            try
            {
                socket.ConnectAsync(new Uri("ws://192.168.1.15:7890"), CancellationToken.None).Wait();

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
            estimatedNumberOfBits = ((binaryData.Length - preDataLength) / 3) + 1;

            socket.SendAsync(new ArraySegment<byte>(binaryData), WebSocketMessageType.Binary, true, CancellationToken.None).Wait();
        }

        public void Dispose()
        {
            socket.Dispose();
        }
    }
}
