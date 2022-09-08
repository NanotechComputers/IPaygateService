namespace Paygate.Models.Shared
{
    public class CardDetails
    {
        public string Number { get; set; }
        public string Cvv { get; set; }
        public BudgetPeriods BudgetPeriod { get; set; } = BudgetPeriods.None;
        public string HolderName { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
    }
    public class VaultCardDetails
    {
        public string VaultId { get; set; }
        public string Cvv { get; set; }
        public BudgetPeriods BudgetPeriod { get; set; } = BudgetPeriods.None;
    }
}