using System;

namespace Box9.Leds.Pi.Api.RequestParsing
{
    public class PutRequestValue<TValue>
    {
        public bool IsDefault { get; }

        public TValue Value { get; }

        public PutRequestValue(TValue value)
        {
            Value = value;
            IsDefault = value.Equals(default(TValue));
        }

        public void DoThis(Action<TValue> action)
        {
            if (!IsDefault)
            {
                action(Value);
            }
        }
    }
}
