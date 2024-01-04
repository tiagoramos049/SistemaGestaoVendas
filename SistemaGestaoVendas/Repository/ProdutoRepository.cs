using Dapper;
using SistemaGestaoVendas.DAO;
using SistemaGestaoVendas.Interfaces;
using SistemaGestaoVendas.Models.Clientes;
using SistemaGestaoVendas.Models.Produtos;
using System.Data;

namespace SistemaGestaoVendas.Repository
{
    public class ProdutoRepository : IProduto
    {
        private readonly Dao _dao;

        public ProdutoRepository(Dao dao)
        {
            _dao = dao;
        }

        public IEnumerable<Produto> GetAll()
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Produto>("SELECT * FROM Produto");
            }
        }

        public async Task<Produto> GetById(int id)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<Produto>("SELECT * FROM Produto WHERE id = @Id", new { Id = id });
            }
        }
        
        public void Insert(Produto produto)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO Produto (nome, descricao, preco_unitario, quantidade_estoque, unidade_medida, link_foto) VALUES (@nome, @descricao, @preco_unitario, @quantidade_estoque, @unidade_medida, @link_foto)", produto);
            }
        }

        public void Update(Produto produto)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("UPDATE Produto SET nome = @Nome, descricao = @descricao, preco_unitario = @preco_unitario, unidade_medida = @unidade_medida, link_foto = @link_foto WHERE id = @Id", produto);
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
