namespace Box9.Leds.Pi.Api.ApiRequests
{
    public class VideoMetadataPutRequest
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public double FrameRate { get; set; }

        public int TotalFrames { get; set; }
    }
}
