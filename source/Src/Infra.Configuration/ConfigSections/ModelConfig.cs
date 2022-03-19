using System;
using System.Configuration;

namespace DotFramework.Infra.Configuration
{
    public class ModelConfigGroupSection : ConfigurationSectionGroup
    {

    }

    public class ModelConfigSection : ConfigurationSection
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

        [ConfigurationProperty("cacheDllPath", IsRequired = true)]
        public String CacheDllPath
        {
            get
            {
                return (String)base["cacheDllPath"];
            }
            set
            {
                base["cacheDllPath"] = value;
            }
        }

        [ConfigurationProperty("cacheType", IsRequired = true)]
        public String CacheType
        {
            get
            {
                return (String)base["cacheType"];
            }
            set
            {
                base["cacheType"] = value;
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

        [ConfigurationProperty("models")]
        public ModelElementCollection Models
        {
            get { return ((ModelElementCollection)(base["models"])); }
            set { base["models"] = value; }
        }
    }

    public class ModelElement : ConfigurationElement
    {
        [ConfigurationProperty("type", IsRequired = true, IsKey = true)]
        public String Type
        {
            get { return (String)base["type"]; }
        }

        [ConfigurationProperty("collectionType", IsRequired = true)]
        public String CollectionType
        {
            get { return (String)base["collectionType"]; }
        }

        [ConfigurationProperty("allowCache", DefaultValue = false, IsRequired = true)]
        public Boolean AllowCache
        {
            get { return (Boolean)base["allowCache"]; }
        }
    }

    [ConfigurationCollection(typeof(ModelElement))]
    public class ModelElementCollection : ConfigurationElementCollection
    {
        internal const string PropertyName = "model";

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
            return new ModelElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ModelElement)(element)).Type;
        }

        public new ModelElement this[String name]
        {
            get { return (ModelElement)BaseGet(name); }
        }
    }
}
