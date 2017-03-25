using System.Configuration;

namespace Box9.Leds.Pi.Core.Config
{
    public class VideoPlayerOptions
    {
        public VideoPlayerOptions()
        {
            UseFadecandyServer = bool.Parse(ConfigurationManager.AppSettings["UseFadecandyServer"]);
            FadecandyPort = 7890;
        }

        public bool UseFadecandyServer { get; set; }

        public int FadecandyPort { get; set; }
    }
}
