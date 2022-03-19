using DotFramework.DynamicQuery;
using DotFramework.Infra.DataAccessFactory;
using DotFramework.Infra.Model;

namespace DotFramework.Infra.BusinessRules
{
    public abstract class GeneralRulesBase<TDataAccess> : RulesBase
        where TDataAccess : IGeneralDataAccessBase
    {
        #region Abstract Properties

        protected abstract TDataAccess DataAccess { get; }

        #endregion

        #region Public Methods

        public SelectQueryResult CustomQuery(CustomQuery CustomQuery)
        {
            return DataAccess.CustomQuery(CustomQuery);
        }

        public dynamic CustomQuerySingleRow(CustomQuery CustomQuery)
        {
            return DataAccess.CustomQuerySingleRow(CustomQuery);
        }

        public void CustomProcedure(CustomQuery CustomQuery)
        {
            DataAccess.CustomProcedure(CustomQuery);
        }

        public SelectQueryResult DynamicGet(SelectQuery query)
        {
            return DataAccess.DynamicSelect(query);
        }

        public dynamic DynamicGetSingleRow(SelectQuery query)
        {
            return DataAccess.DynamicSelectSingleRow(query);
        }

        public bool DynamicEdit(UpdateQuery query)
        {
            return DataAccess.DynamicUpdate(query);
        }

        public bool DynamicRemove(DeleteQuery query)
        {
            return DataAccess.DynamicDelete(query);
        }

        #endregion
    }
}
