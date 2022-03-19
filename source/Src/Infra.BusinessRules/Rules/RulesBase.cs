using System;
using System.Transactions;
using System.Linq;
using System.Collections.Generic;
using DotFramework.DynamicQuery;
using DotFramework.Infra.Model;
using System.Linq.Expressions;
using DotFramework.Infra.DataAccessFactory;

namespace DotFramework.Infra.BusinessRules
{
    public abstract class RulesBase
    {
        protected readonly object padlock = new object();
    }

    public abstract class RulesBase<TKey, TModel, TModelCollection, TDataAccess> : RulesBase
        where TModel : DomainModelBase, new()
        where TModelCollection : ListBase<TKey, TModel>, new()
        where TDataAccess : IDataAccessBase<TKey, TModel, TModelCollection>
    {
        #region Constructors

        public RulesBase()
        {
            ResetPopulateActions();
        }

        #endregion

        #region Abstract Properties

        protected abstract TDataAccess DataAccess { get; }

        protected abstract List<Action<TModel>> MasterPopulateActions { get; }

        protected abstract List<Action<TModel>> DetailPopulateActions { get; }

        #region Cache

        protected abstract TModelCollection Cache { get; }

        protected abstract bool AllowCache { get; }

        #endregion

        #endregion

        #region Properties

        public virtual Func<TModel, dynamic> OrderByKey
        {
            get
            {
                return null;
            }
        }

        public virtual Func<TModel, dynamic> OrderByDescendingKey
        {
            get
            {
                return null;
            }
        }

        #endregion

        #region Protected Methods

        #region Events

        protected virtual void OnBeforeGet()
        {

        }

        protected virtual void OnBeforeGet(TKey key)
        {
            OnBeforeGet();
        }

        protected virtual void OnAfterGet(ref TModel model)
        {
            FillComplex(model, true);
        }

        protected virtual void OnBeforeGetMaster(TKey key)
        {

        }

        protected virtual void OnAfterGetMaster(ref TModel model)
        {
            FillComplex(model, false);
        }

        protected virtual void OnBeforeGetCollection()
        {

        }

        protected virtual void OnAfterGetCollection(ref TModelCollection modelCollection)
        {
            FillComplex(modelCollection);

            if (OrderByKey != null)
            {
                TModelCollection tempCollection = new TModelCollection();
                tempCollection.Append(modelCollection.OrderBy(OrderByKey));
                modelCollection = tempCollection;
            }

            if (OrderByDescendingKey != null)
            {
                TModelCollection tempCollection = new TModelCollection();
                tempCollection.Append(modelCollection.OrderByDescending(OrderByDescendingKey));
                modelCollection = tempCollection;
            }
        }

        protected virtual void OnBeforeAdd(ref TModel model)
        {

        }

        protected virtual void OnAfterAdd(ref TModel model)
        {

        }

        protected virtual void OnBeforeEdit(ref TModel model)
        {

        }

        protected virtual void OnAfterEdit(ref TModel model)
        {

        }

        protected virtual void OnBeforeRemove(TKey key)
        {

        }

        protected virtual void OnAfterRemove(TKey key)
        {

        }

        #endregion

        protected virtual void ResetPopulateActions()
        {

        }

        protected virtual void FillComplex(TModel model, bool getDetail = false)
        {
            foreach (Action<TModel> masterPopulatingAction in MasterPopulateActions)
            {
                masterPopulatingAction(model);
            }

            if (getDetail)
            {
                foreach (Action<TModel> detailPopulatingAction in DetailPopulateActions)
                {
                    detailPopulatingAction(model);
                }
            }
        }

        protected virtual void FillComplex(TModelCollection modelCollection, bool getDetail = false)
        {
            foreach (TModel model in modelCollection)
            {
                FillComplex(model, getDetail);
            }
        }

        #endregion

        #region Public Methods

        public virtual TModel GetSimple(TKey key)
        {
            TModel Model = null;

            if (AllowCache && Cache.Contain(key))
            {
                Model = Cache.Get(key).Clone<TModel>(true);
            }
            else
            {
                Model = DataAccess.Select(key);

                if (AllowCache)
                {
                    Cache.Add(Model.Clone<TModel>(true));
                }
            }

            return Model;
        }

        public virtual TModel Get(TKey key)
        {
            OnBeforeGet(key);
            TModel model = GetSimple(key);
            OnAfterGet(ref model);

            return model;
        }

        public virtual TModel GetMaster(TKey key)
        {
            OnBeforeGetMaster(key);
            TModel model = GetSimple(key);
            OnAfterGetMaster(ref model);

            return model;
        }

        public virtual TModelCollection GetAllSimple()
        {
            TModelCollection TModelCollection = null;

            if (AllowCache && Cache.IsChached)
            {
                TModelCollection = Cache.Clone<TModelCollection>(true);
            }
            else
            {
                TModelCollection = DataAccess.SelectAll();

                if (AllowCache)
                {
                    Cache.Append(TModelCollection.Clone(true));
                    Cache.IsChached = true;
                }
            }

            return TModelCollection;
        }

        public virtual TModelCollection GetAll()
        {
            OnBeforeGetCollection();
            TModelCollection modelCollection = GetAllSimple();
            OnAfterGetCollection(ref modelCollection);

            return modelCollection;
        }

        public virtual Int32 GetCount()
        {
            return DataAccess.SelectCount();
        }

        public virtual TModelCollection GetAllWithPagingSimple(int PageIndex, int RowsInPage)
        {
            TModelCollection TModelCollection = new TModelCollection();

            if (AllowCache && Cache.IsChached)
            {
                TModelCollection.TotalCount = Cache.Count;
                int itemCount = (PageIndex * RowsInPage) + RowsInPage > Cache.Count ? (Cache.Count - (PageIndex * RowsInPage)) : RowsInPage;
                TModelCollection.Append(Cache.GetRange((PageIndex * RowsInPage), itemCount));
            }
            else
            {
                TModelCollection = DataAccess.SelectAllWithPaging(PageIndex, RowsInPage);

                if (AllowCache)
                {
                    Cache.Append(TModelCollection.Clone(true));
                }
            }

            return TModelCollection;
        }

        public virtual TModelCollection GetAllWithPaging(int PageIndex, int RowsInPage)
        {
            OnBeforeGetCollection();
            TModelCollection modelCollection = GetAllWithPagingSimple(PageIndex, RowsInPage);
            OnAfterGetCollection(ref modelCollection);

            return modelCollection;
        }

        public virtual OperationResult<TModel> Add(TModel model)
        {
            OnBeforeAdd(ref model);
            model.State = ObjectState.None;

            bool isAdd = DataAccess.Insert(model);

            if (isAdd)
            {
                if (AllowCache)
                {
                    Cache.Add(model.Clone<TModel>(true));
                }
            }

            OnAfterAdd(ref model);
            return new OperationResult<TModel>(isAdd, model);
        }

        public virtual OperationResult<TModel> Edit(TModel model)
        {
            OnBeforeEdit(ref model);
            model.State = ObjectState.None;

            bool isEdit = DataAccess.Update(model);

            if (isEdit)
            {
                if (AllowCache)
                {
                    this.Cache.Set(model.Key, model.Clone<TModel>(true));
                }
            }

            OnAfterEdit(ref model);
            return new OperationResult<TModel>(isEdit, model);
        }

        public virtual OperationResult Remove(TKey key)
        {
            OnBeforeRemove(key);
            bool isRemove = DataAccess.Delete(key);

            if (isRemove && AllowCache)
            {
                Cache.Remove(key);
            }

            OnAfterRemove(key);
            return new OperationResult(isRemove);
        }

        public virtual OperationResult<TModel> Save(TModel model)
        {
            switch (model.State)
            {
                case ObjectState.Added:
                    AddWithDetail(model);
                    break;
                case ObjectState.Edited:
                    EditWithDetail(model);
                    break;
                case ObjectState.Removed:
                    RemoveWithDetail(model);
                    break;
            }

            return new OperationResult<TModel>(true, model);
        }

        public virtual OperationResult<TModelCollection> SaveCollection(TModelCollection modelCollection)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (TModel model in modelCollection.Where(obj => obj.State == ObjectState.Added || obj.State == ObjectState.Edited || obj.State == ObjectState.Removed))
                    {
                        OperationResult<TModel> result = Save(model);
                    }

                    scope.Complete();
                }

                return new OperationResult<TModelCollection>(true, modelCollection);
            }
            catch (Exception ex)
            {
                if (BusinessRulesExceptionHandler.Instance.HandleException(ref ex))
                {
                    throw ex;
                }

                return null;
            }
        }

        public virtual OperationResult<TModel> AddWithDetail(TModel model)
        {
            throw new NotImplementedException();
        }

        public virtual OperationResult<TModel> EditWithDetail(TModel model)
        {
            throw new NotImplementedException();
        }

        public virtual OperationResult<TModel> RemoveWithDetail(TModel model)
        {
            throw new NotImplementedException();
        }

        public virtual TModelCollection CustomQuery(CustomQuery CustomQuery)
        {
            OnBeforeGetCollection();
            TModelCollection modelCollection = CustomQuerySimple(CustomQuery);
            OnAfterGetCollection(ref modelCollection);

            return modelCollection;
        }

        public virtual TModelCollection CustomQuerySimple(CustomQuery CustomQuery)
        {
            TModelCollection TModelCollection = DataAccess.CustomQuery(CustomQuery);

            if (AllowCache)
            {
                Cache.Append(TModelCollection.Clone(true));
            }

            return TModelCollection;
        }

        public virtual TModel CustomQuerySingleRow(CustomQuery CustomQuery)
        {
            OnBeforeGet();
            TModel model = CustomQuerySingleRowSimple(CustomQuery);
            OnAfterGet(ref model);

            return model;
        }

        public virtual TModel CustomQuerySingleRowSimple(CustomQuery CustomQuery)
        {
            TModel model = DataAccess.CustomQuerySingleRow(CustomQuery);

            if (AllowCache)
            {
                Cache.Add(model.Clone<TModel>(true));
            }

            return model;
        }

        public virtual TModelCollection GetByFilter(Expression<Func<TModel, Boolean>> expression)
        {
            OnBeforeGetCollection();
            TModelCollection modelCollection = GetByFilterSimple(expression);
            OnAfterGetCollection(ref modelCollection);

            return modelCollection;
        }

        public virtual TModelCollection GetByFilterSimple(Expression<Func<TModel, Boolean>> expression)
        {
            //To Be Enhanced
            var query = SelectQueryBuilder.Initialize().From<TModel>().Where(expression).Query;

            TModelCollection TModelCollection = DynamicGetSimple(query);

            if (AllowCache)
            {
                Cache.Append(TModelCollection.Clone(true));
            }

            return TModelCollection;
        }

        public virtual TModelCollection DynamicGet(SelectQuery query)
        {
            OnBeforeGetCollection();
            TModelCollection modelCollection = DynamicGetSimple(query);
            OnAfterGetCollection(ref modelCollection);

            return modelCollection;
        }

        public virtual TModelCollection DynamicGetSimple(SelectQuery query)
        {
            TModelCollection TModelCollection = DataAccess.DynamicSelect(query);

            if (AllowCache)
            {
                Cache.Append(TModelCollection.Clone(true));
            }

            return TModelCollection;
        }

        public virtual TModel GetByFilterSingleRow(Expression<Func<TModel, Boolean>> expression)
        {
            OnBeforeGet();
            TModel model = GetByFilterSingleRowSimple(expression);
            OnAfterGet(ref model);

            return model;
        }

        public virtual TModel GetByFilterSingleRowSimple(Expression<Func<TModel, Boolean>> expression)
        {
            //To Be Enhanced
            var query = SelectQueryBuilder.Initialize().From<TModel>().Where(expression).Query;

            TModel Model = DynamicGetSingleRow(query);

            if (AllowCache)
            {
                Cache.Add(Model.Clone<TModel>(true));
            }

            return Model;
        }

        public virtual TModel DynamicGetSingleRow(SelectQuery query)
        {
            OnBeforeGet();
            TModel model = DynamicGetSingleRowSimple(query);
            OnAfterGet(ref model);

            return model;
        }

        public virtual TModel DynamicGetSingleRowSimple(SelectQuery query)
        {
            TModel model = DataAccess.DynamicSelectSingleRow(query);

            if (AllowCache)
            {
                Cache.Add(model.Clone<TModel>(true));
            }

            return model;
        }

        public virtual Int32 GetCountByFilter(Expression<Func<TModel, Boolean>> expression)
        {
            //To Be Enhanced
            var query = SelectQueryBuilder.Initialize().From<TModel>().Count().Where(expression).Query;
            return DynamicGetScalar<Int32>(query);
        }

        public virtual object DynamicGetScalar(SelectQuery query)
        {
            return DynamicGetScalar<Object>(query);
        }

        public virtual TResult DynamicGetScalar<TResult>(SelectQuery query)
        {
            return DataAccess.DynamicSelectScalar<TResult>(query);
        }

        #endregion
    }
}
