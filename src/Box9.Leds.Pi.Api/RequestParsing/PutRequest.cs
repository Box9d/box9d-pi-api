using System;

namespace Box9.Leds.Pi.Api.RequestParsing
{
    public static class PutRequest
    {
        public static void DoThisIfValueIsNotDefault<TValue>(TValue value, Action<TValue> action)
        {
            if (!value.Equals(default(TValue)))
            {
                action(value);
            }
        }
    }
}
