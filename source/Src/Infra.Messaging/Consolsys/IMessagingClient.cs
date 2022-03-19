using System;

namespace DotFramework.Infra.Messaging.Consolsys
{
    public interface IMessagingClient
    {
        void SendRequestBuffered<TMessage>(TMessage message);
        void SendRequestBuffered<TMessage>(TMessage message, string routingKey);
        void SendRequest<TMessage>(TMessage message);
        void SendRequest<TMessage>(TMessage message, string routingKey);
        TResult ReceiveResult<TResult>();
    }
}