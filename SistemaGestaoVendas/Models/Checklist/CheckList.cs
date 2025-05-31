namespace SistemaGestaoVendas.Models.Checklist
{
    public class CheckList
    {
        public int IdMotos { get; set; }
        public string NomeMoto { get; set; } = string.Empty;
        public string QuantidadeEstoque { get; set; } = string.Empty;
        public bool ChecarDebitos { get; set; }
        public bool ChecarAlinhamento { get; set; }
        public bool ChecarCarenagem { get; set; }
        public bool ChecarRodasPneus { get; set; }
        public bool ChecarVazamentos { get; set; }
        public bool ChecarManualChave { get; set; }
        public bool ChecarPainel { get; set; }
        public bool ChecarEletrica { get; set; }
        public bool ChecarEmbreagem { get; set; }
        public bool ChecarCaixaDirecao { get; set; }
        public bool ChecarMotor { get; set; }
        public bool ChecarFumacaDoMotor { get; set; }
        public bool ChecarRolamentoDisco { get; set; }
        public bool ChecarChassi { get; set; }
    }
}
