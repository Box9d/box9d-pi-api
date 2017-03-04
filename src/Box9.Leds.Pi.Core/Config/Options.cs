using System;
namespace Box9.Leds.Pi.Core.Config
{
    public class Options<T> : IOptions<T> where T: class, new()
    {
        public T Value
        {
            get
            {
                return Activator.CreateInstance<T>();
            }
        }
    }
}
