using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using DotFramework.Infra.DataAccessFactory;
using DotFramework.Infra.Model;
using DotFramework.DynamicQuery;
using DotFramework.DynamicQuery.SqlServer;

namespace DotFramework.Infra.DataAccess.SqlServer
{
    public abstract class SqlServerDataAccessBase : DataAccessBase
    {
        protected override IConnectionHandler CreateConnectionHandler()
        {
            return new SqlServerConnectionHandler();
        }

        protected override DbParameter CreateParameter(string parameterName, object value)
        {
            return new SqlParameter { ParameterName = parameterName, Value = value };
        }

        protected override DbParameter CreateParameter(string parameterName, DbType dbType, object value)
        {
            return new SqlParameter { ParameterName = parameterName, DbType = dbType, Value = value };
        }

        protected override string EvaluateSelectQuery(SelectQuery query)
        {
            return new SqlServerSelectQueryEvaluator(query).ToString();
        }

        protected override string EvaluateUpdateQuery(UpdateQuery query)
        {
            return new SqlServerUpdateQueryEvaluator(query).ToString();
        }

        protected override string EvaluateDeleteQuery(DeleteQuery query)
        {
            return new SqlServerDeleteQueryEvaluator(query).ToString();
        }
    }

    public abstract class SqlServerDataAccessBase<TKey, TModel, TModelCollection> : DataAccessBase<TKey, TModel, TModelCollection>
        where TModel : DomainModelBase, new()
        where TModelCollection : ListBase<TKey, TModel>, new()
    {
        protected override IConnectionHandler CreateConnectionHandler()
        {
            return new SqlServerConnectionHandler();
        }

        protected override DbParameter CreateParameter(string parameterName, object value)
        {
            return new SqlParameter { ParameterName = parameterName, Value = value };
        }

        protected override DbParameter CreateParameter(string parameterName, DbType dbType, object value)
        {
            return new SqlParameter { ParameterName = parameterName, DbType = dbType, Value = value };
        }

        protected override string EvaluateSelectQuery(SelectQuery query)
        {
            return new SqlServerSelectQueryEvaluator(query).ToString();
        }

        protected override string EvaluateUpdateQuery(UpdateQuery query)
        {
            return new SqlServerUpdateQueryEvaluator(query).ToString();
        }

        protected override string EvaluateDeleteQuery(DeleteQuery query)
        {
            return new SqlServerDeleteQueryEvaluator(query).ToString();
        }
    }
}
