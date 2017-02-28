using System;

namespace Box9.Leds.Pi.Core.Identifiers
{
    public class ShortGuid
    {
        private readonly string value;

        public ShortGuid(Guid value)
        {
            this.value = Shorten(value);
        }

        public static ShortGuid NewGuid()
        {
            return new ShortGuid(Guid.NewGuid());
        }

        public override string ToString()
        {
            return value.ToString();
        }

        private string Shorten(Guid guid)
        {
            var sGuidLong = Convert.ToBase64String(guid.ToByteArray());

            return sGuidLong.Substring(0, sGuidLong.Length - 3); // Remove last 2 == from base64 string
        }
    }
}
