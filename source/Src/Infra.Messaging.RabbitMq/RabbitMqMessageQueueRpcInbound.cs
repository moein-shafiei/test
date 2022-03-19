using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Linq;
using System.Text;

namespace DotFramework.Infra.Messaging.RabbitMq
{
    public class RabbitMqMessageQueueRpcInbound : BaseRabbitMqMessageQueueRpc, IMessageQueue
    {
        private Action<Message> _onMessageReceived;

        private EventingBasicConsumer _eventingBasicConsumer;

        public RabbitMqMessageQueueRpcInbound(RabbitMqConfig rabbitMqConfig) : base(rabbitMqConfig)
        {
        }

        public void InitializeOutbound(string name, MessagePattern pattern)
        {
            throw new NotImplementedException();
        }

        public void InitializeInbound(string name, MessagePattern pattern)
        {
            MessagePattern = pattern;

            var b = RabbitMqConfig.Bindings.Get(name).FirstOrDefault();
            if (b == null) throw new ArgumentNullException();

            Config = new MessageQueueConfig(b.QueueName, b.ExchangeName, b.ExchangeType, b.RoutingKey, b.Enabled, MessagePattern);
            InitializeInbound(Config);
        }

        public void InitializeInbound(MessageQueueConfig config)
        {
            Config = config;
            if (!config.Enabled) return;

            CreateChannel();
            CreateQueue(Config.Name);
        }

        public void Send(Message message)
        {
            if (!Config.Enabled) return;

            if (MessagePattern == MessagePattern.RequestResponse)
            {
                var replyProps = Channel.CreateBasicProperties();
                replyProps.CorrelationId = Encoding.UTF8.GetString(message.ResponseKey);

                var responseBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                //Please leave Callback Exchange Name to blank
                Channel.BasicPublish("", message.ResponseAddress, replyProps, responseBytes);
            }

            var deliveryTag = Convert.ToUInt64(message.Properties[DeliveryTag]);
            Channel.BasicAck(deliveryTag, false);
        }

        public void Send(Message message, string key)
        {
            throw new NotImplementedException();
        }

        public void Received(Action<Message> onMessageReceived)
        {
            if (!Config.Enabled) return;

            _onMessageReceived = onMessageReceived;

            _eventingBasicConsumer = new EventingBasicConsumer(Channel);
            _eventingBasicConsumer.Received += OnRabbitMqReceived;
            Channel.BasicConsume(Config.Name, false, _eventingBasicConsumer);
        }

        private void OnRabbitMqReceived(object model, BasicDeliverEventArgs ea)
        {
            var body = ea.Body;
            var props = ea.BasicProperties;

            var stringBody = Encoding.UTF8.GetString(body);

            var message = JsonConvert.DeserializeObject<Message>(stringBody);

            if (MessagePattern == MessagePattern.RequestResponse)
            {
                message.ResponseAddress = props.ReplyTo;
                message.ResponseKey = Encoding.UTF8.GetBytes(props.CorrelationId);
            }
            message.Properties.Add(DeliveryTag, ea.DeliveryTag.ToString());

            _onMessageReceived?.Invoke(message);
        }
    }
}