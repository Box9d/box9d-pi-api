using Newtonsoft.Json;

namespace Box9.Leds.WebSocket.ApiClient
{
    public class LoadRequest
    {
        [JsonProperty("sqlite_connection_string")]
        public string DatabasePath { get; set; }

        [JsonProperty("video_id")]
        public int VideoId { get; set; }

        [JsonProperty("frame_rate")]
        public double FrameRate { get; set; }
    }
}