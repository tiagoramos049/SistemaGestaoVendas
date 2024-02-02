﻿namespace SistemaGestaoVendas.Models
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string AccountNumber { get; set; }
        public string UniqueIdentifier { get; set; }
        public string Category { get; set; }
    }
}
