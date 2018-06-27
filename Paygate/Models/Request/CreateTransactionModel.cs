// ReSharper disable ClassNeverInstantiated.Global

using Paygate.Models.Shared;

namespace Paygate.Models.Request
{
    public class CreateTransactionModel
    {
        //Customer
        public CustomerDetails Customer { get; set; }

        //Credit Card
        public CardDetails Card { get; set; }

        //Redirect
        public RedirectDetails Redirect { get; set; }

        //Order
        public OrderDetails Order { get; set; }
    }

    public class CreateTransactionModel<TUserdefined> : CreateTransactionModel
    {
        public TUserdefined UserdefinedData { get; set; }
    }
}