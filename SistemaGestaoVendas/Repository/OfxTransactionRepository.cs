using Dapper;
using SistemaGestaoVendas.DAO;
using SistemaGestaoVendas.Interfaces;
using SistemaGestaoVendas.Models;
using SistemaGestaoVendas.Models.ContasAPagarrs;
using System.Data;

namespace SistemaGestaoVendas.Repository
{
    public class OfxTransactionRepository : IConciliacaoContas
    {
        private readonly Dao _dao;
        public OfxTransactionRepository(Dao dao)
        {
            _dao = dao;
        }
        public bool Exists(string fitId)
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                string query = "SELECT COUNT(1) FROM OFXData WHERE FITID = @FitId";
                var count = dbConnection.ExecuteScalar<int>(query, new { FitId = fitId });
                return count > 0;
            }
        }
        public IEnumerable<OfxTransaction> GetAll()
        {
            using (IDbConnection dbConnection = _dao.Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<OfxTransaction>("SELECT * FROM OFXData");
            }
        }
        public void Insert(OfxTransaction? ofxTransaction)
        {
            // Verifica se o FITID já existe antes de inserir
            if (!Exists(ofxTransaction.FITID))
            {
                using (IDbConnection dbConnection = _dao.Connection)
                {
                    dbConnection.Open();
                    dbConnection.Execute("INSERT INTO OFXData(OFXHEADER,DATA,VERSION,SECURITY,ENCODING,CHARSET,COMPRESSION,OLDFILEUID,NEWFILEUID,DTSERVER,LANGUAGE,DTACCTUP,ORG,FID,CURDEF,BANKID,ACCTID,ACCTTYPE,DTSTART,DTEND,TRNTYPE,DTPOSTED,TRNAMT,FITID,CHECKNUM,MEMO,BALAMT,DTASOF,MKTGINFO) VALUES (@OFXHEADER,@DATA,@VERSION,@SECURITY,@ENCODING,@CHARSET,@COMPRESSION,@OLDFILEUID,@NEWFILEUID,@DTSERVER,@LANGUAGE,@DTACCTUP,@ORG,@FID,@CURDEF,@BANKID,@ACCTID,@ACCTTYPE,@DTSTART,@DTEND,@TRNTYPE,@DTPOSTED,@TRNAMT,@FITID,@CHECKNUM,@MEMO,@BALAMT,@DTASOF,@MKTGINFO)", ofxTransaction);
                }
            }
            else
            {
                Console.WriteLine($"A transação com FITID {ofxTransaction.FITID} já existe no banco de dados.");
            }
        }
    }
}
