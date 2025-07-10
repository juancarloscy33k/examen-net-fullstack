using Microsoft.Data.SqlClient;

namespace Examen.NET.Data
{
    public class SqlConn
    {
        private readonly string _ConnectionString;

        public SqlConn(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_ConnectionString);
        }
    
    }
}
