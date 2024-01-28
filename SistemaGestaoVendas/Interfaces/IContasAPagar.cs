using SistemaGestaoVendas.Models.Clientes;
using SistemaGestaoVendas.Models.ContasAPagarrs;

namespace SistemaGestaoVendas.Interfaces
{
    public interface IContasAPagar
    {
        public IEnumerable<ContasAPagar> GetAll();
        public ContasAPagar GetById(int id);
        public void Insert(ContasAPagar contasAPagar);
        public void Update(ContasAPagar contasAPagar);
        public void Delete(int id);
    }
}
