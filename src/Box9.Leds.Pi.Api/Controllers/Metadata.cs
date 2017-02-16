using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace Box9.Leds.Pi.Api.Controllers
{
    public static class Metadata
    {
        public static IEnumerable<Type> GetAllControllers()
        {
            return typeof(Metadata).GetTypeInfo().Assembly.GetTypes()
                .Where(t => t.GetTypeInfo().IsSubclassOf(typeof(Controller)));
        }
    }
}