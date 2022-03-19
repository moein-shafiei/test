using Microsoft.Data.Sqlite;
using System.Data.Common;
using System.Threading;

namespace DotFramework.Infra.DataAccess.Sqlite
{
    public class SqliteConnectionHandler : ConnectionHandler
    {
        public SqliteConnectionHandler() : base()
        {

        }

        public SqliteConnectionHandler(string connectionString) : base(connectionString)
        {
        }

        protected override DbConnection GetConnection(string connectionString)
        {
            DbConnection connection = new SqliteConnection(ConnectionString);

            const int maxRetryAttempt = 10;
            int retryAttempt = 1;

            while (true)
            {
                try
                {
                    connection.Open();
                    break;
                }
                catch (SqliteException ex)
                {
                    if (ex.ErrorCode == (int)SQLiteErrorCode.Busy)
                    {
                        if (retryAttempt == maxRetryAttempt)
                        {
                            throw;
                        }

                        retryAttempt++;
                        Thread.Sleep(200);
                    }
                    else
                    {
                        throw;
                    }
                }
                catch
                {
                    throw;
                }
            }

            return connection;
        }
    }
}
