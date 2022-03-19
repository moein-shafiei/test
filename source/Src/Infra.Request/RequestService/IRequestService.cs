namespace DotFramework.Infra.Request
{
    public interface IRequestService
    {
        
    }

    public interface IRequestService<TResponse> : IRequestService
        where TResponse : ResponseBase
    {
        TResponse ProcessRequest();
    }

    public interface IRequestService<TRequest, TResponse> : IRequestService
        where TRequest : RequestBase
        where TResponse : ResponseBase
    {
        TResponse ProcessRequest(TRequest request);
    }
}
