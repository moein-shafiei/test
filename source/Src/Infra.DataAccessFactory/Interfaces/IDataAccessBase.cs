using DotFramework.DynamicQuery;
using DotFramework.Infra.Model;
using System;

namespace DotFramework.Infra.DataAccessFactory
{
    public interface IDataAccessBase
    {
        IConnectionHandler ConnectionHandler { get; }
        void SetConnectionString(string connectionString);

        object DynamicSelectScalar(SelectQuery query);
        TResult DynamicSelectScalar<TResult>(SelectQuery query);
    }

    public interface IDataAccessBase<TKey, TModel, TModelCollection> : IDataAccessBase
    {
        TModel Select(TKey key);
        int SelectCount();
        TModelCollection SelectAll();
        TModelCollection SelectAllWithPaging(Int32 PageIndex, Int32 RowsInPage);
        bool Insert(TModel model);
        bool Update(TModel model);
        bool Delete(TKey key);
        TModelCollection CustomQuery(CustomQuery query);
        TModel CustomQuerySingleRow(CustomQuery query);
        TModelCollection DynamicSelect(SelectQuery query);
        TModel DynamicSelectSingleRow(SelectQuery query);
    }
}
