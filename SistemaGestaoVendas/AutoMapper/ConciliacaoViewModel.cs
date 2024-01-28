using SistemaGestaoVendas.Models.ContasAPagarrs;
using SistemaGestaoVendas.Models.ContasAReceberrs;

namespace SistemaGestaoVendas.AutoMapper
{
    public class ConciliacaoViewModel
    {
        public IEnumerable<ContasAPagar> ContasAPagar { get; set; }
        public IEnumerable<ContasAReceber> ContasAReceber { get; set; }
    }
}
