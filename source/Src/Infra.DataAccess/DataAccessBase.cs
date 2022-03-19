using DotFramework.Infra.DataAccessFactory;
using DotFramework.Infra.Model;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DotFramework.DynamicQuery;

namespace DotFramework.Infra.DataAccess
{
    public abstract class DataAccessBase : IDataAccessBase, IDisposable
    {
        private static readonly object padlock = new object();

        private IConnectionHandler _ConnectionHandler;
        public IConnectionHandler ConnectionHandler
        {
            get
            {
                if (_ConnectionHandler == null)
                {
                    lock (padlock)
                    {
                        if (_ConnectionHandler == null)
                        {
                            _ConnectionHandler = CreateConnectionHandler();
                        }
                    }
                }

                return _ConnectionHandler;
            }
        }

        public void SetConnectionString(string connectionString)
        {
            if (ConnectionHandler == null)
            {
                throw new ArgumentNullException("ConnectionHandler");
            }

            ConnectionHandler.ConnectionString = connectionString;
        }

        public object DynamicSelectScalar(SelectQuery query)
        {
            return DynamicSelectScalar<Object>(query);
        }

        public TResult DynamicSelectScalar<TResult>(SelectQuery query)
        {
            return ExecuteScalar<TResult>(EvaluateSelectQuery(query), CommandType.Text);
        }

        #region Protected Methods

        protected DbCommand CreateCommand(string commandText, CommandType commandType)
        {
            return CreateCommand(commandText, commandType, null);
        }

        protected DbCommand CreateCommand(string commandText, CommandType commandType, IEnumerable<DbParameter> parameters)
        {
            return CreateCommand(commandText, commandType, parameters.ToArrayOrDefault());
        }

        protected DbCommand CreateCommand(string commandText, CommandType commandType, params DbParameter[] parameters)
        {
            DbCommand command = ConnectionHandler.Connection.CreateCommand();

            command.CommandText = commandText;
            command.CommandType = commandType;

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }

        protected bool ExecuteNonQuery(string commandText, CommandType commandType, out long rowVersion)
        {
            return ExecuteNonQuery(commandText, commandType, out rowVersion, null);
        }

        protected bool ExecuteNonQuery(string commandText, CommandType commandType, out long rowVersion, IEnumerable<DbParameter> parameters)
        {
            return ExecuteNonQuery(commandText, commandType, out rowVersion, parameters.ToArrayOrDefault());
        }

        protected bool ExecuteNonQuery(string commandText, CommandType commandType, out long rowVersion, params DbParameter[] parameters)
        {
            using (DbCommand command = CreateCommand(commandText, commandType, parameters))
            {
                return ExecuteNonQuery(command, out rowVersion);
            }
        }

        protected bool ExecuteNonQuery(string commandText, CommandType commandType)
        {
            return ExecuteNonQuery(commandText, commandType, null);
        }

        protected bool ExecuteNonQuery(string commandText, CommandType commandType, IEnumerable<DbParameter> parameters)
        {
            return ExecuteNonQuery(commandText, commandType, parameters.ToArrayOrDefault());
        }

        protected bool ExecuteNonQuery(string commandText, CommandType commandType, params DbParameter[] parameters)
        {
            using (DbCommand command = CreateCommand(commandText, commandType, parameters))
            {
                return ExecuteNonQuery(command);
            }
        }

        protected bool ExecuteNonQuery(DbCommand command, out long rowVersion)
        {
            if (ExecuteNonQuery(command))
            {
                rowVersion = Convert.ToInt64(command.Parameters["@RowVersion"].Value);
                return true;
            }
            else
            {
                throw new DataAccessCustomException("No record has been affected.");
            }
        }

        protected bool ExecuteNonQuery(DbCommand command)
        {
            int executeResult = command.ExecuteNonQuery();
            command.Connection.Close();

            if (executeResult > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected TResult ExecuteScalar<TResult>(string commandText, CommandType commandType)
        {
            return ExecuteScalar<TResult>(commandText, commandType, null);
        }

        protected TResult ExecuteScalar<TResult>(string commandText, CommandType commandType, IEnumerable<DbParameter> parameters)
        {
            return ExecuteScalar<TResult>(commandText, commandType, parameters.ToArrayOrDefault());
        }

        protected TResult ExecuteScalar<TResult>(string commandText, CommandType commandType, params DbParameter[] parameters)
        {
            using (DbCommand command = CreateCommand(commandText, commandType, parameters))
            {
                return ExecuteScalar<TResult>(command);
            }
        }

        protected TResult ExecuteScalar<TResult>(DbCommand command)
        {
            object obj = command.ExecuteScalar();

            if (obj != null)
            {
                return (TResult)obj;
            }
            else
            {
                return default(TResult);
            }
        }

        #endregion

        #region Abstract Methods

        protected abstract IConnectionHandler CreateConnectionHandler();
        protected abstract DbParameter CreateParameter(string parameterName, object value);
        protected abstract DbParameter CreateParameter(string parameterName, DbType dbType, object value);

        protected abstract string EvaluateSelectQuery(SelectQuery query);
        protected abstract string EvaluateUpdateQuery(UpdateQuery query);
        protected abstract string EvaluateDeleteQuery(DeleteQuery query);

        public void Dispose()
        {
            
        }

        #endregion
    }

    public abstract class DataAccessBase<TKey, TModel, TModelCollection> : DataAccessBase, IDataAccessBase<TKey, TModel, TModelCollection>
        where TModel : DomainModelBase, new()
        where TModelCollection : ListBase<TKey, TModel>, new()
    {
        public virtual TModel Select(TKey key)
        {
            throw new NotImplementedException();
        }

        public virtual Int32 SelectCount()
        {
            throw new NotImplementedException();
        }

        public virtual TModelCollection SelectAll()
        {
            throw new NotImplementedException();
        }

        public virtual TModelCollection SelectAllWithPaging(int PageIndex, int RowsInPage)
        {
            throw new NotImplementedException();
        }

        public virtual bool Insert(TModel model)
        {
            throw new NotImplementedException();
        }

        public virtual bool Update(TModel model)
        {
            throw new NotImplementedException();
        }

        public virtual bool Delete(TKey key)
        {
            throw new NotImplementedException();
        }

        public TModelCollection CustomQuery(CustomQuery CustomQuery)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            CustomQuery.Parameters.ForEach(parameter => parameters.Add(CreateParameter(parameter.Key, parameter.Value)));

            return ExecuteReader(CustomQuery.ProcedureName, CommandType.StoredProcedure, parameters);
        }

        public TModel CustomQuerySingleRow(CustomQuery CustomQuery)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            CustomQuery.Parameters.ForEach(parameter => parameters.Add(CreateParameter(parameter.Key, parameter.Value)));

            return ExecuteReaderSingleRow(CustomQuery.ProcedureName, CommandType.StoredProcedure, parameters);
        }

        public TModelCollection DynamicSelect(SelectQuery query)
        {
            return ExecuteReader(EvaluateSelectQuery(query), CommandType.Text);
        }

        public TModel DynamicSelectSingleRow(SelectQuery query)
        {
            return ExecuteReaderSingleRow(EvaluateSelectQuery(query), CommandType.Text);
        }

        #region Abstract Methods

        protected abstract TModel FillFromDataReader(DbDataReader dr);

        #endregion

        #region Protected Methods

        protected TModelCollection ExecuteReader(string commandText, CommandType commandType)
        {
            return ExecuteReader(commandText, commandType, null);
        }

        protected TModelCollection ExecuteReader(string commandText, CommandType commandType, IEnumerable<DbParameter> parameters)
        {
            return ExecuteReader(commandText, commandType, parameters.ToArrayOrDefault());
        }

        protected TModelCollection ExecuteReader(string commandText, CommandType commandType, params DbParameter[] parameters)
        {
            using (DbCommand command = CreateCommand(commandText, commandType, parameters))
            {
                return ExecuteReader(command);
            }
        }

        protected TModelCollection ExecuteReader(DbCommand command)
        {
            TModelCollection modelCollection = new TModelCollection();

            using (DbDataReader dr = command.ExecuteReader())
            {
                while (dr.Read())
                {
                    modelCollection.Add(FillFromDataReader(dr));
                }

                dr.Close();
                command.Connection.Close();
            }

            return modelCollection;
        }

        protected TModel ExecuteReaderSingleRow(string commandText, CommandType commandType)
        {
            return ExecuteReaderSingleRow(commandText, commandType, null);
        }

        protected TModel ExecuteReaderSingleRow(string commandText, CommandType commandType, IEnumerable<DbParameter> parameters)
        {
            return ExecuteReaderSingleRow(commandText, commandType, parameters.ToArrayOrDefault());
        }

        protected TModel ExecuteReaderSingleRow(string commandText, CommandType commandType, params DbParameter[] parameters)
        {
            using (DbCommand command = CreateCommand(commandText, commandType, parameters))
            {
                return ExecuteReaderSingleRow(command);
            }
        }

        protected TModel ExecuteReaderSingleRow(DbCommand command)
        {
            TModel model = new TModel();

            using (DbDataReader dr = command.ExecuteReader(CommandBehavior.SingleRow))
            {
                while (dr.Read())
                {
                    model = FillFromDataReader(dr);
                }

                dr.Close();
                command.Connection.Close();
            }

            return model;
        }

        #endregion
    }
}
