
// ReSharper disable ClassNeverInstantiated.Global

using Paygate.Models.Shared;

namespace Paygate.Models.Request
{
    public class CreateRequest
    {
        //Customer
        public CustomerDetails Customer { get; set; }
        
        //Credit Card Details
        public CardDetails Card { get; set; }
        
        //Redirect
        public RedirectDetails Redirect { get; set; }
        
        //Order
        public OrderDetails Order { get; set; }
    }
}