using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace DotFramework.Infra.Messaging.RabbitMq
{
    public class RabbitMqMessageQueuePubSub : BaseRabbitMqMessageQueueRpc, IMessageQueue
    {
#pragma warning disable 0618
        private QueueingBasicConsumer _consumer;
#pragma warning restore 0618

        public RabbitMqMessageQueuePubSub(RabbitMqConfig rabbitMqConfig) : base(rabbitMqConfig)
        {
        }

        public void InitializeOutbound(string exchangeName, string queueName, MessagePattern pattern)
        {
            throw new NotImplementedException();
        }

        public void InitializeOutbound(string name, MessagePattern pattern)
        {
            Config = new MessageQueueConfig(name, pattern);
            
            Channel = CreateChannel();

            CreateExchange(Config.Name);
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
            Config = config;

            Channel = CreateChannel();

            CreateQueue(config.Name);
#pragma warning disable 0618
            _consumer = new QueueingBasicConsumer(Channel);
#pragma warning restore 0618
            Channel.BasicConsume(config.Name,
                true,
                _consumer);
        }

        public void Send(Message message)
        {
            Send(message, "");
        }

        public void Send(Message message, string key)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            Channel.BasicPublish(Config.Name,
                key,
                null,
                body);
        }

        public void Received(Action<Message> onMessageReceived)
        {
            var ea = _consumer.Queue.Dequeue();
            var result = Encoding.UTF8.GetString(ea.Body);
            onMessageReceived.Invoke(JsonConvert.DeserializeObject<Message>(result));
        }
    }
}