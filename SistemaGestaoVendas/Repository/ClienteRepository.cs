using Dapper;
using SistemaGestaoVendas.DAO;
using SistemaGestaoVendas.Interfaces;
using SistemaGestaoVendas.Models.Clientes;
using SistemaGestaoVendas.Models.Vendedores;
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

        public async Task<Cliente> GetById(int id)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<Cliente>("SELECT * FROM Cliente WHERE id = @Id", new { Id = id });
            }
        }

        public void Insert(Cliente cliente)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO Cliente (nome, cpf_cnpj, email, senha) VALUES (@Nome, @CPF_CNPJ, @Email, @Senha)", cliente);
            }
        }

        public void Update(Cliente cliente)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("UPDATE Cliente SET nome = @Nome, cpf_cnpj = @CPF_CNPJ, email = @Email, senha = @Senha WHERE id = @Id", cliente);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM Cliente WHERE Id = @Id", new { Id = id });
            }
        }
    }
}
