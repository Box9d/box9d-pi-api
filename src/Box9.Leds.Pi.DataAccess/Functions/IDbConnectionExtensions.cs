using Dapper.Contrib.Extensions;
using System.Data;
using System.Linq;

namespace Box9.Leds.Pi.DataAccess.Functions
{
    public static class IDbConnectionExtensions
    {
        public static int GetNextId<T>(this IDbConnection conn) where T : class
        {
            return conn.GetAll<T>()
                .Select(t => (int)((dynamic)t).Id)
                .OrderByDescending(t => t)
                .First() + 1;
        }
    }
}
