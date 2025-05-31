using Dapper;
using Microsoft.AspNetCore.Mvc;
using SistemaGestaoVendas.DAO;
using SistemaGestaoVendas.Interfaces;
using SistemaGestaoVendas.Models.Clientes;
using SistemaGestaoVendas.Models.Motos;
using SistemaGestaoVendas.Models.Vendas;
using System.Data;

namespace SistemaGestaoVendas.Repository
{
    public class MotoRepository : IMoto
    {
        private readonly Dao _dao;

        public MotoRepository(Dao dao)
        {
            _dao = dao;
        }

        public IEnumerable<Moto> GetAll()
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Moto>("SELECT * FROM Moto WHERE Estoque = 1");
            }
        }

        public IEnumerable<Venda> GetAllVendas()
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                string sql = @"SELECT m.Nome, v.DataVenda, c.Nome AS NomeCliente, m.ValorVenda 
                               FROM Moto m 
                               INNER JOIN Venda v ON m.IdMotos = v.IdMotos
                               INNER JOIN Cliente c ON v.IdCliente = c.IdCliente";

                return dbConnection.Query<Venda>(sql);
            }
        }

        public Moto GetById(int id)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<Moto>("SELECT * FROM Moto WHERE IdMotos = @Id", new { Id = id });
            }
        }

        public int Insert(Moto moto)
        {
            moto.DataVenda = DateTime.Now;  // Defina a data atual para o campo DataVenda

            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        string sqlMoto = @"INSERT INTO Moto (Nome, Placa, Marca, Fabricacao, Crv, Cor, Chassi, Cilindrada, ValorVenda, ValorCompra, DataVenda, DataCompra, Observacoes, Regiao, IdCliente, Estoque) 
                           VALUES (@Nome, @Placa, @Marca, @Fabricacao, @Crv, @Cor, @Chassi, @Cilindrada, @ValorVenda, @ValorCompra, @DataVenda, @DataCompra, @Observacoes, @Regiao, 1, 1);
                           SELECT CAST(SCOPE_IDENTITY() as int)";

                        int motoId = dbConnection.Query<int>(sqlMoto, moto, transaction).Single();

                        string sqlChecklist = @"INSERT INTO Checklist (IdMotos, ChecarDebitos, ChecarAlinhamento, ChecarCarenagem, ChecarRodasPneus, ChecarVazamentos, ChecarManualChave, ChecarPainel, ChecarEletrica, ChecarEmbreagem, ChecarCaixaDirecao, ChecarMotor, ChecarFumacaDoMotor, ChecarRolamentoDisco, ChecarChassi) 
                                VALUES (@IdMotos, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)";

                        var checklist = new
                        {
                            IdMotos = motoId
                        };

                        dbConnection.Execute(sqlChecklist, checklist, transaction);

                        transaction.Commit();

                        return motoId;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Erro ao inserir a moto e o checklist: " + ex.Message);
                    }
                }
            }
        }
        public void Update(Moto moto)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                string sql = @"UPDATE Moto 
                               SET Nome = @Nome, Placa = @Placa, Marca = @Marca, Fabricacao = @Fabricacao, Crv = @Crv, Cor = @Cor, Chassi = @Chassi, Cilindrada = @Cilindrada, ValorVenda = @ValorVenda, ValorCompra = @ValorCompra, DataVenda = @DataVenda, DataCompra = @DataCompra, Observacoes = @Observacoes, Regiao = @Regiao, IdCliente = 1, Estoque = 1 
                               WHERE IdMotos = @IdMotos";
                dbConnection.Execute(sql, moto);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM Moto WHERE IdMotos = @IdMotos", new { IdMotos = id });
            }
        }

        public void AtualizarEstoqueERegistrarVenda(int idMotos, int idCliente)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        // Verificar se o clienteId existe na tabela Cliente
                        var clienteExiste = dbConnection.ExecuteScalar<bool>(
                            "SELECT COUNT(1) FROM Cliente WHERE IdCliente = @idCliente",
                            new { idCliente },
                            transaction
                        );

                        if (!clienteExiste)
                        {
                            throw new Exception("Cliente não encontrado.");
                        }

                        // Atualizar o estoque da moto específica
                        dbConnection.Execute(
                            "UPDATE Moto SET Estoque = 0 WHERE IdMotos = @idMotos",
                            new { idMotos },
                            transaction
                        );

                        // Inserir um registro de venda
                        dbConnection.Execute(
                            "INSERT INTO Venda (IdMotos, DataVenda, IdCliente) VALUES (@idMotos, @DataVenda, @idCliente)",
                            new { idMotos, DataVenda = DateTime.Now, idCliente },
                            transaction
                        );

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public IEnumerable<Cliente> ObterTodosClientes()
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Cliente>("SELECT IdCliente, Nome FROM Cliente").ToList();
            }
        }
    }
}