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
using Box9.Leds.WebSocket.ApiClient;

namespace Box9.Leds.Pi.Domain.VideoPlayback
{
    public class VideoPlayer : IVideoPlayer
    {
        private readonly IPlaybackServiceFactory playbackServiceFactory;
        private readonly IVideoPlayerMonitor videoPlayerMonitor;
        private readonly IDispatcher dispatcher;
        private readonly ILog log;
        private readonly WebSocketApiClient websocketClient;

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
            this.websocketClient = new WebSocketApiClient(new Uri("http://localhost:8003"));
        }

        public VideoPlaybackToken Load(Video video)
        {
            var frames = dispatcher.Dispatch(video.DispatchGetFramesForVideo());
            websocketClient.Load(new LoadRequest { Frames = frames.Select(f => f.BinaryData.Select(d => (int)d).ToArray()).ToArray() }).Wait();

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

            using (var playback = playbackServiceFactory.GetPlaybackService())
            {
                playback.Blackout();
                videoPlayerMonitor.PlaybackFinished();
            }
        }

        internal async Task Play(Video video)
        {
            await websocketClient.Play(video.FrameRate);
        }

        internal int CalculateFramePosition(Video video, long elapsedMilliseconds)
        {
            return (int)Math.Round((elapsedMilliseconds * video.FrameRate) / 1000, 0);
        }
    }
}
