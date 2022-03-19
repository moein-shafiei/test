using DotFramework.Infra.BusinessRules;
using DotFramework.Infra.DataAccessFactory;
using DotFramework.Infra.Model;
using DotFramework.Infra.ServiceFactory;
using DotFramework.DynamicQuery;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DotFramework.Infra.BusinessFacade
{
    public abstract class ServiceBase : IServiceBase
    {
        public virtual void Dispose()
        {

        }
    }

    public abstract class ServiceBase<TKey, TModel, TModelCollection, TBusinessRules, TDataAccess> : ServiceBase, IServiceBase<TKey, TModel, TModelCollection>
        where TModel : DomainModelBase, new()
        where TModelCollection : ListBase<TKey, TModel>, new()
        where TDataAccess : IDataAccessBase<TKey, TModel, TModelCollection>
        where TBusinessRules : RulesBase<TKey, TModel, TModelCollection, TDataAccess>, new()
    {
        public TBusinessRules BusinessRules
        {
            get
            {
                return BusinessRulesFactory.Instance.GetBusinessRules<TBusinessRules>();
            }
        }

        public virtual TModel GetSimple(TKey key)
        {
            return BusinessRules.GetSimple(key);
        }

        public Task<TModel> GetSimpleAsync(TKey key)
        {
           return Task.Run(() =>
           {
               return GetSimple(key);
           });
        }

        public virtual TModel Get(TKey key)
        {
            return BusinessRules.Get(key);
        }

        public Task<TModel> GetAsync(TKey key)
        {
            return Task.Run(() =>
            {
                return Get(key);
            });
        }

        public virtual TModel GetMaster(TKey key)
        {
            return BusinessRules.GetMaster(key);
        }

        public Task<TModel> GetMasterAsync(TKey key)
        {
            return Task.Run(() =>
            {
                return GetMaster(key);
            });
        }

        public virtual TModelCollection GetAllSimple()
        {
            return BusinessRules.GetAllSimple();
        }

        public Task<TModelCollection> GetAllSimpleAsync()
        {
            return Task.Run(() =>
            {
                return GetAllSimple();
            });
        }

        public virtual TModelCollection GetAll()
        {
            return BusinessRules.GetAll();
        }

        public Task<TModelCollection> GetAllAsync()
        {
            return Task.Run(() =>
            {
                return GetAll();
            });
        }

        public virtual Int32 GetCount()
        {
            return BusinessRules.GetCount();
        }

        public Task<Int32> GetCountAsync()
        {
            return Task.Run(() =>
            {
                return GetCount();
            });
        }

        public virtual TModelCollection GetAllWithPagingSimple(int PageIndex, int RowsInPage)
        {
            return BusinessRules.GetAllWithPagingSimple(PageIndex, RowsInPage);
        }

        public Task<TModelCollection> GetAllWithPagingSimpleAsync(int PageIndex, int RowsInPage)
        {
            return Task.Run(() =>
            {
                return GetAllWithPagingSimple(PageIndex, RowsInPage);
            });
        }

        public virtual TModelCollection GetAllWithPaging(int PageIndex, int RowsInPage)
        {
            return BusinessRules.GetAllWithPaging(PageIndex, RowsInPage);
        }

        public Task<TModelCollection> GetAllWithPagingAsync(int PageIndex, int RowsInPage)
        {
            return Task.Run(() =>
            {
                return GetAllWithPaging(PageIndex, RowsInPage);
            });
        }

        public virtual OperationResult<TModel> Add(TModel model)
        {
            return BusinessRules.Add(model);
        }

        public Task<OperationResult<TModel>> AddAsync(TModel model)
        {
            return Task.Run(() =>
            {
                return Add(model);
            });
        }

        public virtual OperationResult<TModel> Edit(TModel model)
        {
            return BusinessRules.Edit(model);
        }

        public Task<OperationResult<TModel>> EditAsync(TModel model)
        {
            return Task.Run(() =>
            {
                return Edit(model);
            });
        }

        public virtual OperationResult Remove(TKey key)
        {
            return BusinessRules.Remove(key);
        }

        public Task<OperationResult> RemoveAsync(TKey key)
        {
            return Task.Run(() =>
            {
                return Remove(key);
            });
        }

        public virtual OperationResult<TModel> Save(TModel model)
        {
            return BusinessRules.Save(model);
        }

        public Task<OperationResult<TModel>> SaveAsync(TModel model)
        {
            return Task.Run(() =>
            {
                return Save(model);
            });
        }

        public virtual OperationResult<TModelCollection> SaveCollection(TModelCollection modelCollection)
        {
            return BusinessRules.SaveCollection(modelCollection);
        }

        public Task<OperationResult<TModelCollection>> SaveCollectionAsync(TModelCollection modelCollection)
        {
            return Task.Run<OperationResult<TModelCollection>>(() =>
            {
                return SaveCollection(modelCollection);
            });
        }

        public virtual OperationResult<TModel> AddWithDetail(TModel model)
        {
            return BusinessRules.AddWithDetail(model);
        }

        public Task<OperationResult<TModel>> AddWithDetailAsync(TModel model)
        {
            return Task.Run(() =>
            {
                return AddWithDetail(model);
            });
        }

        public virtual OperationResult<TModel> EditWithDetail(TModel model)
        {
            return BusinessRules.EditWithDetail(model);
        }

        public Task<OperationResult<TModel>> EditWithDetailAsync(TModel model)
        {
            return Task.Run(() =>
            {
                return EditWithDetail(model);
            });
        }

        public virtual OperationResult<TModel> RemoveWithDetail(TModel model)
        {
            return BusinessRules.RemoveWithDetail(model);
        }

        public Task<OperationResult<TModel>> RemoveWithDetailAsync(TModel model)
        {
            return Task.Run(() =>
            {
                return RemoveWithDetail(model);
            });
        }

        public virtual TModelCollection CustomQuerySimple(CustomQuery CustomQuery)
        {
            return BusinessRules.CustomQuerySimple(CustomQuery);
        }

        public Task<TModelCollection> CustomQuerySimpleAsync(CustomQuery CustomQuery)
        {
            return Task.Run(() =>
            {
                return CustomQuerySimple(CustomQuery);
            });
        }

        public virtual TModelCollection CustomQuery(CustomQuery CustomQuery)
        {
            return BusinessRules.CustomQuery(CustomQuery);
        }

        public Task<TModelCollection> CustomQueryAsync(CustomQuery CustomQuery)
        {
            return Task.Run(() =>
            {
                return this.CustomQuery(CustomQuery);
            });
        }

        public virtual TModel CustomQuerySingleRow(CustomQuery CustomQuery)
        {
            return BusinessRules.CustomQuerySingleRow(CustomQuery);
        }

        public Task<TModel> CustomQuerySingleRowAsync(CustomQuery CustomQuery)
        {
            return Task.Run(() =>
            {
                return CustomQuerySingleRow(CustomQuery);
            });
        }

        public virtual TModel CustomQuerySingleRowSimple(CustomQuery CustomQuery)
        {
            return BusinessRules.CustomQuerySingleRowSimple(CustomQuery);
        }

        public Task<TModel> CustomQuerySingleRowSimpleAsync(CustomQuery CustomQuery)
        {
            return Task.Run(() =>
            {
                return CustomQuerySingleRowSimple(CustomQuery);
            });
        }

        public virtual TModelCollection GetByFilter(Expression<Func<TModel, Boolean>> expression)
        {
            return BusinessRules.GetByFilter(expression);
        }

        public Task<TModelCollection> GetByFilterAsync(Expression<Func<TModel, Boolean>> expression)
        {
            return Task.Run(() =>
            {
                return GetByFilter(expression);
            });
        }

        public virtual TModelCollection GetByFilterSimple(Expression<Func<TModel, Boolean>> expression)
        {
            return BusinessRules.GetByFilterSimple(expression);
        }

        public Task<TModelCollection> GetByFilterSimpleAsync(Expression<Func<TModel, Boolean>> expression)
        {
            return Task.Run(() =>
            {
                return GetByFilterSimple(expression);
            });
        }

        public virtual TModelCollection DynamicGet(SelectQuery query)
        {
            return BusinessRules.DynamicGet(query);
        }

        public Task<TModelCollection> DynamicGetAsync(SelectQuery query)
        {
            return Task.Run(() =>
            {
                return DynamicGet(query);
            });
        }

        public virtual TModelCollection DynamicGetSimple(SelectQuery query)
        {
            return BusinessRules.DynamicGetSimple(query);
        }

        public Task<TModelCollection> DynamicGetSimpleAsync(SelectQuery query)
        {
            return Task.Run(() =>
            {
                return DynamicGetSimple(query);
            });
        }

        public virtual TModel GetByFilterSingleRow(Expression<Func<TModel, Boolean>> expression)
        {
            return BusinessRules.GetByFilterSingleRow(expression);
        }

        public Task<TModel> GetByFilterSingleRowAsync(Expression<Func<TModel, Boolean>> expression)
        {
            return Task.Run(() =>
            {
                return GetByFilterSingleRow(expression);
            });
        }

        public virtual TModel GetByFilterSingleRowSimple(Expression<Func<TModel, Boolean>> expression)
        {
            return BusinessRules.GetByFilterSingleRowSimple(expression);
        }

        public Task<TModel> GetByFilterSingleRowSimpleAsync(Expression<Func<TModel, Boolean>> expression)
        {
            return Task.Run(() =>
            {
                return GetByFilterSingleRowSimple(expression);
            });
        }

        public virtual TModel DynamicGetSingleRow(SelectQuery query)
        {
            return BusinessRules.DynamicGetSingleRow(query);
        }

        public Task<TModel> DynamicGetSingleRowAsync(SelectQuery query)
        {
            return Task.Run(() =>
            {
                return DynamicGetSingleRow(query);
            });
        }

        public virtual TModel DynamicGetSingleRowSimple(SelectQuery query)
        {
            return BusinessRules.DynamicGetSingleRowSimple(query);
        }

        public Task<TModel> DynamicGetSingleRowSimpleAsync(SelectQuery query)
        {
            return Task.Run(() =>
            {
                return DynamicGetSingleRowSimple(query);
            });
        }

        public virtual Int32 GetCountByFilter(Expression<Func<TModel, Boolean>> expression)
        {
            return BusinessRules.GetCountByFilter(expression);
        }

        public Task<Int32> GetCountByFilterAsync(Expression<Func<TModel, Boolean>> expression)
        {
            return Task.Run(() =>
            {
                return GetCountByFilter(expression);
            });
        }

        public virtual Object DynamicGetScalar(SelectQuery query)
        {
            return BusinessRules.DynamicGetScalar(query);
        }

        public Task<Object> DynamicGetScalarAsync(SelectQuery query)
        {
            return Task.Run(() =>
            {
                return DynamicGetScalarAsync(query);
            });
        }

        public virtual TResult DynamicGetScalar<TResult>(SelectQuery query)
        {
            return BusinessRules.DynamicGetScalar<TResult>(query);
        }

        public Task<TResult> DynamicGetScalarAsync<TResult>(SelectQuery query)
        {
            return Task.Run(() =>
            {
                return DynamicGetScalarAsync<TResult>(query);
            });
        }
    }
}
