using System.Data.Common;
using System.Data.SqlClient;

namespace DotFramework.Infra.DataAccess.SqlServer
{
    public class SqlServerConnectionHandler : ConnectionHandler
    {
        public SqlServerConnectionHandler() : base()
        {

        }

        public SqlServerConnectionHandler(string connectionString) : base(connectionString)
        {
        }

        protected override DbConnection GetConnection(string connectionString)
        {
            DbConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            return connection;
        }
    }
}
