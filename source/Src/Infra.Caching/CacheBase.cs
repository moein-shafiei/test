using DotFramework.Core;
using DotFramework.Infra.Configuration;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace DotFramework.Infra.Caching
{
    public class CacheBase<TCache> : SingletonProvider<TCache>
        where TCache : class
    {
        private static readonly object padlock = new object();
        protected ModelConfigSection ModelSection { get; set; }

        private IUnityContainer _Container;
        protected IUnityContainer Container
        {
            get
            {
                if (_Container == null)
                {
                    lock (padlock)
                    {
                        if (_Container == null)
                        {
                            InitializeContainer();
                        }
                    }
                }

                return _Container;
            }
        }

        public void CreateInstance(string sectionName)
        {
            ModelSection = (ModelConfigSection)ConfigurationManager.GetSection(sectionName);
        }

        protected virtual void RegisterCache(Object instance)
        {
            Container.RegisterType(instance.GetType(),
                                        new ContainerControlledLifetimeManager(),
                                        new InjectionFactory((c) => instance));
        }

        public bool IsAllowedCache<T>()
        {
            return Container.IsRegistered<T>();
        }

        public T GetCache<T>()
        {
            return Container.Resolve<T>();
        }

        protected virtual void InitializeContainer()
        {
            _Container = new UnityContainer();

            if (ModelSection != null)
            {
                foreach (ModelElement model in ModelSection.Models.Cast<ModelElement>().Where(d => d.AllowCache))
                {
                    string typeName = model.CollectionType;
                    Assembly assembly = Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ModelSection.DllPath));

                    Object temp = assembly.CreateInstance(typeName);
                    RegisterCache(temp);
                }
            }
        }
    }
}
