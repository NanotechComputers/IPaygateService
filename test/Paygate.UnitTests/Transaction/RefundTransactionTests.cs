using Paygate.Models.Shared;
using Paygate.UnitTests.Helpers;
using Paygate.UnitTests.Models;
using Xunit;

namespace Paygate.UnitTests.Transaction
{
    public class RefundTransactionTests
    {
        private TransactionQueryTypesModel _transactionQueryTypesModel;
        PaygateService _paygateService;
        const string _notSettledMessage = "The Requested Transaction Has Not Been Previously Settled";

        public RefundTransactionTests()
        {
            const string url = "https://secure.paygate.co.za/payhost/process.trans";
            const string merchantId = "10011064270"; //No 3d secure redirect on this merchantId
            const string merchantSecret = "test"; //This is the merchantSecret paired with above merchantId
            
            _paygateService = new PaygateService(url, merchantId, merchantSecret);
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
        public void RefundTransactionByTransactionIdTest()
        {
            CreateTransaction();
            var response = _paygateService.RefundTransaction(_transactionQueryTypesModel.PaygateTransactionId, 5000);
            Assert.True(response.StatusName == StatusName.Completed || response.ResultDescription == _notSettledMessage); //Because Paygate only settles after 18:00 every evening
        }

        [Fact]
        public void RefundTransactionByMerchantReferenceTest()
        {
            CreateTransaction();
            var response = _paygateService.RefundTransaction(_transactionQueryTypesModel.MerchantReference, 5000);
            Assert.True(response.StatusName == StatusName.Completed || response.ResultDescription == _notSettledMessage); //Because Paygate only settles after 18:00 every evening
        }
    }
}