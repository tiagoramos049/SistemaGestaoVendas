using SistemaGestaoVendas.Models;
using SistemaGestaoVendas.Models.ContasAPagarrs;
using SistemaGestaoVendas.Models.Vendedores;

namespace SistemaGestaoVendas.Interfaces
{
    public interface IConciliacaoContas
    {
        public IEnumerable<OfxTransaction> GetAll();
        public void Insert(OfxTransaction? ofxTransaction);
    }
}
