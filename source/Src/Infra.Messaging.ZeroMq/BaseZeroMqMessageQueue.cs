using System;
using System.Collections.Generic;

namespace DotFramework.Infra.Messaging.ZeroMq
{
    public class BaseZeroMqMessageQueue
    {
        private readonly IDictionary<string, string> _addressMapping;

        protected MessageQueueConfig Config;

        public BaseZeroMqMessageQueue()
        {
        }

        public BaseZeroMqMessageQueue(IDictionary<string, string> addressMapping)
        {
            if (addressMapping == null)
                throw new ArgumentNullException(nameof(addressMapping));
            if (addressMapping.Count < 1)
                throw new ArgumentOutOfRangeException(nameof(addressMapping));

            _addressMapping = addressMapping;
        }

        public string Name => Config.Name;

        public string Address => GetAddress(Config.Name);

        public IDictionary<string, string> Properties { get; }

        public string GetAddress(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return _addressMapping[name.ToLower()];
        }
    }
}