﻿using Dapper;
using SistemaGestaoVendas.DAO;
using SistemaGestaoVendas.Interfaces;
using SistemaGestaoVendas.Models.ContasAPagar;
using SistemaGestaoVendas.Models.ContasAReceber;
using System.Data;

namespace SistemaGestaoVendas.Repository
{
    public class ContasAReceberRepository : IContasAReceber
    {
        private readonly Dao _dao;
        public ContasAReceberRepository(Dao dao)
        {
            _dao = dao;
        }
        public IEnumerable<ContasAReceber> GetAll()
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<ContasAReceber>("SELECT * FROM ContasAReceber");
            }
        }

        public ContasAReceber GetById(int id)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<ContasAReceber>("SELECT * FROM ContasAReceber WHERE id = @Id", new { Id = id });
            }
        }

        public void Insert(ContasAReceber contasAReceber)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO ContasAReceber (DataEmissao, DataVencimento, Favorecido, Valor, FormaPagamento, Banco) VALUES (@DataEmissao, @DataVencimento, @Favorecido, @Valor, @FormaPagamento, @Banco)", contasAReceber);
            }
        }

        public void Update(ContasAReceber contasAReceber)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("UPDATE ContasAReceber SET DataEmissao = @DataEmissao, DataVencimento = @DataVencimento, Favorecido = @Favorecido, Valor = @Valor, FormaPagamento = @FormaPagamento, Banco = @Banco WHERE id = @Id", contasAReceber);
            }
        }
        public void Delete(int id)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM ContasAReceber WHERE Id = @Id", new { Id = id });
            }
        }
    }
}