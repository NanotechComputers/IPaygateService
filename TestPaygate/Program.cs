using System;
using System.Collections.Generic;
using System.Text;
using Paygate;
using Paygate.Models.Request;
using Paygate.Models.Shared;

namespace TestPaygate
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Transaction starting");

                const string url = "https://secure.paygate.co.za/payhost/process.trans";
                const string merchantId = "10011064270"; //No 3d secure redirect on this merchantId
                const string merchantSecret = "test"; //This is the merchantSecret paired with above merchantId

                var paygate = new PaygateService(url, merchantId, merchantSecret);

                var orderitems = new List<OrderItems>
                {
                    new OrderItems
                    {
                        Currency = Currencies.ZAR
                    }
                };

                var orderId = Guid.NewGuid().ToString("N"); //We need to pass in a unique reference/orderId per transaction

                //Build the CreateRequestData expected by Paygate with the minimum of fields
                var data = new CreateTransactionModel
                {
                    Customer = new CustomerDetails
                    {
                        Title = "Mr.",
                        FirstName = "Customer First Name",
                        LastName = "Customer Last Name",
                        Email = "paygate-testing@localhost.co.za",
                        Fax = "",
                        Mobile = "0843344332",
                        Telephone = ""
                    },
                    Card = new CardDetails
                    {
                        HolderName = "Test Card Holder",
                        Number = "4000000000000002",
                        ExpiryMonth = "08",
                        ExpiryYear = "2020",
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
                var response = paygate.CreateTransaction(data);
                switch (response.StatusName)
                {
                    case StatusName.Error:
                        Console.WriteLine("Transaction Error: {0}", response.ResultDescription);
                        break;
                    case StatusName.Pending:
                        Console.WriteLine("Transaction Pending");
                        break;
                    case StatusName.Cancelled:
                        Console.WriteLine("Transaction Cancelled");
                        break;
                    case StatusName.Completed:
                        Console.WriteLine("Transaction Successful");

                        var settled = paygate.SettleTransaction(response.TransactionId.ToString(), data.Order.Amount);
                        Console.WriteLine("Transaction Settled");

                        var refunded = paygate.RefundTransaction(response.TransactionId.ToString(), data.Order.Amount);
                        Console.WriteLine("Transaction Refunded");
                        break;
                    case StatusName.ValidationError:
                        Console.WriteLine("Transaction Failed Validation");
                        break;
                    case StatusName.ThreeDSecureRedirectRequired:
                        Console.WriteLine("Transaction needs to be redirected to 3d secure");
                        break;
                    case StatusName.WebRedirectRequired:
                        Console.WriteLine("Transaction needs a webredirect");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error has occured");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}