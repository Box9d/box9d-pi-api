using System;

namespace Box9.Leds.Pi.Domain.VideoPlayback
{
    public interface IPlaybackService : IDisposable
    {
        void DisplayFrame(byte[] binaryData);

        void Blackout();
    }
}
