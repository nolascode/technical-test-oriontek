using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Repository.Context
{
    public class DatabaseContext
    {
        private readonly IConfiguration _configuration;

        public DatabaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
            Console.WriteLine(_configuration.GetConnectionString("MysqlConectionString"));
        }
        public MySqlConnection GetMySqlConnetion()
        {
            var conn = new MySqlConnection(_configuration.GetConnectionString("MysqlConectionString"));
            if(conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }
    }
}
