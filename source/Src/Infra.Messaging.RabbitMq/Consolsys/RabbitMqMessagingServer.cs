using DotFramework.Infra.Messaging.Configuration;
using DotFramework.Infra.Messaging.Consolsys;
using System;
using System.Collections.Generic;

namespace DotFramework.Infra.Messaging.RabbitMq.Consolsys
{
    public class RabbitMqMessagingServer : IMessagingServer
    {
        private readonly IMessageQueue _server;
        private string _responseAddress;
        private IDictionary<string, object> _properties;
        private byte[] _responseKey;

        #region Ctors
        public RabbitMqMessagingServer(IMessageQueueFactory factory)
        {
            _server = factory.CreateInboundQueue("rpc_queue", MessagePattern.RequestResponse);
        }

        public RabbitMqMessagingServer(IMessageQueueFactory factory, string queueName, MessagePattern pattern)
        {
            _server = factory.CreateInboundQueue(queueName, pattern);
        }

        public RabbitMqMessagingServer(string serverName = "")
        {
            var config = MessagingConfigSection.Current;

            if (config == null || config.Servers == null || config.Servers.Count <= 0)
                throw new ArgumentNullException("Missing MessagingConfig / MessagingConfig's Servers");

            ServerElement server = string.IsNullOrEmpty(serverName) ? config.Servers[0] : config.Servers[serverName];

            if (server == default(ServerElement)) throw new ArgumentException("Invalid MessagingConfig's Servers");

            var rabbitConfig = GetRabbitMqConfig(server);

            var factory = new RabbitMqMessageQueueFactory(rabbitConfig);

            _server = factory.CreateInboundQueue(server.QueueName, server.MessagePattern);
        }
        #endregion

        public void ReceiveRequest<TRequest>(Action<TRequest> onRequestReceived)
        {
            _server.Received(message => { OnReceive(onRequestReceived, message); });
        }

        private void OnReceive<TRequest>(Action<TRequest> onRequestReceived, Message message)
        {
            var result = message.BodyAs<TRequest>();
            _responseAddress = message.ResponseAddress;
            _properties = message.Properties;
            _responseKey = message.ResponseKey;
            onRequestReceived.Invoke(result);
        }

        public void SendResult<TMessage>(TMessage message)
        {
            _server.Send(new Message
            {
                Body = message,
                ResponseAddress = _responseAddress,
                Properties = _properties,
                ResponseKey = _responseKey
            });
        }

        public void ReceiveRequest(Action<Message> onRequestReceived)
        {
            _server.Received(message => onRequestReceived.Invoke(message));
        }

        public void SendResult(Message message)
        {
            _server.Send(message);
        }

        public void Dispose()
        {
            _server.Dispose();
        }

        private RabbitMqConfig GetRabbitMqConfig(ServerElement server)
        {
            var bindings = new RabbitMqBinding
            {
                new RabbitMqBindingItem(server.ExchangeName, server.ExchangeType, server.QueueName, server.RoutingKey, server.Enabled)
            };

            return new RabbitMqConfig(server.Host, server.Username, server.Password, bindings)
            {
                CreateExchange = true,
                CreateQueue = true
            };
        }
    }
}