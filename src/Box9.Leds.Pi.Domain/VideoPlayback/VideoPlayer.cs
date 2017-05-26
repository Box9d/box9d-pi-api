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
        private readonly IDispatcher dispatcher;
        private readonly ILog log;
        private readonly WebSocketApiClient websocketClient;

        public VideoPlayer(IPlaybackServiceFactory playbackServiceFactory,
            IDispatcher dispatcher,
            ILog log)
        {
            this.playbackServiceFactory = playbackServiceFactory;
            this.dispatcher = dispatcher;
            this.log = log;
            this.websocketClient = new WebSocketApiClient(new Uri("http://localhost:8003"));
        }

        public void Load(Video video)
        {
            var frames = dispatcher.Dispatch(video.DispatchGetFramesForVideo());
            websocketClient.Load(new LoadRequest { Frames = frames.Select(f => f.BinaryData.Select(d => (int)d).ToArray()).ToArray() }).Wait();
        }

        public async Task Play(DateTime? playAt, Video video)
        {
            await websocketClient.Play(playAt, video.FrameRate);
        }

        public async Task Stop()
        {
            await websocketClient.Stop();
        }

        internal int CalculateFramePosition(Video video, long elapsedMilliseconds)
        {
            return (int)Math.Round((elapsedMilliseconds * video.FrameRate) / 1000, 0);
        }
    }
}
