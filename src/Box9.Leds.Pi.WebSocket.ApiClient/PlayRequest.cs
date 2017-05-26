using Newtonsoft.Json;
using System;

namespace Box9.Leds.WebSocket.ApiClient
{
    public class PlayRequest
    {
        [JsonProperty("frameRate")]
        public double FrameRate { get; set; }

        [JsonProperty("playAt")]
        public DateTime? PlayAt { get; set; }

        [JsonProperty("timeReferenceUrl")]
        public string TimeReferenceUrl { get; set; }
    }
}
