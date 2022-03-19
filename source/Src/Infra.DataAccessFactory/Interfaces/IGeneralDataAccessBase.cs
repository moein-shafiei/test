using DotFramework.DynamicQuery;
using DotFramework.Infra.Model;

namespace DotFramework.Infra.DataAccessFactory
{
    public interface IGeneralDataAccessBase : IDataAccessBase
    {
        SelectQueryResult CustomQuery(CustomQuery CustomQuery);

        dynamic CustomQuerySingleRow(CustomQuery CustomQuery);

        void CustomProcedure(CustomQuery CustomQuery);

        SelectQueryResult DynamicSelect(SelectQuery query);

        dynamic DynamicSelectSingleRow(SelectQuery query);

        bool DynamicUpdate(UpdateQuery query);

        bool DynamicDelete(DeleteQuery query);
    }
}
