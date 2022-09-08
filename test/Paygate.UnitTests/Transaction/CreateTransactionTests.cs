using System;
using System.Collections.Generic;
using Paygate.Models.Request;
using Paygate.Models.Shared;
using Paygate.UnitTests.Helpers;
using Paygate.UnitTests.Models;
using Xunit;

namespace Paygate.UnitTests.Transaction
{
    public class CreateTransactionTests
    {
        PaygateService _paygateService;
        public CreateTransactionTests()
        {
            const string url = "https://secure.paygate.co.za/payhost/process.trans";
            const string merchantId = "10011064270"; //No 3d secure redirect on this merchantId
            const string merchantSecret = "test"; //This is the merchantSecret paired with above merchantId
            _paygateService = new PaygateService(url, merchantId, merchantSecret);
        }

        [Theory]
        [InlineData("Nanotech", "Computers", "IPaygateServiceUnitTesting@nanotechcomputers.co.za")]
        public void CreateTransactionTest(string name, string surname, string email)
        {
            var data = CreateTransactionModelHelper.GetModelTestData(name, surname, email);
            var response = _paygateService.CreateTransaction(data);
            Assert.True(response.StatusName == StatusName.Completed);
        }
        
        //TODO: move to helper
        [Theory]
        [InlineData("Nanotech", "Computers", "IPaygateServiceUnitTesting@nanotechcomputers.co.za")]
        public void CreateTransactionWithCustomDataTest(string name, string surname, string email)
        {
            var data = CreateTransactionModelHelper.GetUserDefinedModelTestData(name, surname, email);
            var response = _paygateService.CreateTransaction(data);
            Assert.True(response.UserdefinedData.UserDefinedField == "CustomUserDefinedData");
            Assert.True(response.StatusName == StatusName.Completed);
        }

    }
}