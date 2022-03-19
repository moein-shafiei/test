using DotFramework.Infra.BusinessRules;
using DotFramework.Infra.DataAccessFactory;
using DotFramework.Infra.Model;
using DotFramework.Infra.ServiceFactory;
using DotFramework.DynamicQuery;

namespace DotFramework.Infra.BusinessFacade
{
    public abstract class GeneralServiceBase<TBusinessRules, TDataAccess> : ServiceBase, IGeneralServiceBase
        where TDataAccess : IGeneralDataAccessBase
        where TBusinessRules : GeneralRulesBase<TDataAccess>, new()
    {
        public TBusinessRules BusinessRules
        {
            get
            {
                return BusinessRulesFactory.Instance.GetBusinessRules<TBusinessRules>();
            }
        }

        #region Public Methods

        public SelectQueryResult CustomQuery(CustomQuery CustomQuery)
        {
            return BusinessRules.CustomQuery(CustomQuery);
        }

        public dynamic CustomQuerySingleRow(CustomQuery CustomQuery)
        {
            return BusinessRules.CustomQuerySingleRow(CustomQuery);
        }

        public void CustomProcedure(CustomQuery CustomQuery)
        {
            BusinessRules.CustomProcedure(CustomQuery);
        }

        public SelectQueryResult DynamicGet(SelectQuery query)
        {
            return BusinessRules.DynamicGet(query);
        }

        public dynamic DynamicGetSingleRow(SelectQuery query)
        {
            return BusinessRules.DynamicGetSingleRow(query);
        }

        public bool DynamicEdit(UpdateQuery query)
        {
            return BusinessRules.DynamicEdit(query);
        }

        public bool DynamicRemove(DeleteQuery query)
        {
            return BusinessRules.DynamicRemove(query);
        }

        #endregion
    }
}
