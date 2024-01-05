using SistemaGestaoVendas.Models.Clientes;
using SistemaGestaoVendas.Models.Produtos;

namespace SistemaGestaoVendas.Interfaces
{
    public interface IProduto
    {
        public IEnumerable<Produto> GetAll();
        public Produto GetById(int id);
        public void Insert(Produto produto);
        public void Update(Produto produto);
        public void Delete(int id);
    }
}
