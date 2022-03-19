using System;
using System.Configuration;

namespace DotFramework.Infra.Messaging.Configuration
{
    public class MessagingConfigSection : ConfigurationSection
    {
        private static MessagingConfigSection _config;

        public static MessagingConfigSection Current
        {
            get
            {
                return _config;
            }
        }

        static MessagingConfigSection()
        {
            _config = ConfigurationManager.GetSection("MessagingConfig") as MessagingConfigSection;
        }

        [ConfigurationProperty("servers")]
        public ServerElementCollection Servers
        {
            get { return (ServerElementCollection)this["servers"]; }
        }

        [ConfigurationProperty("clients")]
        public ClientElementCollection Clients
        {
            get { return (ClientElementCollection)this["clients"]; }
        }
    }

    public class ServerElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)base["name"]; }
        }

        [ConfigurationProperty("host", IsRequired = true)]
        public string Host
        {
            get { return (string)base["host"]; }
        }

        [ConfigurationProperty("username")]
        public string Username
        {
            get { return (string)base["username"]; }
        }

        [ConfigurationProperty("password")]
        public string Password
        {
            get { return (string)base["password"]; }
        }

        [ConfigurationProperty("exchangeName")]
        public string ExchangeName
        {
            get { return (string)base["exchangeName"]; }
        }

        [ConfigurationProperty("exchangeType", DefaultValue = "fanout")]
        public string ExchangeType
        {
            get { return (string)base["exchangeType"]; }
        }

        [ConfigurationProperty("queueName")]
        public string QueueName
        {
            get { return (string)base["queueName"]; }
        }

        [ConfigurationProperty("routingKey", DefaultValue = "")]
        public string RoutingKey
        {
            get { return (string)base["routingKey"]; }
        }

        [ConfigurationProperty("messagePattern", DefaultValue = MessagePattern.FireAndForget)]
        public MessagePattern MessagePattern
        {
            get { return (MessagePattern)((int)base["messagePattern"]); }
        }

        [ConfigurationProperty("enabled", DefaultValue = true)]
        public bool Enabled
        {
            get { return (bool)base["enabled"]; }
        }
    }

    public class ClientElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)base["name"]; }
        }

        [ConfigurationProperty("host", IsRequired = true)]
        public string Host
        {
            get { return (string)base["host"]; }
        }

        [ConfigurationProperty("username")]
        public string Username
        {
            get { return (string)base["username"]; }
        }

        [ConfigurationProperty("password")]
        public string Password
        {
            get { return (string)base["password"]; }
        }

        [ConfigurationProperty("exchangeName")]
        public string ExchangeName
        {
            get { return (string)base["exchangeName"]; }
        }

        [ConfigurationProperty("exchangeType", DefaultValue = "fanout")]
        public string ExchangeType
        {
            get { return (string)base["exchangeType"]; }
        }

        [ConfigurationProperty("queueName")]
        public string QueueName
        {
            get { return (string)base["queueName"]; }
        }

        [ConfigurationProperty("routingKey", DefaultValue = "")]
        public string RoutingKey
        {
            get { return (string)base["routingKey"]; }
        }

        [ConfigurationProperty("messagePattern", DefaultValue = MessagePattern.FireAndForget)]
        public MessagePattern MessagePattern
        {
            get { return (MessagePattern)((int)base["messagePattern"]); }
        }

        [ConfigurationProperty("clientThreads", DefaultValue = 1)]
        public int ClientThreads
        {
            get { return (int)base["clientThreads"]; }
        }

        [ConfigurationProperty("enabled", DefaultValue = true)]
        public bool Enabled
        {
            get { return (bool)base["enabled"]; }
        }
    }

    [ConfigurationCollection(typeof(ServerElement))]
    public class ServerElementCollection : ConfigurationElementCollection
    {
        internal const string PropertyName = "server";

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMapAlternate;
            }
        }
        protected override string ElementName
        {
            get
            {
                return PropertyName;
            }
        }

        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(PropertyName, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ServerElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ServerElement)(element)).Name;
        }

        public new ServerElement this[string name]
        {
            get
            {
                if (IndexOf(name) < 0) return null;
                return (ServerElement)BaseGet(name);
            }
        }

        public ServerElement this[int index]
        {
            get { return (ServerElement)BaseGet(index); }
        }

        public int IndexOf(string name)
        {
            name = name.ToLower();

            for (int idx = 0; idx < base.Count; idx++)
            {
                if (this[idx].Name.ToLower() == name)
                    return idx;
            }
            return -1;
        }
    }

    [ConfigurationCollection(typeof(ClientElement))]
    public class ClientElementCollection : ConfigurationElementCollection
    {
        internal const string PropertyName = "client";

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMapAlternate;
            }
        }
        protected override string ElementName
        {
            get
            {
                return PropertyName;
            }
        }

        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(PropertyName, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ClientElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ClientElement)(element)).Name;
        }

        public new ClientElement this[string name]
        {
            get
            {
                if (IndexOf(name) < 0) return null;
                return (ClientElement)BaseGet(name);
            }
        }

        public ClientElement this[int index]
        {
            get { return (ClientElement)BaseGet(index); }
        }

        public int IndexOf(string name)
        {
            name = name.ToLower();

            for (int idx = 0; idx < base.Count; idx++)
            {
                if (this[idx].Name.ToLower() == name)
                    return idx;
            }
            return -1;
        }
    }
}