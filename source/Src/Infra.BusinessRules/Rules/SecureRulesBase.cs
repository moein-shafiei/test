using DotFramework.Infra.DataAccessFactory;
using DotFramework.Infra.Model;
using DotFramework.Infra.Security;

namespace DotFramework.Infra.BusinessRules
{
    public abstract class SecureRulesBase : RulesBase
    {
        
    }

    public abstract class SecureRulesBase<TKey, TModel, TModelCollection, TDataAccess> : RulesBase<TKey, TModel, TModelCollection, TDataAccess>
        where TModel : SecureDomainModelBase, new()
        where TModelCollection : ListBase<TKey, TModel>, new()
        where TDataAccess : IDataAccessBase<TKey, TModel, TModelCollection>
    {
        protected override void OnBeforeAdd(ref TModel model)
        {
            base.OnBeforeAdd(ref model);
            model.SessionID = UserSessionManager.Instance.GetSessionID();
        }

        protected override void OnBeforeEdit(ref TModel model)
        {
            base.OnBeforeEdit(ref model);
            model.SessionID = UserSessionManager.Instance.GetSessionID();
        }
    }
}
