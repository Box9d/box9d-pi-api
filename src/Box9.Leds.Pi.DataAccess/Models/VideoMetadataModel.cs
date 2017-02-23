namespace Box9.Leds.Pi.DataAccess.Models
{
    public class VideoMetadataModel
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public int FrameRate { get; set; }

        public int TotalFrames { get; set; }
    }
}
