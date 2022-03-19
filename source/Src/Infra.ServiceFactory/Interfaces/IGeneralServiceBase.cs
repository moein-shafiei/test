using DotFramework.DynamicQuery;
using DotFramework.Infra.Model;

namespace DotFramework.Infra.ServiceFactory
{
    public interface IGeneralServiceBase : IServiceBase
    {
        [OperationType("GetCollection")]
        SelectQueryResult CustomQuery(CustomQuery CustomQuery);

        [OperationType("Get")]
        dynamic CustomQuerySingleRow(CustomQuery CustomQuery);

        [OperationType("Procedure")]
        void CustomProcedure(CustomQuery CustomQuery);

        [OperationType("GetCollection")]
        SelectQueryResult DynamicGet(SelectQuery query);

        [OperationType("Get")]
        dynamic DynamicGetSingleRow(SelectQuery query);

        [OperationType("Edit")]
        bool DynamicEdit(UpdateQuery query);

        [OperationType("Remove")]
        bool DynamicRemove(DeleteQuery query);
    }
}
