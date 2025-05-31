using SistemaGestaoVendas.Models.Clientes;
using SistemaGestaoVendas.Models.Motos;
using SistemaGestaoVendas.Models.Vendas;

namespace SistemaGestaoVendas.Interfaces
{
    public interface IMoto
    {
        public IEnumerable<Moto> GetAll();
        public IEnumerable<Venda> GetAllVendas();
        public Moto GetById(int id);
        public int Insert(Moto moto);
        public void Update(Moto moto);
        public void Delete(int id);
        public void AtualizarEstoqueERegistrarVenda(int id, int clienteId);
        public IEnumerable<Cliente> ObterTodosClientes();
    }
}
