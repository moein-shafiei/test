using DotFramework.Core;
using DotFramework.Core.DependencyInjection;
using DotFramework.Infra.Configuration;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace DotFramework.Infra.DataAccessFactory
{
    public class DataAccessFactoryBase<TDataAccessFacory> : InterfaceFactoryBase<TDataAccessFacory, IDataAccessBase, DataAccessInterceptionBehavior> where TDataAccessFacory : class
    {
        protected DataAccessConfigSection DataAccessSection { get; set; }

        public void Configure(string sectionName)
        {
            try
            {
                DataAccessSection = (DataAccessConfigSection)ConfigurationManager.GetSection(sectionName);
            }
            catch (Exception ex)
            {
                if (DataAccessFactoryExceptionHandler.Instance.HandleException(ref ex))
                {
                    throw ex;
                }
            }
        }

        public IDataAccess GetDataAccess<IDataAccess>() where IDataAccess : IDataAccessBase
        {
            return Resolve<IDataAccess>();
        }

        public virtual void ChangeConnection(string connectionName, string connectionString, bool isEncrypted = false)
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            ((ConnectionConfigSection)configuration.GetSection("connectionConfigSection")).Connections[connectionName].ConnectionString = connectionString;
            ((ConnectionConfigSection)configuration.GetSection("connectionConfigSection")).Connections[connectionName].IsEncrypted = isEncrypted;

            configuration.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("connectionConfigSection");
        }

        protected override IDataAccess CreateType<IDataAccess>()
        {
            DataAccessServiceElement dataAccessElement = DataAccessSection.DataAccessServices[typeof(IDataAccess).FullName];
            ConnectionElement connectionElement = dataAccessElement.Connection != null ? dataAccessElement.Connection : DataAccessSection.Connection;

            string connectionString = String.Empty;

            if (connectionElement.IsEncrypted)
            {
                connectionString = EncryptUtility.DecryptText(connectionElement.ConnectionString);
            }
            else
            {
                connectionString = connectionElement.ConnectionString;
            }

            string typeName = dataAccessElement.ServiceType;
            IDataAccess dataAccess = default(IDataAccess);

            Assembly assembly = Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DataAccessSection.DllPath));

            dataAccess = (IDataAccess)assembly.CreateInstance(typeName);
            dataAccess.SetConnectionString(connectionString);
            return dataAccess;
        }

        protected override bool HandleException(ref Exception ex)
        {
            return DataAccessFactoryExceptionHandler.Instance.HandleException(ref ex);
        }
    }
}
