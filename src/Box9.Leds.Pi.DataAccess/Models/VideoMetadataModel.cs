using Dapper.Contrib.Extensions;

namespace Box9.Leds.Pi.DataAccess.Models
{
    [Table("Video")]
    public class VideoMetadataModel
    {
        [ExplicitKey]
        public int Id { get; set; }

        public string Name { get; set; }

        public double FrameRate { get; set; }
    }
}
