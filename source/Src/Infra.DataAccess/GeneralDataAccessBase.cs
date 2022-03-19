using DotFramework.DynamicQuery;
using DotFramework.Infra.DataAccessFactory;
using DotFramework.Infra.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace DotFramework.Infra.DataAccess
{
    public abstract class GeneralDataAccessBase : DataAccessBase, IGeneralDataAccessBase
    {
        public SelectQueryResult CustomQuery(CustomQuery CustomQuery)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            CustomQuery.Parameters.ForEach(parameter => parameters.Add(CreateParameter(parameter.Key, parameter.Value)));

            return ExecuteReader(CustomQuery.ProcedureName, CommandType.StoredProcedure, parameters);
        }

        public dynamic CustomQuerySingleRow(CustomQuery CustomQuery)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            CustomQuery.Parameters.ForEach(parameter => parameters.Add(CreateParameter(parameter.Key, parameter.Value)));

            return ExecuteReaderSingleRow(CustomQuery.ProcedureName, CommandType.StoredProcedure, parameters);
        }

        public void CustomProcedure(CustomQuery CustomQuery)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            CustomQuery.Parameters.ForEach(parameter => parameters.Add(CreateParameter(parameter.Key, parameter.Value)));

            ExecuteNonQuery(CustomQuery.ProcedureName, CommandType.StoredProcedure, parameters);
        }

        public SelectQueryResult DynamicSelect(SelectQuery query)
        {
            return ExecuteReader(EvaluateSelectQuery(query), CommandType.Text);
        }

        public dynamic DynamicSelectSingleRow(SelectQuery query)
        {
            return ExecuteReaderSingleRow(EvaluateSelectQuery(query), CommandType.Text);
        }

        public bool DynamicUpdate(UpdateQuery query)
        {
            return ExecuteNonQuery(EvaluateUpdateQuery(query), CommandType.Text);
        }

        public bool DynamicDelete(DeleteQuery query)
        {
            return ExecuteNonQuery(EvaluateDeleteQuery(query), CommandType.Text);
        }

        #region Protected Methods

        protected SelectQueryResult ExecuteReader(string commandText, CommandType commandType)
        {
            return ExecuteReader(commandText, commandType, null);
        }

        protected SelectQueryResult ExecuteReader(string commandText, CommandType commandType, IEnumerable<DbParameter> parameters)
        {
            return ExecuteReader(commandText, commandType, parameters.ToArrayOrDefault());
        }

        protected SelectQueryResult ExecuteReader(string commandText, CommandType commandType, params DbParameter[] parameters)
        {
            using (DbCommand command = CreateCommand(commandText, commandType, parameters))
            {
                return ExecuteReader(command);
            }
        }

        protected SelectQueryResult ExecuteReader(DbCommand command)
        {
            using (DbDataReader dr = command.ExecuteReader())
            {
                SelectQueryResult result = new SelectQueryResult(dr);

                dr.Close();
                command.Connection.Close();

                return result;
            }
        }

        protected dynamic ExecuteReaderSingleRow(string commandText, CommandType commandType)
        {
            return ExecuteReaderSingleRow(commandText, commandType, null);
        }

        protected dynamic ExecuteReaderSingleRow(string commandText, CommandType commandType, IEnumerable<DbParameter> parameters)
        {
            return ExecuteReaderSingleRow(commandText, commandType, parameters.ToArrayOrDefault());
        }

        protected dynamic ExecuteReaderSingleRow(string commandText, CommandType commandType, params DbParameter[] parameters)
        {
            using (DbCommand command = CreateCommand(commandText, commandType, parameters))
            {
                return ExecuteReaderSingleRow(command);
            }
        }

        protected dynamic ExecuteReaderSingleRow(DbCommand command)
        {
            dynamic model = null;

            using (DbDataReader dr = command.ExecuteReader(CommandBehavior.SingleRow))
            {
                if (dr.HasRows)
                {
                    foreach (IDataRecord record in dr)
                    {
                        model = new DataRecordDynamicWrapper(record);
                        break;
                    }
                }

                dr.Close();
                command.Connection.Close();
            }

            return model;
        }

        #endregion
    }
}
