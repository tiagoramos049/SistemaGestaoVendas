using Dapper;
using SistemaGestaoVendas.DAO;
using SistemaGestaoVendas.Interfaces;
using SistemaGestaoVendas.Models.Produtos;
using SistemaGestaoVendas.Models.Vendedores;
using System.Data;

namespace SistemaGestaoVendas.Repository
{
    public class VendedorRepository : IVendedor
    {
        private readonly Dao _dao;
        public VendedorRepository(Dao dao)
        {
            _dao = dao;
        }
        public IEnumerable<Vendedor> GetAll()
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Vendedor>("SELECT * FROM Vendedor");
            }
        }

        public async Task<Vendedor> GetById(int id)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<Vendedor>("SELECT * FROM Vendedor WHERE id = @Id", new { Id = id });
            }
        }

        public void Insert(Vendedor vendedor)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO Vendedor (nome, cpf_cnpj, email, senha) VALUES (@Nome, @CPF_CNPJ, @Email, @Senha)", vendedor);
            }
        }

        public void Update(Vendedor vendedor)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("UPDATE Produto SET nome = @Nome, cpf_cnpj = @CPF_CNPJ, email = @Email, senha = @Senha WHERE id = @Id", vendedor);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM Produto WHERE Id = @Id", new { Id = id });
            }
        }
    }
}
