using System;
using System.Data;

namespace Box9.Leds.Pi.Domain.Dispatch
{
    public interface IDispatcher
    {
        void Dispatch(Action<IDbConnection> action);

        T Dispatch<T>(Func<IDbConnection, T> func);
    }
}
