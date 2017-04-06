using System;
using System.Data;
using System.IO;
using System.Linq;
using Box9.Leds.Pi.Database;
using Dapper;
using System.Configuration;
using System.Data.SQLite;

namespace Box9.Leds.Pi.DataAccess
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private readonly string dbFilePath;
        private readonly IScriptDiscovery scriptDiscovery;
        private Func<IDbConnection> database;

        public Func<IDbConnection> Database
        {
            get
            {
                return database;
            }
            private set
            {
                database = value;
            }
        }

        public DatabaseFactory(IScriptDiscovery scriptDiscovery)
        {
            dbFilePath = Path.Combine(Directory.GetCurrentDirectory(), "box9database.sqlite");
            database = () =>
            {
                if (bool.Parse(ConfigurationManager.AppSettings["MonoSqlite"]))
                {
                    var conn = new Mono.Data.Sqlite.SqliteConnection(string.Format("Data Source={0};", dbFilePath));
                    conn.Open();
                    return conn;
                }
                else
                {
                    var conn = new SQLiteConnection(string.Format("Data Source={0};", dbFilePath));
                    conn.Open();
                    return conn;
                }
            };
            this.scriptDiscovery = scriptDiscovery;

            Initialize();
        }

        private void Initialize()
        {
            // Apply update scripts on init of Database Factory
            using (var conn = Database())
            {
                conn.Execute("CREATE TABLE IF NOT EXISTS Versions(id INTERGER PRIMARY KEY, name TEXT NOT NULL)");
                var executedScriptIds = conn.Query<ExecutedScript>("SELECT * FROM Versions").Select(scr => scr.Id);

                foreach (var script in scriptDiscovery.Discover()
                    .Where(scr => !executedScriptIds.Contains(scr.Id))
                    .OrderBy(scr => scr.Id))
                {
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            conn.Execute(script.Sql, transaction: transaction);
                            conn.Execute("INSERT INTO Versions(id, name) VALUES(@Id, @Name)", new { script.Id, script.Name });
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();

                            var exception = new Exception(string.Format("Whilst trying to apply script '{0}', with Id '{1}'", script.Name, script.Id), ex);

                            // This ensures any further calls to the database are met with exception until script is amended
                            Database = () => { throw exception; };
                        }
                    }
                }
            }
        }
    }
}
