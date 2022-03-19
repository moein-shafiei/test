using DotFramework.DynamicQuery;
using DotFramework.Infra.ExceptionHandling;
using DotFramework.Infra.Model;
using System;
using System.Linq.Expressions;
using System.ServiceModel;
using System.Threading.Tasks;

namespace DotFramework.Infra.ServiceFactory
{
    public interface IServiceBase<TKey, TModel, TModelCollection> : IServiceBase
    {
        [OperationType("Get")]
        TModel GetSimple(TKey key);

        [OperationType("Get")]
        Task<TModel> GetSimpleAsync(TKey key);

        [OperationType("Get")]
        TModel Get(TKey key);

        [OperationType("Get")]
        Task<TModel> GetAsync(TKey key);

        [OperationType("Get")]
        TModel GetMaster(TKey key);

        [OperationType("Get")]
        Task<TModel> GetMasterAsync(TKey key);

        [OperationType("GetCollection")]
        TModelCollection GetAllSimple();

        [OperationType("GetCollection")]
        Task<TModelCollection> GetAllSimpleAsync();

        [OperationType("GetCollection")]
        TModelCollection GetAll();

        [OperationType("GetCollection")]
        Task<TModelCollection> GetAllAsync();

        [OperationType("GetCount")]
        Int32 GetCount();

        [OperationType("GetCount")]
        Task<Int32> GetCountAsync();

        [OperationType("Add")]
        OperationResult<TModel> Add(TModel model);

        [OperationType("Add")]
        Task<OperationResult<TModel>> AddAsync(TModel model);

        [OperationType("Edit")]
        OperationResult<TModel> Edit(TModel model);

        [OperationType("Edit")]
        Task<OperationResult<TModel>> EditAsync(TModel model);

        [OperationType("Remove")]
        OperationResult Remove(TKey key);

        [OperationType("Remove")]
        Task<OperationResult> RemoveAsync(TKey key);

        [OperationType("Save")]
        OperationResult<TModel> Save(TModel model);

        [OperationType("Save")]
        Task<OperationResult<TModel>> SaveAsync(TModel model);

        [OperationType("Save")]
        OperationResult<TModelCollection> SaveCollection(TModelCollection modelCollection);

        [OperationType("Save")]
        Task<OperationResult<TModelCollection>> SaveCollectionAsync(TModelCollection modelCollection);

        [OperationType("Add")]
        OperationResult<TModel> AddWithDetail(TModel model);

        [OperationType("Add")]
        Task<OperationResult<TModel>> AddWithDetailAsync(TModel model);

        [OperationType("Edit")]
        OperationResult<TModel> EditWithDetail(TModel model);

        [OperationType("Edit")]
        Task<OperationResult<TModel>> EditWithDetailAsync(TModel model);

        [OperationType("Remove")]
        OperationResult<TModel> RemoveWithDetail(TModel model);

        [OperationType("Remove")]
        Task<OperationResult<TModel>> RemoveWithDetailAsync(TModel model);

        [OperationType("GetCollection")]
        TModelCollection CustomQuery(CustomQuery CustomQuery);

        [OperationType("GetCollection")]
        Task<TModelCollection> CustomQueryAsync(CustomQuery CustomQuery);

        [OperationType("GetCollection")]
        TModelCollection CustomQuerySimple(CustomQuery CustomQuery);

        [OperationType("GetCollection")]
        Task<TModelCollection> CustomQuerySimpleAsync(CustomQuery CustomQuery);

        [OperationType("Get")]
        TModel CustomQuerySingleRow(CustomQuery CustomQuery);

        [OperationType("Get")]
        Task<TModel> CustomQuerySingleRowAsync(CustomQuery CustomQuery);

        [OperationType("Get")]
        TModel CustomQuerySingleRowSimple(CustomQuery CustomQuery);

        [OperationType("Get")]
        Task<TModel> CustomQuerySingleRowSimpleAsync(CustomQuery CustomQuery);

        [OperationType("GetCollection")]
        TModelCollection GetByFilter(Expression<Func<TModel, Boolean>> expression);

        [OperationType("GetCollection")]
        Task<TModelCollection> GetByFilterAsync(Expression<Func<TModel, Boolean>> expression);

        [OperationType("GetCollection")]
        TModelCollection GetByFilterSimple(Expression<Func<TModel, Boolean>> expression);

        [OperationType("GetCollection")]
        Task<TModelCollection> GetByFilterSimpleAsync(Expression<Func<TModel, Boolean>> expression);

        [OperationType("GetCollection")]
        TModelCollection DynamicGet(SelectQuery query);

        [OperationType("GetCollection")]
        Task<TModelCollection> DynamicGetAsync(SelectQuery query);

        [OperationType("GetCollection")]
        TModelCollection DynamicGetSimple(SelectQuery query);

        [OperationType("GetCollection")]
        Task<TModelCollection> DynamicGetSimpleAsync(SelectQuery query);

        [OperationType("Get")]
        TModel GetByFilterSingleRow(Expression<Func<TModel, Boolean>> expression);

        [OperationType("Get")]
        Task<TModel> GetByFilterSingleRowAsync(Expression<Func<TModel, Boolean>> expression);

        [OperationType("Get")]
        TModel GetByFilterSingleRowSimple(Expression<Func<TModel, Boolean>> expression);

        [OperationType("Get")]
        Task<TModel> GetByFilterSingleRowSimpleAsync(Expression<Func<TModel, Boolean>> expression);

        [OperationType("Get")]
        TModel DynamicGetSingleRow(SelectQuery query);

        [OperationType("Get")]
        Task<TModel> DynamicGetSingleRowAsync(SelectQuery query);

        [OperationType("Get")]
        TModel DynamicGetSingleRowSimple(SelectQuery query);

        [OperationType("Get")]
        Task<TModel> DynamicGetSingleRowSimpleAsync(SelectQuery query);

        [OperationType("GetCount")]
        Int32 GetCountByFilter(Expression<Func<TModel, Boolean>> expression);

        [OperationType("GetCount")]
        Task<Int32> GetCountByFilterAsync(Expression<Func<TModel, Boolean>> expression);

        [OperationType("GetScalar")]
        Object DynamicGetScalar(SelectQuery query);

        [OperationType("GetScalar")]
        Task<Object> DynamicGetScalarAsync(SelectQuery query);

        [OperationType("GetScalar")]
        TResult DynamicGetScalar<TResult>(SelectQuery query);

        [OperationType("GetScalar")]
        Task<TResult> DynamicGetScalarAsync<TResult>(SelectQuery query);
    }

    public interface IServiceBase : IDisposable
    {

    }
}
