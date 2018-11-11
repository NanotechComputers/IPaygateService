using Paygate.Models.Shared;
using Paygate.UnitTests.Helpers;
using Paygate.UnitTests.Models;
using Xunit;

namespace Paygate.UnitTests.Transaction
{
    public class QueryTransactionTests
    {
        private TransactionQueryTypesModel _transactionQueryTypesModel;
        PaygateService _paygateService;

        public QueryTransactionTests()
        {
            const string url = "https://secure.paygate.co.za/payhost/process.trans";
            const string merchantId = "10011064270"; //No 3d secure redirect on this merchantId
            const string merchantSecret = "test"; //This is the merchantSecret paired with above merchantId
            
            _paygateService = new PaygateService(url, merchantId, merchantSecret);
            
            CreateTransaction();
        }
        
        private void CreateTransaction()
        {
            var data = CreateTransactionModelHelper.GetModelTestData("Nanotech", "Computers", "IPaygateServiceUnitTesting@nanotechcomputers.co.za");
            var response = _paygateService.CreateTransaction(data);
            _transactionQueryTypesModel = new TransactionQueryTypesModel
            {
                PaygateRequestId = response.PayRequestId,
                MerchantReference = data.Order.MerchantOrderId,
                PaygateTransactionId = response.TransactionId
            };
        }

        [Fact]
        public void QueryTransactionByPaygateRequestIdTest()
        {
            var response = _paygateService.QueryTransaction(_transactionQueryTypesModel.PaygateRequestId);
            Assert.True(response.StatusName == StatusName.Completed);
        }

        [Fact]
        public void QueryTransactionByTransactionIdTest()
        {
            var response = _paygateService.QueryTransaction(_transactionQueryTypesModel.PaygateTransactionId);
            Assert.True(response.StatusName == StatusName.Completed);
        }

        [Fact]
        public void QueryTransactionByMerchantReferenceTest()
        {
            var response = _paygateService.QueryTransaction(_transactionQueryTypesModel.MerchantReference);
            Assert.True(response.StatusName == StatusName.Completed);
        }
    }
}