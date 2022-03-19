using RabbitMQ.Client;
using System;
using System.Collections.Generic;

namespace DotFramework.Infra.Messaging.RabbitMq
{
    public class BaseRabbitMqMessageQueueRpc
    {
        protected const string DeliveryTag = "DeliveryTag";
        protected readonly RabbitMqConfig RabbitMqConfig;
        protected IModel Channel;
        protected MessageQueueConfig Config;
        protected MessagePattern MessagePattern;
        private IConnection Connection;

        public BaseRabbitMqMessageQueueRpc(RabbitMqConfig rabbitMqConfig)
        {
            RabbitMqConfig = rabbitMqConfig;
        }

        public string Address { get; }

        public string Name => Config.Name;

        public IDictionary<string, string> Properties { get; }

        public string GetAddress(string name)
        {
            throw new NotImplementedException();
        }

        protected IModel CreateChannel()
        {
            var factory = new ConnectionFactory
            {
                HostName = RabbitMqConfig.HostName,
                UserName = RabbitMqConfig.UserName,
                Password = RabbitMqConfig.Password,
            };

            Connection = factory.CreateConnection();
            Channel = Connection.CreateModel();

            return Channel;
        }

        protected void CreateExchange(string exchangeName)
        {
            CreateRabbitExchange(exchangeName, GetExchangeType(Config.MessagePattern));
        }

        protected void CreateQueue(string queueName)
        {
            var bindingItems = RabbitMqConfig.Bindings.Get(queueName);
            foreach (var binding in bindingItems)
            {
                CreateRabbitExchange(binding.ExchangeName, binding.ExchangeType);
                CreateAndBindRabbitMqQueue(binding.ExchangeName, binding.QueueName, binding.RoutingKey);
            }
        }

        protected void CreateAndBindRabbitMqQueue(string exchangeName, string queueName, string routingKey)
        {
            CreateRabbitQueue(queueName);
            BindRabbitQueue(exchangeName, queueName, routingKey);
        }

        protected void CreateRabbitQueue(string queueName)
        {
            Channel.QueueDeclare(queueName, true, false, false, null);
        }

        protected void BindRabbitQueue(string exchangeName, string queueName, string routingKey)
        {
            Channel.QueueBind(queueName, exchangeName, routingKey);
        }

        protected string GetExchangeType(MessagePattern pattern)
        {
            switch (pattern)
            {
                case MessagePattern.FireAndForget:
                    return ExchangeType.Fanout;
                case MessagePattern.RequestResponse:
                    return ExchangeType.Direct;
                case MessagePattern.PublishSubscribe:
                    return ExchangeType.Fanout;
                default:
                    throw new ArgumentOutOfRangeException(nameof(pattern), pattern, null);
            }
        }

        protected string GetExchangeType(MessagePattern pattern, Direction direction, string name, string key)
        {
            if (pattern == MessagePattern.FireAndForget && key.IsNullOrEmpty())
                return ExchangeType.Fanout;

            switch (pattern)
            {
                case MessagePattern.FireAndForget:
                    return ExchangeType.Fanout;
                case MessagePattern.RequestResponse:
                    return ExchangeType.Direct;
                case MessagePattern.PublishSubscribe:
                    return ExchangeType.Fanout;
                default:
                    throw new ArgumentOutOfRangeException(nameof(pattern), pattern, null);
            }
        }

        protected void CreateRabbitExchange(string exchangeName, string exchangeType)
        {
            Channel.ExchangeDeclare(exchangeName, exchangeType, true, false, null);
        }

        public IMessageQueue GetResponseQueue()
        {
            throw new NotImplementedException();
        }

        public IMessageQueue GetReplyQueue(Message message)
        {
            throw new NotImplementedException();
        }

        public void Listen(Action<Message> onMessageReceived)
        {
            throw new NotImplementedException();
        }

        public void Listen(Action<Message> onMessageReceived, string key)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            if (Channel != null)
            {
                if (!Channel.IsClosed)
                    Channel.Close();
                Channel.Dispose();
            }
            if (Connection != null)
            {
                Connection.Close();
                Connection.Dispose();
            }
        }
    }
}