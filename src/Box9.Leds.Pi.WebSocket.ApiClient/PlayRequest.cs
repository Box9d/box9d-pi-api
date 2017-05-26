using Newtonsoft.Json;
using System;

namespace Box9.Leds.WebSocket.ApiClient
{
    public class PlayRequest
    {
        [JsonProperty("frameRate")]
        public double FrameRate { get; set; }

        public DateTime? PlayAt { get; set; }

        public PlayRequest(DateTime? playAt)
        {
            PlayAt = playAt;
        }
    }
}
