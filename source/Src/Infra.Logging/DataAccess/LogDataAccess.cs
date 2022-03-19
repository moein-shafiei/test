using DotFramework.Infra.DataAccess;
using DotFramework.Infra.DataAccess.Sqlite;
using DotFramework.Infra.Model;
using System.Data;

namespace DotFramework.Infra.Logging
{
    internal class LogDataAccess : SqliteDataAccessBase
    {
        public LogDataAccess(string connectionString)
        {
            SetConnectionString(connectionString);
        }

        public bool Insert(LogEntryModel LogEntryModel)
        {
            string commandText = @"INSERT INTO [Log] 
                                   (
	                                   [LogGuid],
	                                   [EventID],
	                                   [Priority],
	                                   [Severity],
	                                   [Title],
	                                   [Timestamp],
	                                   [MachineName],
	                                   [AppDomainName],
	                                   [ApplicationCode],
	                                   [ClassName],
	                                   [MethodName],
	                                   [ProcessID],
	                                   [ProcessName],
	                                   [ThreadName],
	                                   [Win32ThreadId],
	                                   [Message],
	                                   [FormattedMessage],
	                                   [Status],
	                                   [ModificationTime],
	                                   [SessionID]
                                   )
                                   VALUES 
                                   (
	                                   @LogGuid,
	                                   @EventID,
	                                   @Priority,
	                                   @Severity,
	                                   @Title,
	                                   @Timestamp,
	                                   @MachineName,
	                                   @AppDomainName,
	                                   @ApplicationCode,
	                                   @ClassName,
	                                   @MethodName,
	                                   @ProcessID,
	                                   @ProcessName,
	                                   @ThreadName,
	                                   @Win32ThreadId,
	                                   @Message,
	                                   @FormattedMessage,
	                                   @Status,
	                                   @ModificationTime,
	                                   @SessionID
                                   )";

            return ExecuteNonQuery(
                commandText, 
                CommandType.Text,
                CreateParameter("@LogGuid", DbType.Guid, LogEntryModel.LogGuid.GetDbValue()),
                CreateParameter("@EventID", DbType.Int32, LogEntryModel.EventID.GetDbValue()),
                CreateParameter("@Priority", DbType.Int32, LogEntryModel.Priority.GetDbValue()),
                CreateParameter("@Severity", DbType.String, LogEntryModel.Severity.GetDbValue()),
                CreateParameter("@Title", DbType.String, LogEntryModel.Title.GetDbValue()),
                CreateParameter("@Timestamp", DbType.DateTime, LogEntryModel.Timestamp.GetDbValue()),
                CreateParameter("@MachineName", DbType.String, LogEntryModel.MachineName.GetDbValue()),
                CreateParameter("@AppDomainName", DbType.String, LogEntryModel.AppDomainName.GetDbValue()),
                CreateParameter("@ApplicationCode", DbType.String, LogEntryModel.ApplicationCode.GetDbValue()),
                CreateParameter("@ClassName", DbType.String, LogEntryModel.ClassName.GetDbValue()),
                CreateParameter("@MethodName", DbType.String, LogEntryModel.MethodName.GetDbValue()),
                CreateParameter("@ProcessID", DbType.String, LogEntryModel.ProcessID.GetDbValue()),
                CreateParameter("@ProcessName", DbType.String, LogEntryModel.ProcessName.GetDbValue()),
                CreateParameter("@ThreadName", DbType.String, LogEntryModel.ThreadName.GetDbValue()),
                CreateParameter("@Win32ThreadId", DbType.String, LogEntryModel.Win32ThreadId.GetDbValue()),
                CreateParameter("@Message", DbType.String, LogEntryModel.Message.GetDbValue()),
                CreateParameter("@FormattedMessage", DbType.String, LogEntryModel.FormattedMessage.GetDbValue()),
                CreateParameter("@Status", DbType.Int16, 0),
                CreateParameter("@ModificationTime", DbType.DateTime, LogEntryModel.ModificationTime),
                CreateParameter("@SessionID", DbType.Int64, LogEntryModel.SessionID.GetDbValue()));
        }
    }
}
