using DotFramework.Core.DependencyInjection;
using DotFramework.Infra.Configuration;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using Unity.Injection;
using Unity.Interception.InterceptionBehaviors;
using Unity.Lifetime;
using Unity;

namespace DotFramework.Infra.ServiceFactory
{
    public abstract class ServiceFactoryBase<TServiceFacory, TInterceptionBehavior, TEndpointBehavior> : InterfaceFactoryBase<TServiceFacory, IServiceBase, TInterceptionBehavior>
        where TServiceFacory : class
        where TInterceptionBehavior : IInterceptionBehavior
        where TEndpointBehavior : IEndpointBehavior, new()
    {
        protected ServiceConfigSection ServiceSection { get; set; }

        public void Configure(string sectionName)
        {
            try
            {
                ServiceSection = (ServiceConfigSection)ConfigurationManager.GetSection(sectionName);
            }
            catch (Exception ex)
            {
                if (ServiceFactoryExceptionHandler.Instance.HandleException(ref ex))
                {
                    throw ex;
                }
            }
        }

        public virtual IService GetService<IService>() where IService : IServiceBase
        {
            return Resolve<IService>();
        }

        protected override void Register<TType>()
        {
            CreateType<TType>();
        }

        protected override TType CreateType<TType>()
        {
            BusinessServiceElement serviceElement = ServiceSection.BusinessServices[typeof(TType).FullName];
            ProxyType proxyType = serviceElement.ProxyType ?? ServiceSection.ProxyType;

            TType service = default(TType);

            switch (proxyType)
            {
                case ProxyType.Assembly:
                    service = CreateAssemblyProxy<TType>(serviceElement.ServiceType, ServiceSection.DllPath);
                    break;
                case ProxyType.WCF:
                    service = CreateWCFProxy<TType>(serviceElement.Binding, serviceElement.BindingConfiguration, ServiceSection.ServicePath, serviceElement.ServiceAddress);
                    break;
                case ProxyType.API:
                    service = CreateAPIProxy<TType>(serviceElement.ServiceType, ServiceSection.DllPath, ServiceSection.ServicePath, serviceElement.ServiceAddress);
                    break;
                default:
                    throw new NotSupportedException("Not Supported Proxy Type");
            }

            return service;
        }

        protected override bool HandleException(ref Exception ex)
        {
            return ServiceFactoryExceptionHandler.Instance.HandleException(ref ex);
        }

        #region Create Proxy

        private TType CreateAssemblyProxy<TType>(string serviceType, string dllPath) where TType : IServiceBase
        {
            Assembly assembly = Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dllPath));
            TType service = (TType)assembly.CreateInstance(serviceType);

            RegisterType(service);
            return service;
        }

        private TType CreateWCFProxy<TType>(string binding, string bindingConfiguration, string servicePath, string serviceAddress) where TType : IServiceBase
        {
            ChannelFactory<TType> channel = null;

            switch (binding)
            {
                case "wsHttpBinding":
                    //channel = new ChannelFactory<TType>(new WSHttpBinding(bindingConfiguration), servicePath + serviceAddress);
                    break;
                case "basicHttpBinding":
                    var BasicHttpBinding = new BasicHttpBinding();
                    BasicHttpBinding.Name = bindingConfiguration;
                    channel = new ChannelFactory<TType>(BasicHttpBinding, new EndpointAddress(servicePath + serviceAddress));
                    break;
                default:
                    break;
            }

            TType service = RegisterChannel(channel);
            SimpleRegisterType(service);

            return service;
        }

        private TType CreateAPIProxy<TType>(string serviceType, string dllPath, string servicePath, string serviceAddress) where TType : IServiceBase
        {
            Assembly assembly = Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dllPath));
            var type = assembly.GetType(serviceType);
            TType service = (TType)Activator.CreateInstance(type, servicePath, serviceAddress);

            RegisterType(service);
            return service;
        }

        #endregion

        #region Register Channel

        protected IService RegisterChannel<IService>(ChannelFactory<IService> channel)
        {
            RegisterEndpointBehaviors(channel);
            IService service = channel.CreateChannel();

            ((ICommunicationObject)service).Faulted += channel_Faulted<IService>;
            ((ICommunicationObject)service).Closed += channel_Closed<IService>;

            Container.RegisterType<ChannelFactory<IService>>(new ContainerControlledLifetimeManager(),
                                                             new InjectionFactory((c) => channel));

            return service;
        }

        private void channel_Faulted<IService>(object sender, EventArgs e)
        {
            ((ICommunicationObject)sender).Abort();

            ChannelFactory<IService> channel = Container.Resolve<ChannelFactory<IService>>();
            RegisterChannel(channel);
        }

        private void channel_Closed<IService>(object sender, EventArgs e)
        {

        }

        private void RegisterEndpointBehaviors<IService>(ChannelFactory<IService> channel)
        {
            if (!channel.Endpoint.EndpointBehaviors.Contains(typeof(CustomEndpointBehavior)))
            {
                channel.Endpoint.EndpointBehaviors.Add(new TEndpointBehavior());
            }
        }

        #endregion
    }

    public class ServiceFactoryBase<TServiceFacory> : ServiceFactoryBase<TServiceFacory, ServiceInterceptionBehavior, CustomEndpointBehavior>
        where TServiceFacory : class
    {

    }
}
