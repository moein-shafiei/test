using DotFramework.Infra.DataAccessFactory;
using System;
using System.Data.Common;

namespace DotFramework.Infra.DataAccess
{
    public abstract class ConnectionHandler : IConnectionHandler
    {
        public ConnectionHandler()
        {

        }

        public ConnectionHandler(string connectionString) : this()
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }

        public DbConnection Connection
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ConnectionString))
                {
                    throw new ArgumentNullException("ConnectionString");
                }

                return GetConnection(ConnectionString);
            }
        }

        protected abstract DbConnection GetConnection(string connectionString);
    }
}
