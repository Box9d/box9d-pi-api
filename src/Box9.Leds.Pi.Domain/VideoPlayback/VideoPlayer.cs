using System;
using System.Threading.Tasks;
using Box9.Leds.Pi.Domain.Logging;
using Box9.Leds.WebSocket.ApiClient;
using System.IO;

namespace Box9.Leds.Pi.Domain.VideoPlayback
{
    public class VideoPlayer
    {
        private readonly ILog log;
        private readonly WebSocketApiClient websocketClient;

        public VideoPlayer()
        {
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
    }
}
