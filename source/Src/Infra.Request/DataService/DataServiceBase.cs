namespace DotFramework.Infra.Request
{
    public abstract class DataServiceBase : IDataService
    {
    }

    public abstract class DataServiceBase<TResponse> : DataServiceBase, IDataService<TResponse>
        where TResponse : ResponseBase
    {
        public abstract TResponse ProcessRequest();
    }

    public abstract class DataServiceBase<TRequest, TResponse>: DataServiceBase, IDataService<TRequest, TResponse>
        where TRequest : RequestBase
        where TResponse : ResponseBase
    {
        public abstract TResponse ProcessRequest(TRequest request);
    }
}
