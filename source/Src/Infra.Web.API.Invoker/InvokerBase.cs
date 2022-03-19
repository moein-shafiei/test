using DotFramework.DynamicQuery;
using DotFramework.Infra.Model;
using DotFramework.Infra.Security;
using DotFramework.Infra.ServiceFactory;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DotFramework.Core.Web;

namespace DotFramework.Infra.Web.API.Invoker
{
    public abstract class InvokerBase : IServiceBase
    {
        protected readonly AbstractHttpUtility _HttpUtility;

        public InvokerBase(string endpointAddress, string routePrefix)
        {
#if NET40
            _HttpUtility = new WebRequestUtility(String.Format("{0}/{1}/", endpointAddress, routePrefix));
#else
            _HttpUtility = new HttpClientUtility(String.Format("{0}/{1}/", endpointAddress, routePrefix));
#endif
        }

        public virtual void Dispose()
        {

        }
    }

    public abstract class InvokerBase<TKey, TModel, TModelCollection> : InvokerBase, IServiceBase<TKey, TModel, TModelCollection>
        where TModel : DomainModelBase, new()
        where TModelCollection : ListBase<TKey, TModel>, new()
    {
        public InvokerBase(string endpointAddress, string routePrefix)
            : base(endpointAddress, routePrefix)
        {

        }

        public virtual TModel GetSimple(TKey key)
        {
            throw new NotImplementedException();
        }

        public Task<TModel> GetSimpleAsync(TKey key)
        {
            throw new NotImplementedException();
        }

        public virtual TModel Get(TKey key)
        {
            throw new NotImplementedException();
        }

        public Task<TModel> GetAsync(TKey key)
        {
            throw new NotImplementedException();
        }

        public virtual TModel GetMaster(TKey key)
        {
            throw new NotImplementedException();
        }

        public Task<TModel> GetMasterAsync(TKey key)
        {
            throw new NotImplementedException();
        }

        public virtual TModelCollection GetAllSimple()
        {
            throw new NotImplementedException();
        }

        public Task<TModelCollection> GetAllSimpleAsync()
        {
            throw new NotImplementedException();
        }

        public virtual TModelCollection GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TModelCollection> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Int32 GetCount()
        {
            throw new NotImplementedException();
        }

        public Task<Int32> GetCountAsync()
        {
            throw new NotImplementedException();
        }

        public virtual TModelCollection GetAllWithPagingSimple(int PageIndex, int RowsInPage)
        {
            throw new NotImplementedException();
        }

        public Task<TModelCollection> GetAllWithPagingSimpleAsync(int PageIndex, int RowsInPage)
        {
            throw new NotImplementedException();
        }

        public virtual TModelCollection GetAllWithPaging(int PageIndex, int RowsInPage)
        {
            throw new NotImplementedException();
        }

        public Task<TModelCollection> GetAllWithPagingAsync(int PageIndex, int RowsInPage)
        {
            throw new NotImplementedException();
        }

        public virtual OperationResult<TModel> Add(TModel model)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<TModel>> AddAsync(TModel model)
        {
            throw new NotImplementedException();
        }

        public virtual OperationResult<TModel> Edit(TModel model)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<TModel>> EditAsync(TModel model)
        {
            throw new NotImplementedException();
        }

        public virtual OperationResult Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> RemoveAsync(TKey key)
        {
            throw new NotImplementedException();
        }

        public virtual OperationResult<TModel> Save(TModel model)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<TModel>> SaveAsync(TModel model)
        {
            throw new NotImplementedException();
        }

        public virtual OperationResult<TModelCollection> SaveCollection(TModelCollection modelCollection)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<TModelCollection>> SaveCollectionAsync(TModelCollection modelCollection)
        {
            throw new NotImplementedException();
        }

        public virtual OperationResult<TModel> AddWithDetail(TModel model)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<TModel>> AddWithDetailAsync(TModel model)
        {
            throw new NotImplementedException();
        }

        public virtual OperationResult<TModel> EditWithDetail(TModel model)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<TModel>> EditWithDetailAsync(TModel model)
        {
            throw new NotImplementedException();
        }

        public virtual OperationResult<TModel> RemoveWithDetail(TModel model)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult<TModel>> RemoveWithDetailAsync(TModel model)
        {
            throw new NotImplementedException();
        }

        public virtual TModelCollection CustomQuerySimple(CustomQuery CustomQuery)
        {
            throw new NotImplementedException();
        }

        public Task<TModelCollection> CustomQuerySimpleAsync(CustomQuery CustomQuery)
        {
            throw new NotImplementedException();
        }

        public virtual TModelCollection CustomQuery(CustomQuery CustomQuery)
        {
            throw new NotImplementedException();
        }

        public Task<TModelCollection> CustomQueryAsync(CustomQuery CustomQuery)
        {
            throw new NotImplementedException();
        }

        public virtual TModel CustomQuerySingleRow(CustomQuery CustomQuery)
        {
            throw new NotImplementedException();
        }

        public Task<TModel> CustomQuerySingleRowAsync(CustomQuery CustomQuery)
        {
            throw new NotImplementedException();
        }

        public virtual TModel CustomQuerySingleRowSimple(CustomQuery CustomQuery)
        {
            throw new NotImplementedException();
        }

        public Task<TModel> CustomQuerySingleRowSimpleAsync(CustomQuery CustomQuery)
        {
            throw new NotImplementedException();
        }

        public virtual TModelCollection GetByFilter(Expression<Func<TModel, Boolean>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<TModelCollection> GetByFilterAsync(Expression<Func<TModel, Boolean>> expression)
        {
            throw new NotImplementedException();
        }

        public virtual TModelCollection GetByFilterSimple(Expression<Func<TModel, Boolean>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<TModelCollection> GetByFilterSimpleAsync(Expression<Func<TModel, Boolean>> expression)
        {
            throw new NotImplementedException();
        }

        public virtual TModelCollection DynamicGet(SelectQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<TModelCollection> DynamicGetAsync(SelectQuery query)
        {
            throw new NotImplementedException();
        }

        public virtual TModelCollection DynamicGetSimple(SelectQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<TModelCollection> DynamicGetSimpleAsync(SelectQuery query)
        {
            throw new NotImplementedException();
        }

        public virtual TModel GetByFilterSingleRow(Expression<Func<TModel, Boolean>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<TModel> GetByFilterSingleRowAsync(Expression<Func<TModel, Boolean>> expression)
        {
            throw new NotImplementedException();
        }

        public virtual TModel GetByFilterSingleRowSimple(Expression<Func<TModel, Boolean>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<TModel> GetByFilterSingleRowSimpleAsync(Expression<Func<TModel, Boolean>> expression)
        {
            throw new NotImplementedException();
        }

        public virtual TModel DynamicGetSingleRow(SelectQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<TModel> DynamicGetSingleRowAsync(SelectQuery query)
        {
            throw new NotImplementedException();
        }

        public virtual TModel DynamicGetSingleRowSimple(SelectQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<TModel> DynamicGetSingleRowSimpleAsync(SelectQuery query)
        {
            throw new NotImplementedException();
        }

        public virtual Int32 GetCountByFilter(Expression<Func<TModel, Boolean>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<Int32> GetCountByFilterAsync(Expression<Func<TModel, Boolean>> expression)
        {
            throw new NotImplementedException();
        }

        public virtual Object DynamicGetScalar(SelectQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<Object> DynamicGetScalarAsync(SelectQuery query)
        {
            throw new NotImplementedException();
        }

        public virtual TResult DynamicGetScalar<TResult>(SelectQuery query)
        {
            throw new NotImplementedException();
        }

        public Task<TResult> DynamicGetScalarAsync<TResult>(SelectQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
