using DotFramework.Infra.BusinessRules;
using DotFramework.Infra.DataAccessFactory;
using DotFramework.Infra.Model;
using DotFramework.Infra.ServiceFactory;

namespace DotFramework.Infra.BusinessFacade
{
    public abstract class SecureServiceBase<TKey, TModel, TModelCollection, TBusinessRules, TDataAccess> : ServiceBase<TKey, TModel, TModelCollection, TBusinessRules, TDataAccess>, ISecureServiceBase<TKey, TModel, TModelCollection>
        where TModel : SecureDomainModelBase, new()
        where TModelCollection : ListBase<TKey, TModel>, new()
        where TDataAccess : IDataAccessBase<TKey, TModel, TModelCollection>
        where TBusinessRules : RulesBase<TKey, TModel, TModelCollection, TDataAccess>, new()
    {
        
    }

    public abstract class SecureServiceBase : ServiceBase, ISecureServiceBase
    {
        
    }
}