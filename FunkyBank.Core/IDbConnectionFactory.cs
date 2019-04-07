using System.Data;

namespace FunkyBank.Core
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetConnection(string connectionString);
    }
}