using Dapper;
using SistemaGestaoVendas.DAO;
using System.Data;

namespace SistemaGestaoVendas.Repository
{
    public class DashboardRepository
    {
        private readonly Dao _dao;
        public DashboardRepository(Dao dao)
        {
            _dao = dao;
        }
        public decimal GetTotalContasAPagar()
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<decimal>("SELECT SUM(Valor) FROM ContasAPagar").FirstOrDefault();
            }
        }

        public decimal GetTotalContasAReceber()
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<decimal>("SELECT SUM(Valor) FROM ContasAReceber").FirstOrDefault();
            }
        }

    }
}
