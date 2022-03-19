using DotFramework.Core;
using System;
using Unity;
using Unity.Interception;
using Unity.Interception.ContainerIntegration;
using Unity.Interception.Interceptors.TypeInterceptors.VirtualMethodInterception;
using Unity.Lifetime;

namespace DotFramework.Infra.BusinessRules
{
    public class BusinessRulesFactory : SingletonProvider<BusinessRulesFactory>
    {
        private static readonly object padlock = new object();

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

        public TRules GetBusinessRules<TRules>() where TRules : RulesBase, new()
        {
            try
            {
                lock (padlock)
                {
                    if (!Container.IsRegistered<TRules>())
                    {
                        RegisterType<TRules>();
                    }

                    return Container.Resolve<TRules>();
                }
            }
            catch (Exception ex)
            {
                if (BusinessRulesFactoryExceptionHandler.Instance.HandleException(ref ex))
                {
                    throw ex;
                }

                return default(TRules);
            }
        }

        protected virtual void RegisterType<TRules>()
        {
            Container.RegisterType<TRules>(new ContainerControlledLifetimeManager(),
                                           new Interceptor<VirtualMethodInterceptor>(),
                                           new InterceptionBehavior<BusinessRulesInterceptionBehavior>());
        }

        protected virtual void InitializeContainer()
        {
            _Container = new UnityContainer();
            _Container.AddNewExtension<Interception>();
        }
    }
}
