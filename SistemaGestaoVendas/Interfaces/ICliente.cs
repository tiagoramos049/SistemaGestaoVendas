using SistemaGestaoVendas.DAO;
using SistemaGestaoVendas.Models.Clientes;
using System.Data;

namespace SistemaGestaoVendas.Interfaces
{
    public interface ICliente
    {
        public IEnumerable<Cliente> GetAll();
        public Cliente GetById(int id);
        public void Insert(Cliente cliente);
        public void Update(Cliente cliente);
        public void Delete(int id);
        
    }
}
