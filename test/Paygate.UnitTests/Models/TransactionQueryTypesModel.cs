using System;

namespace Paygate.UnitTests.Models
{
    public class TransactionQueryTypesModel
    {
        public Guid PaygateRequestId { get; set; }
        public int PaygateTransactionId { get; set; }
        public string MerchantReference { get; set; }
    }
}