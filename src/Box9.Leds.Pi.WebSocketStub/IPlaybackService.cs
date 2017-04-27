using System;

namespace Box9.Leds.Pi.WebSocketStub
{
    public interface IPlaybackService : IDisposable
    {
        void DisplayFrame(byte[] binaryData);

        void Blackout();
    }
}
