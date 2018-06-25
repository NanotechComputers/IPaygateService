using System.Collections.Generic;
using Paygate.Models.Request;
using Paygate.Models.Response;

namespace Paygate
{
    public interface IPaygateService
    {
        TransactionResponse CreateTransaction(CreateRequest requestData);
        bool VerifyTransaction(Dictionary<string, string> urlParams, string payRequestId);
        TransactionResponse QueryTransaction(string paygateRequestId);
    }
}