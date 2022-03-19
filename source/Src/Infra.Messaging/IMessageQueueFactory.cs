namespace DotFramework.Infra.Messaging
{
    public interface IMessageQueueFactory
    {
        IMessageQueue CreateInboundQueue(string name, MessagePattern pattern);
        IMessageQueue CreateInboundQueue(MessageQueueConfig config);
        IMessageQueue CreateOutboundQueue(string name, MessagePattern pattern);
    }
}