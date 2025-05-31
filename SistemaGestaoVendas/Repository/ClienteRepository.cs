using Dapper;
using SistemaGestaoVendas.DAO;
using SistemaGestaoVendas.Interfaces;
using SistemaGestaoVendas.Models.Clientes;
using System.Data;

namespace SistemaGestaoVendas.Repository
{
    public class ClienteRepository : ICliente
    {
        private readonly Dao _dao;
        public ClienteRepository(Dao dao)
        {
            _dao = dao;
        }
        public IEnumerable<Cliente> GetAll()
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Cliente>("SELECT * FROM Cliente");
            }
        }

        public Cliente GetById(int id)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<Cliente>("SELECT * FROM Cliente WHERE IdCliente = @Id", new { Id = id });
            }
        }

        public void Insert(Cliente cliente)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO Cliente (Nome, Telefone, CpfCnpj, Observacoes) VALUES (@Nome, @Telefone, @CpfCnpj, @Observacoes)", cliente);
            }
        }

        public void Update(Cliente cliente)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("UPDATE Cliente SET Nome = @Nome, Telefone = @Telefone, CpfCnpj = @CpfCnpj, Observacoes = @Observacoes WHERE IdCliente = @IdCliente", cliente);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM Cliente WHERE IdCliente = @IdCliente", new { IdCliente = id });
            }
        }
        
    }
}
