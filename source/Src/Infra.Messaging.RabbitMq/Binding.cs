using DotFramework.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotFramework.Infra.Messaging.RabbitMq
{
    public class RabbitMqBindingItem : ValueObject<RabbitMqBindingItem>
    {
        public RabbitMqBindingItem(string exchangeName, string exchangeType, string queueName, string routingKey, bool enabled = true)
        {
            if (exchangeName.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(exchangeName));

            if (exchangeType.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(exchangeType));

            if (queueName.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(queueName));

            QueueName = queueName;
            RoutingKey = routingKey;
            ExchangeName = exchangeName;
            ExchangeType = exchangeType;
            Enabled = enabled;
        }

        public string ExchangeName { get; }
        public string ExchangeType { get; }
        public string QueueName { get; }
        public string RoutingKey { get; }
        public bool Enabled { get; set; }
    }

    public class RabbitMqBinding : HashSet<RabbitMqBindingItem>
    {
        //private readonly HashSet<RabbitMqBindingItem> _bindings;

        //public RabbitMqBinding()
        //{
        //    _bindings = new HashSet<RabbitMqBindingItem>();
        //}


        //public void Add(RabbitMqBindingItem binding)
        //{
        //    if (!_bindings.Add(binding))
        //        throw new ArgumentException(nameof(binding));
        //}

        public bool Has(RabbitMqBindingItem binding)
        {
            return Contains(binding);
        }

        public IEnumerable<RabbitMqBindingItem> Get()
        {
            return this.ToList();
        }

        public IEnumerable<RabbitMqBindingItem> Get(string queueName)
        {
            return this.ToList().Where(b => b.QueueName == queueName);
        }
    }
}