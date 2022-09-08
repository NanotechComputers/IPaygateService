using Paygate.Models.Shared;

namespace Paygate.Models.Request
{
    public class CreateTokenTransactionModel
    {
        //Customer
        public CustomerDetails Customer { get; set; }

        //Token
        public string Token { get; set; }

        public string TokenDetail { get; set; }

        public bool Vault { get; set; }

        public string BillingDescriptor { get; set; }

        //Order
        public OrderDetails Order { get; set; }
    }

    public class CreateTokenTransactionModel<TUserdefined> : CreateTokenTransactionModel
    {
        public TUserdefined UserdefinedData { get; set; }
    }

    public class CreateVaultTransactionModel
    {
        //Customer
        public CustomerDetails Customer { get; set; }

        //Vault Card
        public VaultCardDetails Card { get; set; }

        public string BillingDescriptor { get; set; }

        //Order
        public OrderDetails Order { get; set; }
    }

    public class CreateVaultTransactionModel<TUserdefined> : CreateVaultTransactionModel
    {
        public TUserdefined UserdefinedData { get; set; }
    }
}