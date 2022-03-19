using System;

namespace DotFramework.Infra.Messaging.Consolsys
{
    public interface IMessagingServer : IDisposable
    {
        void ReceiveRequest<TRequest>(Action<TRequest> onRequestReceived);
        void ReceiveRequest(Action<Message> onRequestReceived);
        void SendResult<TMessage>(TMessage message);
        void SendResult(Message message);
    }
}