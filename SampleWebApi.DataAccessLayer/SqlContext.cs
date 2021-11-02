using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace SampleWebApi.DataAccessLayer
{
    public class SqlContext : ISqlContext
    {
        private readonly string connectionString;

        private IDbConnection connection;
        public IDbConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    connection = new SqlConnection(connectionString);
                }

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                return connection;
            }
        }

        //public SqlContext(IConfiguration configuration)
        //{
        //    connectionString = configuration.GetConnectionString("SqlConnection");
        //}

        public SqlContext(string connectionString, ILogger<SqlContext> logger)
        {
            this.connectionString = connectionString;
        }

        public void Dispose()
        {
            if (connection?.State == ConnectionState.Open)
            {
                connection.Close();
            }

            connection?.Dispose();
            connection = null;
        }
    }
}
