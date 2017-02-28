using System.Threading;

namespace Box9.Leds.Pi.Domain.VideoPlayback
{
    public class VideoPlayerMonitor : IVideoPlayerMonitor
    {
        private int frameRate;

        private Timer timer;
        private int framesReceivedSinceLastTick;

        public int FrameRate { get; private set; }

        public void PlaybackStarted()
        {
            timer = new Timer((state) =>
            {
                UpdateFrameRate();
            }, null, 0, 1000);
        }

        public void FrameReceived()
        {
            framesReceivedSinceLastTick++;
        }

        public void PlaybackFinished()
        {
            timer.Dispose();
        }

        private void UpdateFrameRate()
        {
            FrameRate = framesReceivedSinceLastTick;
            framesReceivedSinceLastTick = 0;
        }
    }
}
