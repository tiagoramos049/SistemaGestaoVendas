using SistemaGestaoVendas.Models.Checklist;
using SistemaGestaoVendas.Models.Clientes;

namespace SistemaGestaoVendas.Interfaces
{
    public interface ICheckList
    {
        public IEnumerable<CheckList> GetAll();
        public CheckList GetById(int id);
        public void Insert(CheckList checkList);
        public void Update(CheckList checkList);
        public void Delete(int id);
    }
}
