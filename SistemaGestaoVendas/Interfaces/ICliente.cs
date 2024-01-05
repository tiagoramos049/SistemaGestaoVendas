using SistemaGestaoVendas.DAO;
using SistemaGestaoVendas.Models.Clientes;
using System.Data;

namespace SistemaGestaoVendas.Interfaces
{
    public interface ICliente
    {
        public IEnumerable<Cliente> GetAll();
        public Task<Cliente> GetById(int id);
        public void Insert(Cliente cliente);
        public Task Update(Task<Cliente> produto);
        public void Delete(int id);
        
    }
}
