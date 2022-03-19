using DotFramework.Infra.BusinessRules;
using DotFramework.Infra.DataAccessFactory;
using DotFramework.Infra.ServiceFactory;

namespace DotFramework.Infra.BusinessFacade
{
    public abstract class SecureGeneralServiceBase<TBusinessRules, TDataAccess> : GeneralServiceBase<TBusinessRules, TDataAccess>, ISecureGeneralServiceBase
        where TDataAccess : IGeneralDataAccessBase
        where TBusinessRules : GeneralRulesBase<TDataAccess>, new()
    {
        
    }
}