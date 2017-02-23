using System;
using System.Data;

namespace Box9.Leds.Pi.DataAccess
{
    public interface IDatabaseFactory
    {
        Func<IDbConnection> Database { get; }
    }
}
