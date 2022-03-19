using System.ServiceModel;
using DotFramework.Infra.Model;

namespace DotFramework.Infra.ServiceFactory
{
    [ServiceContract]
    public interface ISecureServiceBase<TKey, TModel, TModelCollection> : IServiceBase<TKey, TModel, TModelCollection>, ISecureServiceBase
        where TModel : DomainModelBase
        where TModelCollection : ListBase<TKey, TModel>
    {
        
    }

    [ServiceContract]
    public interface ISecureServiceBase : IServiceBase
    {
        
    }
}