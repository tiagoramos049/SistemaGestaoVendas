using Dapper;
using SistemaGestaoVendas.DAO;
using SistemaGestaoVendas.Interfaces;
using SistemaGestaoVendas.Models.ContasAPagar;
using SistemaGestaoVendas.Models.Produtos;
using System.Data;

namespace SistemaGestaoVendas.Repository
{


    public class ContasAPagarRepository : IContasAPagar
    {
        private readonly Dao _dao;
        public ContasAPagarRepository(Dao dao)
        {
            _dao = dao;
        }
        public IEnumerable<ContasAPagar> GetAll()
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<ContasAPagar>("SELECT * FROM ContasAPagar");
            }
        }
        
        public ContasAPagar GetById(int id)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<ContasAPagar>("SELECT * FROM ContasAPagar WHERE id = @Id", new { Id = id });
            }
        }

        public void Insert(ContasAPagar contasAPagar)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO ContasAPagar (DataEmissao, DataVencimento, Favorecido, Valor, FormaPagamento, Banco) VALUES (@DataEmissao, @DataVencimento, @Favorecido, @Valor, @FormaPagamento, @Banco)", contasAPagar);
            }
        }

        public void Update(ContasAPagar contasAPagar)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("UPDATE ContasAPagar SET DataEmissao = @DataEmissao, DataVencimento = @DataVencimento, Favorecido = @Favorecido, Valor = @Valor, FormaPagamento = @FormaPagamento, Banco = @Banco WHERE id = @Id", contasAPagar);
            }
        }
        public void Delete(int id)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM ContasAPagar WHERE Id = @Id", new { Id = id });
            }
        }
    }
}
