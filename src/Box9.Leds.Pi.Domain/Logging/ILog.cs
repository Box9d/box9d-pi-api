using System;
using System.Collections.Generic;

namespace Box9.Leds.Pi.Domain.Logging
{
    public interface ILog
    {
        IEnumerable<string> Messages { get; }

        void Add(string message);

        void Add(Exception ex);
    }
}
