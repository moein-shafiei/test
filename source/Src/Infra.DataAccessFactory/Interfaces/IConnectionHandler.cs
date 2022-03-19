using System.Data.Common;

namespace DotFramework.Infra.DataAccessFactory
{
    public interface IConnectionHandler
    {
        string ConnectionString { get; set; }
        DbConnection Connection { get; }
    }
}
