using EFT.Core.Repositories.Dapper;
using System.Data;
using System.Data.Common;
namespace EFT.Infrastructure.Repositories.Dapper.Helper
{
    public class SqlDapperHelper : DapperHelper
    {
        protected override IDbConnection CreateConnection()
        {
            var connectionString = Properties.Settings.Default.eftClearPointEsubeDb;

            var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
            var connection = factory.CreateConnection();
            connection.ConnectionString = connectionString;
            return connection;
        }
    }
}
