using SistemaGestaoVendas.Models;
using SistemaGestaoVendas.Models.Vendedores;

namespace SistemaGestaoVendas.Interfaces
{
    public interface IConciliacaoContas
    {
        public void Insert(OfxTransaction? ofxTransaction);
    }
}
