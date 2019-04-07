using System.Data;
using System.Data.SqlClient;
using FunkyBank.Core;

namespace FunkyBank.DataAccess.Dapper.Repositories
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        public IDbConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}