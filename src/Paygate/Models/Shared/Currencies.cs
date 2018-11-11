namespace Paygate.Models.Shared
{
    public sealed class Currencies
    {
        private readonly string _name;
        private readonly int _value;

        public static readonly Currencies ZAR = new Currencies(0, "ZAR");
        public static readonly Currencies USD = new Currencies(1, "USD");

        private Currencies(int value, string name)
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