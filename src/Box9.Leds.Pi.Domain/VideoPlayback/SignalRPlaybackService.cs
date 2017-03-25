using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Box9.Leds.Pi.Domain.VideoPlayback
{
    public class SignalRPlaybackService : Hub, IPlaybackService
    { 
        public void Blackout()
        {
            Clients.All.broadcastMessage("Blackout", new { });
        }

        public void DisplayFrame(byte[] binaryData)
        {
            var skip = 0;
            var bytesPerColor = 3;

            var pixelHtmlColors = new List<string>();
            while (skip + bytesPerColor <= binaryData.Length)
            {
                var pixelBytes = binaryData
                    .Skip(skip)
                    .Take(bytesPerColor)
                    .ToArray();

                var htmlColor = ColorTranslator.ToHtml(Color.FromArgb(pixelBytes[0], pixelBytes[1], pixelBytes[2]));
                pixelHtmlColors.Add(htmlColor);

                skip += bytesPerColor;
            }

            Clients.All.broadcastMessage("Frame", pixelHtmlColors);
        }
    }
}
