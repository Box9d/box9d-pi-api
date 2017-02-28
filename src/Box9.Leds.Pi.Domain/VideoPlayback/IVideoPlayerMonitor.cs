namespace Box9.Leds.Pi.Domain.VideoPlayback
{
    public interface IVideoPlayerMonitor
    {
        int FrameRate { get; }

        void PlaybackStarted();

        void FrameReceived();

        void PlaybackFinished();
    }
}
