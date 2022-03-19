namespace DotFramework.Infra.Messaging
{
    public class MessageQueueConfig
    {
        public MessageQueueConfig()
        {

        }

        public MessageQueueConfig(string name, MessagePattern pattern)
        {
            Name = name;
            MessagePattern = pattern;
        }

        public MessageQueueConfig(string name, string exchangeName, string exchangeType, string routingKey, bool enabled, MessagePattern pattern)
        {
            Name = name;
            ExchangeName = exchangeName;
            ExchangeType = exchangeType;
            RoutingKey = routingKey;
            MessagePattern = pattern;
            Enabled = enabled;
        }

        public string SubscribeKey { get; set; }
        public string Name { get; set; }
        public string ExchangeType { get; set; }
        public string ExchangeName { get; set; }
        public string RoutingKey { get; set; }
        public bool Enabled { get; set; }
        public MessagePattern MessagePattern { get; set; }
    }
}