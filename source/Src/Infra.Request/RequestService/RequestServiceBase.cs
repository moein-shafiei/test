namespace DotFramework.Infra.Request
{
    public abstract class RequestServiceBase : IRequestService
    {
    }

    public abstract class RequestServiceBase<TResponse> : RequestServiceBase, IRequestService<TResponse>
        where TResponse : ResponseBase
    {
        public abstract TResponse ProcessRequest();
    }

    public abstract class RequestServiceBase<TRequest, TResponse>: RequestServiceBase, IRequestService<TRequest, TResponse>
        where TRequest : RequestBase
        where TResponse : ResponseBase
    {
        public abstract TResponse ProcessRequest(TRequest request);
    }
}
