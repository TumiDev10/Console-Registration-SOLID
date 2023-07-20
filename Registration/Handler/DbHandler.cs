using System.Data.SqlClient;


namespace Registration.Handler
{
    public class DbHandler
    {
        private string connectionString;

        public DbHandler(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
