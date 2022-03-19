using System.Collections.Generic;
using NetMQ;

namespace DotFramework.Infra.Messaging.ZeroMq
{
    public class ZeroMqMessageQueueFactory : MessageQueueFactory
    {
        private readonly IDictionary<string, string> _bindings;

        public ZeroMqMessageQueueFactory(IDictionary<string, string> bindings)
        {
            _bindings = bindings;
        }

        public override IMessageQueue CreateMessageQueue(Direction direction)
        {
            return new ZeroMqMessageQueue(_bindings);
        }
    }


    public struct ZeroMqBindingItem
    {
        public ZeroMqBindingItem(string name, string address)
        {
            Name = name;
            Address = address;

        }
        public string Name { get; }
        public string Address { get; }
    }

    public class ZeroMqBinding
    {
        private readonly IList<ZeroMqBindingItem> _bindings;

        public ZeroMqBinding()
        {
            _bindings = new List<ZeroMqBindingItem>();
        }

        public ZeroMqBinding(IList<ZeroMqBindingItem> bindings)
        {
            _bindings = bindings;
        }

        public void Add(ZeroMqBindingItem binding)
        {
            _bindings.Add(binding);
        }
    }

}