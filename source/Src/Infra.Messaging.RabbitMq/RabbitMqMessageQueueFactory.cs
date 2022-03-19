using System;

namespace DotFramework.Infra.Messaging.RabbitMq
{
    public class RabbitMqMessageQueueFactory : IMessageQueueFactory
    {
        private readonly RabbitMqConfig _rabbitMqConfig;

        public RabbitMqMessageQueueFactory(RabbitMqConfig rabbitMqConfig)
        {
            _rabbitMqConfig = rabbitMqConfig;
        }

        public IMessageQueue CreateInboundQueue(string name, MessagePattern pattern)
        {
            var que = CreateMessageQueue(Direction.Inbound);

            que.InitializeInbound(name, pattern);
            return que;
        }

        public IMessageQueue CreateInboundQueue(MessageQueueConfig config)
        {
            var que = CreateMessageQueue(Direction.Inbound);

            que.InitializeInbound(config.Name, config.MessagePattern);
            return que;
        }

        public IMessageQueue CreateOutboundQueue(string name, MessagePattern pattern)
        {
            var que = CreateMessageQueue(Direction.OutBound);
            que.InitializeOutbound(name, pattern);
            return que;
        }

        public IMessageQueue CreateMessageQueue(Direction direction)
        {
            switch (direction)
            {
                case Direction.Inbound:
                    return new RabbitMqMessageQueueRpcInbound(_rabbitMqConfig);
                case Direction.OutBound:
                    return new RabbitMqMessageQueueRpcOutbound(_rabbitMqConfig);
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
    }
}