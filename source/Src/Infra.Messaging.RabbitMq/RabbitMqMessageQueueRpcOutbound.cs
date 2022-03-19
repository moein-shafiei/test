using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace DotFramework.Infra.Messaging.RabbitMq
{
    public class RabbitMqMessageQueueRpcOutbound : BaseRabbitMqMessageQueueRpc, IMessageQueue
    {
#pragma warning disable 0618
        private QueueingBasicConsumer _consumer;
#pragma warning restore 0618
        private string _correlationId;
        private string _replyQueueName;

        public RabbitMqMessageQueueRpcOutbound(RabbitMqConfig rabbitMqConfig) : base(rabbitMqConfig)
        {
        }

        public void InitializeOutbound(string name, MessagePattern pattern)
        {
            MessagePattern = pattern;
            var b = RabbitMqConfig.Bindings.Get(name).FirstOrDefault();
            if (b == null) throw new ArgumentNullException();
            Config = new MessageQueueConfig(b.QueueName, b.ExchangeName, b.ExchangeType, b.RoutingKey, b.Enabled, MessagePattern);

            if (!Config.Enabled) return;

            Channel = CreateChannel();
            CreateQueue(Config.Name);

            if (pattern == MessagePattern.RequestResponse)
            {
                _replyQueueName = Channel.QueueDeclare().QueueName;
#pragma warning disable 0618
                _consumer = new QueueingBasicConsumer(Channel);
#pragma warning restore 0618
                Channel.BasicConsume(_replyQueueName, true, _consumer);
            }
        }

        public void InitializeInbound(string exchangeName, string queueName, MessagePattern pattern)
        {
            throw new NotImplementedException();
        }

        public void InitializeInbound(string name, MessagePattern pattern)
        {
            var config = new MessageQueueConfig(name, pattern);
            InitializeInbound(config);
        }

        public void InitializeInbound(MessageQueueConfig config)
        {
        }

        public void Send(Message message)
        {
            if (!Config.Enabled) return;

            var props = Channel.CreateBasicProperties();

            if (MessagePattern == MessagePattern.RequestResponse)
            {
                _correlationId = Guid.NewGuid().ToString();
                props.CorrelationId = _correlationId;
                props.ReplyTo = _replyQueueName;
            }

            var messageBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            Channel.BasicPublish(Config.ExchangeName, Config.RoutingKey, props, messageBytes);
        }

        public void Send(Message message, string routingKey)
        {
            if (!Config.Enabled) return;

            var props = Channel.CreateBasicProperties();

            if (MessagePattern == MessagePattern.RequestResponse)
            {
                _correlationId = Guid.NewGuid().ToString();
                props.CorrelationId = _correlationId;
                props.ReplyTo = _replyQueueName;
            }

            var messageBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            Channel.BasicPublish(Config.ExchangeName, routingKey, props, messageBytes);
        }

        public void Received(Action<Message> onMessageReceived)
        {
            if (!Config.Enabled || Config.MessagePattern != MessagePattern.RequestResponse) return;

            var ea = _consumer.Queue.Dequeue();
            if (ea.BasicProperties.CorrelationId != _correlationId)
                return;
            var result = Encoding.UTF8.GetString(ea.Body);
            onMessageReceived.Invoke(JsonConvert.DeserializeObject<Message>(result));
        }
    }
}