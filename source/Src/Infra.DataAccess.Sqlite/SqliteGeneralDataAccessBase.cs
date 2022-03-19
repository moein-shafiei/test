using DotFramework.Infra.DataAccessFactory;
using System.Data;
using Microsoft.Data.Sqlite;
using System.Data.Common;

namespace DotFramework.Infra.DataAccess.Sqlite
{
    public abstract class SqliteGeneralDataAccessBase : GeneralDataAccessBase
    {
        protected override IConnectionHandler CreateConnectionHandler()
        {
            return new SqliteConnectionHandler();
        }

        protected override DbParameter CreateParameter(string parameterName, object value)
        {
            return new SqliteParameter { ParameterName = parameterName, Value = value };
        }

        protected override DbParameter CreateParameter(string parameterName, DbType dbType, object value)
        {
            return new SqliteParameter { ParameterName = parameterName, DbType = dbType, Value = value };
        }
    }
}
