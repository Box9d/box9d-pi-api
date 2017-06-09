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
using System.IO;

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
            var sqliteDatabasePath = Path.Combine(Directory.GetCurrentDirectory(), "box9database.sqlite");
            websocketClient.Load(new LoadRequest
            {
                DatabasePath = sqliteDatabasePath,
                FrameRate = video.FrameRate,
                VideoId = video.Id
            }).Wait();
        }

        public async Task Play(string timeReferenceUrl, DateTime? playAt)
        {
            await websocketClient.Play(timeReferenceUrl, playAt);
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
