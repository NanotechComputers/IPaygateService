using System.Collections.Generic;
using Paygate.Models.Request;
using Paygate.Models.Response;

namespace Paygate
{
    public interface IPaygateService
    {
        TransactionResponse CreateTransaction(CreateTransactionModel requestData);
        TransactionResponse<TUserdefined> CreateTransaction<TUserdefined>(CreateTransactionModel<TUserdefined> requestData) where TUserdefined : class;
        bool VerifyTransaction(Dictionary<string, string> urlParams, string payRequestId);
        TransactionResponse QueryTransaction(string paygateRequestId);
        TransactionResponse SettleTransaction(string transactionId, int amount);
        TransactionResponse RefundTransaction(string transactionId, int amount);
    }
}