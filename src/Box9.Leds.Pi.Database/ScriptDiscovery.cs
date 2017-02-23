using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Box9.Leds.Pi.Database
{
    public class ScriptDiscovery : IScriptDiscovery
    {
        public IEnumerable<IScript> Discover()
        {
            var scripts = typeof(ScriptDiscovery).GetTypeInfo().Assembly.GetTypes()
                .Where(t =>
                {
                    // Ensure script is class, inherits from IScript and has a parameterless constructor
                    var typeInfo = t.GetTypeInfo();
                    return typeInfo.IsClass
                        && typeof(IScript).GetTypeInfo().IsAssignableFrom(t)
                        && typeInfo.GetConstructors().Any(c => !c.GetParameters().Any());
                });

            foreach (var script in scripts)
            {
                yield return (IScript)Activator.CreateInstance(script);
            }
        }
    }
}
