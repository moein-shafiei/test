using System;
using System.Configuration;

namespace DotFramework.Infra.Configuration
{
    public class ServiceConfigGroupSection : ConfigurationSectionGroup
    {

    }

    public class ServiceConfigSection : ConfigurationSection
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

        [ConfigurationProperty("proxyType", DefaultValue = ProxyType.Assembly, IsRequired = true)]
        public ProxyType ProxyType
        {
            get
            {
                return (ProxyType)base["proxyType"];
            }
            set
            {
                base["proxyType"] = value;
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

        [ConfigurationProperty("servicePath", IsRequired = false)]
        public String ServicePath
        {
            get
            {
                return (String)base["servicePath"];
            }
            set
            {
                base["servicePath"] = value;
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

        [ConfigurationProperty("businessServices")]
        public BusinessServiceElementCollection BusinessServices
        {
            get { return ((BusinessServiceElementCollection)(base["businessServices"])); }
            set { base["businessServices"] = value; }
        }
    }

    public class BusinessServiceElement : ConfigurationElement
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

        [ConfigurationProperty("proxyType", DefaultValue = null, IsRequired = false)]
        public ProxyType? ProxyType
        {
            get { return (ProxyType?)base["proxyType"]; }
        }

        [ConfigurationProperty("serviceAddress", IsRequired = false)]
        public String ServiceAddress
        {
            get { return (String)base["serviceAddress"]; }
        }

        [ConfigurationProperty("binding", IsRequired = false)]
        public String Binding
        {
            get { return (String)base["binding"]; }
        }

        [ConfigurationProperty("bindingConfiguration", IsRequired = false)]
        public String BindingConfiguration
        {
            get { return (String)base["bindingConfiguration"]; }
        }
    }

    [ConfigurationCollection(typeof(BusinessServiceElement))]
    public class BusinessServiceElementCollection : ConfigurationElementCollection
    {
        internal const string PropertyName = "businessService";

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
            return new BusinessServiceElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((BusinessServiceElement)(element)).ContractType;
        }

        public new BusinessServiceElement this[String name]
        {
            get { return (BusinessServiceElement)BaseGet(name); }
        }
    }
}
