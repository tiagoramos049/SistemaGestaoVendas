using SistemaGestaoVendas.Models.ContasAPagarrs;
using SistemaGestaoVendas.Models.ContasAReceberrs;

namespace SistemaGestaoVendas.Interfaces
{
    public interface IContasAReceber
    {
        public IEnumerable<ContasAReceber> GetAll();
        public IEnumerable<ContasAReceber> GetAnyTrue();
        public ContasAReceber GetById(int id);
        public void Insert(ContasAReceber contasAReceber);
        public void Update(ContasAReceber contasAReceber);
        public void Delete(int id);
    }
}
