namespace Paygate.Models.Shared
{
    public sealed class BudgetPeriods
    {
        private readonly string _name;
        private readonly int _value;

        public static readonly BudgetPeriods None = new BudgetPeriods(0, "0");
        public static readonly BudgetPeriods TwelveMonths = new BudgetPeriods(12, "12");
        public static readonly BudgetPeriods EighteenMonths = new BudgetPeriods(18, "18");
        public static readonly BudgetPeriods TwentyFourMonths = new BudgetPeriods(24, "24");
        public static readonly BudgetPeriods ThirtySixMonths = new BudgetPeriods(36, "36");

        private BudgetPeriods(int value, string name)
        {
            _name = name;
            _value = value;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}