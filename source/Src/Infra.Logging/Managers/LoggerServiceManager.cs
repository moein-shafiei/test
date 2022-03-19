using DotFramework.Core;
using DotFramework.Infra.Model;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Transactions;

namespace DotFramework.Infra.Logging
{
    internal class LoggerServiceManager : SingletonProvider<LoggerServiceManager>
    {
        #region Constructor

        private LoggerServiceManager()
        {
            _SQLiteDatabaseDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _LogsDirectory);
            _SQLiteDatabaseFilePath = Path.Combine(_SQLiteDatabaseDirectoryPath, _SQLiteDatabaseName);

            _SQLiteConnectionString = String.Format("data source={0};", _SQLiteDatabaseFilePath);
        }

        #endregion

        #region Variables

        private const string _LogsDirectory = "Logs";
        private const string _SQLiteDatabaseName = "application.log.sqlite3";

        private readonly string _SQLiteDatabaseDirectoryPath;
        private readonly string _SQLiteDatabaseFilePath;
        private readonly string _SQLiteConnectionString;

        #endregion

        #region Properties

        private string _WriteLogEndpoint;
        public string WriteLogEndpoint
        {
            get { return _WriteLogEndpoint; }
        }

        #endregion

        #region Public Methods

        public void Initialize(string writeLogEndpoint)
        {
            _WriteLogEndpoint = writeLogEndpoint;

            if (!File.Exists(_SQLiteDatabaseFilePath))
            {
                Directory.CreateDirectory(_SQLiteDatabaseDirectoryPath);
                File.WriteAllBytes(_SQLiteDatabaseFilePath, Properties.Resources.LoggingDatabase);
            }
        }

        public void WriteLog(LogEntryModel model)
        {
            Task.Run(() =>
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Suppress))
                {
                    LogDataAccess dataAccess = new LogDataAccess(_SQLiteConnectionString);
                    dataAccess.Insert(model);
                }
            });
        }

        #endregion
    }
}
