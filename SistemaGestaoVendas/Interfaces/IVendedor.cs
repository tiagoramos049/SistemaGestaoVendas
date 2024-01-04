using SistemaGestaoVendas.Models.Produtos;
using SistemaGestaoVendas.Models.Vendedores;

namespace SistemaGestaoVendas.Interfaces
{
    public interface IVendedor
    {
        public IEnumerable<Vendedor> GetAll();
        public Task<Vendedor> GetById(int id);
        public void Insert(Vendedor vendedor);
        public void Update(Vendedor vendedor);
        public void Delete(int id);
    }
}
