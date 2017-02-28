using System;
using Box9.Leds.Pi.Core.Identifiers;
using Xunit;

namespace Box9.Leds.Pi.Core.Tests.Identifiers
{
    public class ShortGuidTests
    {
        [Fact]
        public void CreateShortGuidFromTheSameGuidTwice_ReturnsTheSameResult()
        {
            var guid = Guid.NewGuid();

            var shortGuid1 = new ShortGuid(guid);
            var shortGuid2 = new ShortGuid(guid);

            Assert.Equal(shortGuid1.ToString(), shortGuid2.ToString());
        }
    }
}
