using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Box9.Leds.Pi.Core.Identifiers;
using Box9.Leds.Pi.Domain.Dispatch;
using Box9.Leds.Pi.Domain.VideoFrames;
using Box9.Leds.Pi.Domain.Videos;
using Box9.Leds.Pi.Domain.Logging;

namespace Box9.Leds.Pi.Domain.VideoPlayback
{
    public class VideoPlayer : IVideoPlayer
    {
        private readonly IPlaybackServiceFactory playbackServiceFactory;
        private readonly IVideoPlayerMonitor videoPlayerMonitor;
        private readonly IDispatcher dispatcher;
        private readonly ILog log;

        private IEnumerable<VideoFrame> frames;
        private KeyValuePair<string, CancellationTokenSource> cancellationTokenPair;

        public VideoPlayer(IPlaybackServiceFactory playbackServiceFactory,
            IVideoPlayerMonitor videoPlayerMonitor,
            IDispatcher dispatcher,
            ILog log)
        {
            this.playbackServiceFactory = playbackServiceFactory;
            this.videoPlayerMonitor = videoPlayerMonitor;
            this.dispatcher = dispatcher;
            this.log = log;
        }

        public VideoPlaybackToken Load(Video video)
        {
            frames = dispatcher.Dispatch(video.DispatchGetFramesForVideo());

            var cancellationTokenSource = new CancellationTokenSource();
            var playbackToken = ShortGuid.NewGuid().ToString();
            cancellationTokenPair = new KeyValuePair<string, CancellationTokenSource>(
                playbackToken,
                cancellationTokenSource);

            return new VideoPlaybackToken(playbackToken);
        }

        public async Task PlayAsync(Video video, string playbackToken)
        {    
            if (cancellationTokenPair.Key != playbackToken)
            {
                throw new ArgumentException("Playback token not recognized");
            }

            await Task.Run(() => Play(video), cancellationTokenPair.Value.Token); 
        }

        public void Stop(string playbackToken)
        {
            if (cancellationTokenPair.Key != playbackToken)
            {
                throw new ArgumentException("Playback token not recognized");
            }

            cancellationTokenPair.Value.Cancel();

            using (var playback = playbackServiceFactory.Playback())
            {
                playback.Blackout();
                videoPlayerMonitor.PlaybackFinished();
            }
        }

        internal void Play(Video video)
        {
            try
            {
                using (var playback = playbackServiceFactory.Playback())
                {
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();
                    videoPlayerMonitor.PlaybackStarted();

                    while (stopwatch.ElapsedMilliseconds < (frames.Count() / video.FrameRate) * 1000)
                    {
                        var framePosition = CalculateFramePosition(video, stopwatch.ElapsedMilliseconds);
                        var frame = frames.SingleOrDefault(f => f.Position == framePosition);

                        if (frame != null)
                        {
                            playback.DisplayFrame(frame.BinaryData);
                        }

                        videoPlayerMonitor.FrameReceived();
                    }

                    playback.Blackout();
                    videoPlayerMonitor.PlaybackFinished();
                }
            }
            catch (Exception ex)
            {
                log.Add(ex);
            }
        }

        internal int CalculateFramePosition(Video video, long elapsedMilliseconds)
        {
            return (int)Math.Round((elapsedMilliseconds * video.FrameRate) / 1000, 0);
        }
    }
}
