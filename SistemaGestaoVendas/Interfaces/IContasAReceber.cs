using SistemaGestaoVendas.Models.ContasAPagar;
using SistemaGestaoVendas.Models.ContasAReceber;

namespace SistemaGestaoVendas.Interfaces
{
    public interface IContasAReceber
    {
        public IEnumerable<ContasAReceber> GetAll();
        public ContasAReceber GetById(int id);
        public void Insert(ContasAReceber contasAReceber);
        public void Update(ContasAReceber contasAReceber);
        public void Delete(int id);
    }
}
