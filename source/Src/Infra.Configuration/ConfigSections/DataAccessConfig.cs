using System;
using System.Configuration;

namespace DotFramework.Infra.Configuration
{
    public class DataAccessConfigGroupSection : ConfigurationSectionGroup
    {

    }

    public class DataAccessConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("projectName", IsRequired = true)]
        public String ProjectName
        {
            get
            {
                return (String)base["projectName"];
            }
            set
            {
                base["projectName"] = value;
            }
        }

        [ConfigurationProperty("dllPath", IsRequired = true)]
        public String DllPath
        {
            get
            {
                return (String)base["dllPath"];
            }
            set
            {
                base["dllPath"] = value;
            }
        }

        [ConfigurationProperty("factoryDllPath", IsRequired = true)]
        public String FactoryDllPath
        {
            get
            {
                return (String)base["factoryDllPath"];
            }
            set
            {
                base["factoryDllPath"] = value;
            }
        }

        [ConfigurationProperty("factoryType", IsRequired = true)]
        public String FactoryType
        {
            get
            {
                return (String)base["factoryType"];
            }
            set
            {
                base["factoryType"] = value;
            }
        }

        [ConfigurationProperty("nameSpace", IsRequired = true)]
        public String NameSpace
        {
            get
            {
                return (String)base["nameSpace"];
            }
            set
            {
                base["nameSpace"] = value;
            }
        }

        [ConfigurationProperty("connectionName", IsRequired = false)]
        public String ConnectionName { get { return (String)base["connection"]; } }

        public ConnectionElement Connection
        {
            get
            {
                return ((ConnectionConfigSection)ConfigurationManager.GetSection("connectionConfigSection")).Connections[base["connectionName"].ToString()];
            }
        }

        [ConfigurationProperty("dataAccessServices")]
        public DataAccessServiceElementCollection DataAccessServices
        {
            get { return ((DataAccessServiceElementCollection)(base["dataAccessServices"])); }
            set { base["dataAccessServices"] = value; }
        }
    }

    public class DataAccessServiceElement : ConfigurationElement
    {
        [ConfigurationProperty("contractType", IsRequired = true, IsKey = true)]
        public String ContractType
        {
            get { return (String)base["contractType"]; }
        }

        [ConfigurationProperty("serviceType", IsRequired = true)]
        public String ServiceType
        {
            get { return (String)base["serviceType"]; }
        }

        [ConfigurationProperty("connectionName", IsRequired = false)]
        public String ConnectionName { get { return (String)base["connection"]; } }

        public ConnectionElement Connection
        {
            get
            {
                if (base["connectionName"] == null || base["connectionName"].ToString() == String.Empty)
                {
                    return null;
                }
                else
                {
                    return ((ConnectionConfigSection)ConfigurationManager.GetSection("connectionConfigSection")).Connections[base["connectionName"].ToString()];
                }
            }
        }
    }

    [ConfigurationCollection(typeof(DataAccessServiceElement))]
    public class DataAccessServiceElementCollection : ConfigurationElementCollection
    {
        internal const string PropertyName = "dataAccessService";

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
            return new DataAccessServiceElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DataAccessServiceElement)(element)).ContractType;
        }

        public new DataAccessServiceElement this[String name]
        {
            get { return (DataAccessServiceElement)BaseGet(name); }
        }
    }
}
