using System.Data.Common;
using DotFramework.Infra.DataAccessFactory;
using DotFramework.Infra.Model;
using System.Data;
using DotFramework.DynamicQuery;
using System;
using Microsoft.Data.Sqlite;

namespace DotFramework.Infra.DataAccess.Sqlite
{
    public abstract class SqliteDataAccessBase : DataAccessBase
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

        protected override string EvaluateSelectQuery(SelectQuery query)
        {
            throw new NotImplementedException();
        }

        protected override string EvaluateUpdateQuery(UpdateQuery query)
        {
            throw new NotImplementedException();
        }

        protected override string EvaluateDeleteQuery(DeleteQuery query)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class SqliteDataAccessBase<TKey, TModel, TModelCollection> : DataAccessBase<TKey, TModel, TModelCollection>
        where TModel : DomainModelBase, new()
        where TModelCollection : ListBase<TKey, TModel>, new()
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

        protected override string EvaluateSelectQuery(SelectQuery query)
        {
            throw new NotImplementedException();
        }

        protected override string EvaluateUpdateQuery(UpdateQuery query)
        {
            throw new NotImplementedException();
        }

        protected override string EvaluateDeleteQuery(DeleteQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
