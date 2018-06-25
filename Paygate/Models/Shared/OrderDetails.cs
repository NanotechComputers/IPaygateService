using System.Collections.Generic;

namespace Paygate.Models.Shared
{
    public class OrderDetails
    {
        public string MerchantOrderId { get; set; }
        public Currencies Currency { get; set; }
        public int Amount { get; set; }
        public IList<OrderItems> Items { get; set; }
    }
}