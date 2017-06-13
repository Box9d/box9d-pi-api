using Newtonsoft.Json;
using System;

namespace Box9.Leds.WebSocket.ApiClient
{
    public class PlayRequest
    {
        [JsonProperty("play_at")]
        public DateTime? PlayAt { get; set; }

        [JsonProperty("time_reference_url")]
        public string TimeReferenceUrl { get; set; }
    }
}
