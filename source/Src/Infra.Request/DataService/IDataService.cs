namespace DotFramework.Infra.Request
{
    public interface IDataService
    {
        
    }

    public interface IDataService<TResponse> : IDataService
        where TResponse : ResponseBase
    {
        TResponse ProcessRequest();
    }

    public interface IDataService<TRequest, TResponse> : IDataService
        where TRequest : RequestBase
        where TResponse : ResponseBase
    {
        TResponse ProcessRequest(TRequest request);
    }
}
