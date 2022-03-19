using DotFramework.Core;
using System;
using System.Configuration;

namespace DotFramework.Infra.Configuration
{
    public class ConnectionConfigGroupSection : ConfigurationSectionGroup
    {

    }

    public class ConnectionConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("connections")]
        public ConnectionElementCollection Connections
        {
            get { return ((ConnectionElementCollection)(base["connections"])); }
            set { base["connections"] = value; }
        }
    }

    public class ConnectionElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public String Name
        {
            get { return (String)base["name"]; }
        }

        [ConfigurationProperty("provider", DefaultValue = DatabaseProvider.SqlServer, IsRequired = true)]
        public DatabaseProvider Provider
        {
            get { return (DatabaseProvider)base["provider"]; }
            set { base["provider"] = value; }
        }

        [ConfigurationProperty("connectionString", IsRequired = true)]
        public String ConnectionString
        {
            get { return (String)base["connectionString"]; }
            set { base["connectionString"] = value; }
        }

        [ConfigurationProperty("isEncrypted", DefaultValue = false, IsRequired = true)]
        public Boolean IsEncrypted
        {
            get { return (Boolean)base["isEncrypted"]; }
            set { base["isEncrypted"] = value; }
        }
    }

    [ConfigurationCollection(typeof(ConnectionElement))]
    public class ConnectionElementCollection : ConfigurationElementCollection
    {
        internal const string PropertyName = "connection";

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
            return new ConnectionElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ConnectionElement)(element)).Name;
        }

        public new ConnectionElement this[String name]
        {
            get { return (ConnectionElement)BaseGet(name); }
        }
    }
}
