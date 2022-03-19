using DotFramework.Core;
using System;
using System.IO;
using System.Reflection;

namespace DotFramework.Infra.Configuration
{
    public static class ApplicationConfigurator
    {
        #region Properties

        public static DatabaseConfigGroupSection DatabaseConfig { get; set; } 

        #endregion

        public static void Configure(System.Configuration.Configuration config)
        {
            ModelConfigGroupSection modelConfig = (ModelConfigGroupSection)config.GetSectionGroup("modelConfigSections");
            ServiceConfigGroupSection serviceConfig = (ServiceConfigGroupSection)config.GetSectionGroup("serviceConfigSections");
            DataAccessConfigGroupSection dataAccessConfig = (DataAccessConfigGroupSection)config.GetSectionGroup("dataAccessConfigSections");

            if (modelConfig != null)
            {
                foreach (ModelConfigSection section in modelConfig.Sections)
                {
                    Assembly assembly = Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, section.CacheDllPath));
                    object temp = Activator.CreateInstance(assembly.GetType(section.CacheType), true);

                    Type singletonProviderType = GetSingletonProviderType(temp.GetType());

                    object instance = singletonProviderType.GetProperty("Instance").GetGetMethod().Invoke(null, null);
                    instance.GetType().GetMethod("CreateInstance", new Type[] { typeof(String) }).Invoke(instance, new object[] { section.SectionInformation.SectionName });
                }
            }

            if (serviceConfig != null)
            {
                foreach (ServiceConfigSection section in serviceConfig.Sections)
                {
                    Assembly assembly = Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, section.FactoryDllPath));
                    object temp = Activator.CreateInstance(assembly.GetType(section.FactoryType), true);

                    Type singletonProviderType = GetSingletonProviderType(temp.GetType());

                    object instance = singletonProviderType.GetProperty("Instance").GetGetMethod().Invoke(null, null);
                    instance.GetType().GetMethod("Configure", new Type[] { typeof(String) }).Invoke(instance, new object[] { section.SectionInformation.SectionName });
                }
            }

            if (dataAccessConfig != null)
            {
                foreach (DataAccessConfigSection section in dataAccessConfig.Sections)
                {
                    Assembly assembly = Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, section.FactoryDllPath));
                    object temp = Activator.CreateInstance(assembly.GetType(section.FactoryType), true);

                    Type singletonProviderType = GetSingletonProviderType(temp.GetType());

                    object instance = singletonProviderType.GetProperty("Instance").GetGetMethod().Invoke(null, null);
                    instance.GetType().GetMethod("Configure", new Type[] { typeof(String) }).Invoke(instance, new object[] { section.SectionInformation.SectionName });
                }
            }

            DatabaseConfig = config.GetSectionGroup("databaseConfigSections") as DatabaseConfigGroupSection;
        }

        private static Type GetSingletonProviderType(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(SingletonProvider<>))
            {
                return type;
            }
            else
            {
                return GetSingletonProviderType(type.BaseType);
            }
        }
    }
} 
