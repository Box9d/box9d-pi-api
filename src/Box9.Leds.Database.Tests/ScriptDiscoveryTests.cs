﻿using System.Linq;
using Box9.Leds.Pi.Database;
using Xunit;

namespace Box9.Leds.Database.Tests
{
    public class ScriptDiscoveryTests
    {
        [Fact]
        public void AllScriptsHaveAUniqueId()
        {
            var scriptDiscovery = new ScriptDiscovery();
            var result = scriptDiscovery.Discover();

            var groupedByOrder = result.GroupBy(r => r.Id, r => r, (o, s) => 
                new { Id = o, Scripts = s });

            foreach (var groupByOrder in groupedByOrder)
            {
                if (groupByOrder.Scripts.Count() > 1)
                {
                    var message = string.Format("{0} scripts were found with the Id {1} but this should be unique to each script",
                        groupByOrder.Scripts,
                        groupByOrder.Id);

                    Assert.True(false, message);
                }
            }
        }
    }
}
