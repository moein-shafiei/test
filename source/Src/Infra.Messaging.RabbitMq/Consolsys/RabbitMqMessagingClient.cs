using DotFramework.Core;
using DotFramework.Infra.Messaging.Configuration;
using DotFramework.Infra.Messaging.Consolsys;
using System;

namespace DotFramework.Infra.Messaging.RabbitMq.Consolsys
{
    public class RabbitMqMessagingClient : IMessagingClient
    {
        private readonly IMessageQueue _client;

        private int sendThreads = 1;
        private IBlockingQueue<object> sendQueue;
        private IQueueConsumer<object> sendQueueConsumer;

        public RabbitMqMessagingClient(IMessageQueueFactory factory)
        {
            _client = factory.CreateOutboundQueue("rpc_queue", MessagePattern.RequestResponse);
            if (_client == null)
                throw new Exception($"Error on creating messaging service connection");

            InitializeSendQueue();
        }

        public RabbitMqMessagingClient(IMessageQueueFactory factory, string queueName, MessagePattern pattern)
        {
            _client = factory.CreateOutboundQueue(queueName, pattern);
            if (_client == null)
                throw new Exception($"Error on creating messaging service connection");

            InitializeSendQueue();
        }

        public RabbitMqMessagingClient(string clientName)
        {
            var config = MessagingConfigSection.Current;

            if (config == null || config.Clients == null || config.Clients.Count <= 0)
                throw new ArgumentNullException("Missing MessagingConfig / MessagingConfig's Clients");

            ClientElement client = string.IsNullOrEmpty(clientName) ? config.Clients[0] : config.Clients[clientName];

            if (client == default(ClientElement)) throw new ArgumentException("Invalid MessagingConfig's Clients");

            var rabbitConfig = GetRabbitMqConfig(client);

            var factory = new RabbitMqMessageQueueFactory(rabbitConfig);

            _client = factory.CreateOutboundQueue(client.QueueName, client.MessagePattern);
            if (_client == null)
                throw new Exception($"Error on creating messaging service connection");

            this.sendThreads = client.ClientThreads;
            InitializeSendQueue();
        }

        private void InitializeSendQueue()
        {
            this.sendQueue = new BlockingQueue<object>();
            this.sendQueueConsumer = new QueueConsumer<object>(this.sendThreads);
            this.sendQueueConsumer.Start(this.sendQueue, _sendBuffered);
        }

        private void _sendBuffered(object obj)
        {
            if (obj is Message message)
            {
                this._client.Send(message);
            }
            else
            {
                var t = (Tuple<Message, string>)obj;
                this._client.Send(t.Item1, t.Item2);
            }
        }

        public void SendRequestBuffered<TMessage>(TMessage message)
        {
            this.sendQueue.Enqueue(new Message { Body = message });
        }

        public void SendRequestBuffered<TMessage>(TMessage message, string routingKey)
        {
            this.sendQueue.Enqueue(new Tuple<Message, string>(new Message { Body = message }, routingKey));
        }

        public void SendRequest<TMessage>(TMessage message)
        {
            _client.Send(new Message { Body = message });
        }

        public void SendRequest<TMessage>(TMessage message, string routingKey)
        {
            _client.Send(new Message { Body = message }, routingKey);
        }

        public TResult ReceiveResult<TResult>()
        {
            var result = default(TResult);
            _client.Received(message => { result = message.BodyAs<TResult>(); });
            return result;
        }

        private RabbitMqConfig GetRabbitMqConfig(ClientElement client)
        {
            var bindings = new RabbitMqBinding
            {
                new RabbitMqBindingItem(client.ExchangeName, client.ExchangeType, client.QueueName, client.RoutingKey, client.Enabled)
            };

            return new RabbitMqConfig(client.Host, client.Username, client.Password, bindings)
            {
                CreateExchange = true,
                CreateQueue = true
            };
        }
    }
}