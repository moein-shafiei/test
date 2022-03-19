namespace DotFramework.Infra.ServiceFactory
{
    public class SecureServiceFactoryBase<TServiceFacory> : ServiceFactoryBase<TServiceFacory, AuthorizationInterceptionBehavior, AuthorizationEndpointBehavior> where TServiceFacory : class
    {
    }
}
