using DotFramework.DynamicQuery;
using DotFramework.Infra.Model;
using DotFramework.Infra.ServiceFactory;
using System;

namespace DotFramework.Infra.Web.API.Invoker
{
    public abstract class GeneralInvokerBase : InvokerBase, IGeneralServiceBase
    {
        public GeneralInvokerBase(string endpointAddress, string routePrefix) 
            : base(endpointAddress, routePrefix)
        {
        }

        #region Public Methods

        public SelectQueryResult CustomQuery(CustomQuery CustomQuery)
        {
            throw new NotImplementedException();
        }

        public dynamic CustomQuerySingleRow(CustomQuery CustomQuery)
        {
            throw new NotImplementedException();
        }

        public void CustomProcedure(CustomQuery CustomQuery)
        {
            throw new NotImplementedException();
        }

        public SelectQueryResult DynamicGet(SelectQuery query)
        {
            throw new NotImplementedException();
        }

        public dynamic DynamicGetSingleRow(SelectQuery query)
        {
            throw new NotImplementedException();
        }

        public bool DynamicEdit(UpdateQuery query)
        {
            throw new NotImplementedException();
        }

        public bool DynamicRemove(DeleteQuery query)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
