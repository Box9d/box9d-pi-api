using System;
using System.Data;
using Box9.Leds.Pi.DataAccess;

namespace Box9.Leds.Pi.Domain.Dispatch
{
    public class Dispatcher : IDispatcher
    {
        private readonly IDatabaseFactory databaseFactory;

        public Dispatcher(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        public void Dispatch(Action<IDbConnection> action)
        {
            using (var conn = databaseFactory.Database())
            {
                action(conn);
            }
        }

        public T Dispatch<T>(Func<IDbConnection, T> func)
        {
            using (var conn = databaseFactory.Database())
            {
                return func(conn);
            }
        }
    }
}
