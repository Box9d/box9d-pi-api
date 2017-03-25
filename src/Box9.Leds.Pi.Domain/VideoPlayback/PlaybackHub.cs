using Microsoft.AspNet.SignalR;

namespace Box9.Leds.Pi.Domain.VideoPlayback
{
    public class PlaybackHub : Hub
    {
        public void Send(string name, string message)
        {
            Clients.All.broadcastMessage(name, message);
        }
    }
}
