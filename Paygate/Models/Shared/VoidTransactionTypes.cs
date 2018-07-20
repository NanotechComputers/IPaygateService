namespace Paygate.Models.Shared
{
    public sealed class VoidTransactionTypes
    {
        private readonly string _name;
        private readonly int _value;

        public static readonly VoidTransactionTypes Settlement = new VoidTransactionTypes(0, "Settlement");
        public static readonly VoidTransactionTypes Refund = new VoidTransactionTypes(1, "Refund");

        private VoidTransactionTypes(int value, string name)
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