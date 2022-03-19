using System;
using System.Configuration;

namespace DotFramework.Infra.Configuration
{
    public class DatabaseConfigGroupSection : ConfigurationSectionGroup
    {

    }

    public class DatabaseConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public String Name
        {
            get { return (String)base["name"]; }
        }

        [ConfigurationProperty("tables")]
        public TableElementCollection Tables
        {
            get { return ((TableElementCollection)(base["tables"])); }
            set { base["tables"] = value; }
        }
    }

    public class TableElement : ConfigurationElement
    {
        [ConfigurationProperty("key", IsRequired = true, IsKey = true)]
        public String Key
        {
            get { return (String)base["key"]; }
        }

        [ConfigurationProperty("name", IsRequired = true)]
        public String Name
        {
            get { return (String)base["name"]; }
        }
    }

    [ConfigurationCollection(typeof(TableElement))]
    public class TableElementCollection : ConfigurationElementCollection
    {
        internal const string PropertyName = "table";

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
            return new TableElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TableElement)(element)).Key;
        }

        public new TableElement this[String key]
        {
            get { return (TableElement)BaseGet(key); }
        }
    }
}
