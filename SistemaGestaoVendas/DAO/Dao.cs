using System.Data;
using System.Data.SqlClient;

namespace SistemaGestaoVendas.DAO
{
    public class Dao
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        
        public Dao(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }
    }
}
