using Dapper;
using SistemaGestaoVendas.DAO;
using SistemaGestaoVendas.Interfaces;
using SistemaGestaoVendas.Models.Checklist;
using System.Collections.Generic;
using System.Data;

namespace SistemaGestaoVendas.Repository
{
    public class CheckListRepository : ICheckList
    {
        private readonly Dao _dao;
        public CheckListRepository(Dao dao)
        {
            _dao = dao;
        }

        public IEnumerable<CheckList> GetAll()
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                var sql = @"
                SELECT 
	                case
		                when m.Estoque = 1 then 'Sim'
		                when m.Estoque = 0 then 'não' end as QuantidadeEstoque, c.*,  m.Nome as NomeMoto  
                FROM Checklist c
                INNER JOIN Moto m ON c.IdMotos = m.IdMotos";
                return dbConnection.Query<CheckList>(sql);
            }
        }

        public CheckList GetById(int id)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                return dbConnection.QueryFirstOrDefault<CheckList>("SELECT * FROM Checklist WHERE IdMotos = @Id", new { Id = id });
            }
        }

        public void Insert(CheckList checkList)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                dbConnection.Execute(@"INSERT INTO Checklist 
                    (IdMotos, ChecarDebitos, ChecarAlinhamento, ChecarCarenagem, ChecarRodasPneus, ChecarVazamentos, ChecarManualChave, ChecarPainel, ChecarEletrica, ChecarEmbreagem, ChecarCaixaDirecao, ChecarMotor, ChecarFumacaDoMotor, ChecarRolamentoDisco, ChecarChassi) 
                    VALUES (@IdMotos, @ChecarDebitos, @ChecarAlinhamento, @ChecarCarenagem, @ChecarRodasPneus, @ChecarVazamentos, @ChecarManualChave, @ChecarPainel, @ChecarEletrica, @ChecarEmbreagem, @ChecarCaixaDirecao, @ChecarMotor, @ChecarFumacaDoMotor, @ChecarRolamentoDisco, @ChecarChassi)", checkList);
            }
        }

        public void Update(CheckList checkList)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                dbConnection.Execute(@"UPDATE Checklist SET 
                    ChecarDebitos = @ChecarDebitos, 
                    ChecarAlinhamento = @ChecarAlinhamento, 
                    ChecarCarenagem = @ChecarCarenagem, 
                    ChecarRodasPneus = @ChecarRodasPneus, 
                    ChecarVazamentos = @ChecarVazamentos, 
                    ChecarManualChave = @ChecarManualChave, 
                    ChecarPainel = @ChecarPainel, 
                    ChecarEletrica = @ChecarEletrica, 
                    ChecarEmbreagem = @ChecarEmbreagem, 
                    ChecarCaixaDirecao = @ChecarCaixaDirecao, 
                    ChecarMotor = @ChecarMotor, 
                    ChecarFumacaDoMotor = @ChecarFumacaDoMotor, 
                    ChecarRolamentoDisco = @ChecarRolamentoDisco, 
                    ChecarChassi = @ChecarChassi 
                    WHERE IdMotos = @IdMotos", checkList);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM Checklist WHERE IdMotos = @IdMotos", new { IdMotos = id });
            }
        }
    }
}