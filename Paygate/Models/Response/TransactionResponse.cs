using System;
using Paygate.Models.Shared;

namespace Paygate.Models.Response
{
    public class TransactionResponse
    {
        public int TransactionId { get; set; }
        public string Reference { get; set; }
        public string AcquirerCode { get; set; }
        public StatusName StatusName { get; set; }
        public string AuthCode { get; set; }
        public Guid PayRequestId { get; set; }
        public int TransactionStatusCode { get; set; }
        public string TransactionStatusDescription { get; set; }
        public int ResultCode { get; set; }
        public string ResultDescription { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public string RiskIndicator { get; set; }
        public PaymentType Type { get; set; }
        public RedirectOptions Redirect { get; set; }
    }
}