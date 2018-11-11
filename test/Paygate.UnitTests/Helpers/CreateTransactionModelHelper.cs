using System;
using System.Collections.Generic;
using Paygate.Models.Request;
using Paygate.Models.Shared;
using Paygate.UnitTests.Models;

namespace Paygate.UnitTests.Helpers
{
    public static class CreateTransactionModelHelper
    {
        public static CreateTransactionModel GetModelTestData(string name, string surname, string email)
        {
            var orderitems = new List<OrderItems>
            {
                new OrderItems
                {
                    Currency = Currencies.ZAR
                }
            };

            var orderId = Guid.NewGuid().ToString("N"); //We need to pass in a unique reference/orderId per transaction
            return new CreateTransactionModel
            {
                Customer = new CustomerDetails
                {
                    FirstName = name,
                    LastName = surname,
                    Mobile = "",
                    Email = email
                },
                Card = new CardDetails
                {
                    HolderName = "IPaygateService Unit Testing",
                    Number = "4000000000000002",
                    ExpiryMonth = "08",
                    ExpiryYear = $"{DateTime.Now.Year+3}",
                    BudgetPeriod = BudgetPeriods.None,
                    Cvv = "459"
                },
                Order = new OrderDetails
                {
                    Amount = (int) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds / 1337, //Paygate Likes unique values afaik
                    Currency = Currencies.ZAR,
                    MerchantOrderId = orderId,
                    Items = orderitems
                }
            };
        }
        
        public static CreateTransactionModel<UserDefinedModel> GetUserDefinedModelTestData(string name, string surname, string email)
        {
            var orderitems = new List<OrderItems>
            {
                new OrderItems
                {
                    Currency = Currencies.ZAR
                }
            };

            var orderId = Guid.NewGuid().ToString("N"); //We need to pass in a unique reference/orderId per transaction
            return new CreateTransactionModel<UserDefinedModel>
            {
                
                Customer = new CustomerDetails
                {
                    FirstName = name,
                    LastName = surname,
                    Mobile = "",
                    Email = email
                },
                Card = new CardDetails
                {
                    HolderName = "IPaygateService Unit Testing",
                    Number = "4000000000000002",
                    ExpiryMonth = "08",
                    ExpiryYear = $"{DateTime.Now.Year+3}",
                    BudgetPeriod = BudgetPeriods.None,
                    Cvv = "459"
                },
                Order = new OrderDetails
                {
                    Amount = (int) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds / 1337, //Paygate Likes unique values afaik
                    Currency = Currencies.ZAR,
                    MerchantOrderId = orderId,
                    Items = orderitems
                },UserdefinedData = new UserDefinedModel
                {
                    UserDefinedField = "CustomUserDefinedData"
                }
            };
        }
    }
}