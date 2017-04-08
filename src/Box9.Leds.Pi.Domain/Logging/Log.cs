using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Box9.Leds.Pi.Domain.Logging
{
    public class Log : ILog
    {
        private readonly BlockingCollection<string> messages;

        public IEnumerable<string> Messages { get { return messages; } }

        public Log()
        {
            messages = new BlockingCollection<string>();
        }

        public void Add(string message)
        {
            messages.Add(FormatMessage(message));
        }

        public void Add(Exception ex)
        {
            messages.Add(FormatMessage(ex.Message + " " + ex.StackTrace));
        }

        private string FormatMessage(string message)
        {
            return string.Format("| {0} | {1} ", DateTime.Now.ToString("s"), message);
        }
    }
}
