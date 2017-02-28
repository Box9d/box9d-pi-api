namespace Box9.Leds.Pi.Domain.VideoPlayback
{
    public class DummyPlaybackService : IPlaybackService
    { 
        public void Blackout()
        {
            return;
        }

        public void DisplayFrame(byte[] binaryData)
        {
            return;
        }

        public void Dispose()
        {
            return;
        }
    }
}
