using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Paygate;
using Paygate.Models.Request;
using Paygate.Models.Response;
using Paygate.Models.Shared;
using TestPayGateMVC.Models;

namespace TestPayGateMVC.Controllers
{
    public class HomeController : Controller
    {
        private IPaygateService _paygateService;

        public HomeController(IPaygateService paygateService)
        {
            _paygateService = paygateService;
        }

        public IActionResult Index()
        {
            try
            {
                var orderitems = new List<OrderItems>
                {
                    new OrderItems
                    {
                        Currency = Currencies.ZAR
                    }
                };

                var transactionId = Guid.NewGuid().ToString("N");
                Debug.WriteLine("TransactionId: {0}", transactionId);

                //This is the custom data object we pass to paygate in the 5 fields allowed - Please check the api documentation for more.
                var userData = new UserDefinedData
                {
                    CustomData = transactionId,
                    CustomData2 = "FirstName LastName"
                };

                var data = new CreateTransactionModel<UserDefinedData>
                {
                    Customer = new CustomerDetails
                    {
                        Title = "Mr.",
                        FirstName = "Customer First Name",
                        LastName = "Customer Last Name",
                        Email = "paygate-testing@localhost.co.za",
                        Fax = "",
                        Mobile = "0110111011",
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
                        MerchantOrderId = transactionId,
                        Items = orderitems
                    },
                    Redirect = new RedirectDetails
                    {
                        NotifyUrl = "http://localhost:5000/Home/Notify", //This needs to be a proper DNS name that can be resolved by Paygate in order to send ITN
                        RedirectUrl = "http://localhost:5000/Home/Redirect"
                    },
                    UserdefinedData = userData
                };
                var response = _paygateService.CreateTransaction(data);
                switch (response.StatusName)
                {
                    case StatusName.Error:
                        Debug.WriteLine("Transaction Error: {0}", response.ResultDescription);
                        break;
                    case StatusName.Pending:
                        Debug.WriteLine("Transaction Pending");
                        break;
                    case StatusName.Cancelled:
                        Debug.WriteLine("Transaction Cancelled");
                        break;
                    case StatusName.Completed:
                        Debug.WriteLine("Transaction Successful");
                        
                        var settled = _paygateService.SettleTransaction(response.TransactionId);
                        Console.WriteLine("Transaction Settled");

                        var refunded = _paygateService.RefundTransaction(response.TransactionId, data.Order.Amount);
                        Console.WriteLine("Transaction Refunded");
                        
                        return View(response);
                        break;
                    case StatusName.ValidationError:
                        Debug.WriteLine("Transaction Failed Validation");
                        break;
                    case StatusName.ThreeDSecureRedirectRequired:
                        return View(response);
                    case StatusName.WebRedirectRequired:
                        Debug.WriteLine("Transaction needs a webredirect");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }

            return View(new TransactionResponse
            {
                ResultDescription = "An unknown error has occured"
            });
        }

        public IActionResult Notify()
        {
            return Ok(); //Paygate expects 200 - OK resonse else they send again
        }

        public IActionResult Redirect()
        {
            var data = Request;
            var payGateForm = Request.Form;
            if (data == null)
            {
                throw new Exception("Invalid data returned by merchant");
            }

            var dict = payGateForm.ToDictionary(x => x.Key, x => x.Value.ToString());
            dict.TryGetValue("PAY_REQUEST_ID", out var paygateRequestId);

            var queryResponse = _paygateService.QueryTransaction(paygateRequestId); //Paygate does not return userdefinedfields on 3d secure redirects. It's stupid but nothing we can do about it

            var validChecksum = _paygateService.VerifyTransaction(dict, queryResponse.Reference); //the reference should ideally be stored locally for the verification but for this example we will use what is returned from Paygate
            Debug.WriteLine(validChecksum ? "Checksum Matches" : "Checksum does not match, possible tampering");

            switch (queryResponse.StatusName)
            {
                case StatusName.Error:
                    Debug.WriteLine("Transaction Error: {0}", queryResponse.ResultDescription);
                    break;
                case StatusName.Pending:
                    Debug.WriteLine("Transaction Pending");
                    break;
                case StatusName.Cancelled:
                    Debug.WriteLine("Transaction Cancelled");
                    break;
                case StatusName.Completed:
                    Debug.WriteLine("Transaction Successful");
                    return View(queryResponse);
                case StatusName.ValidationError:
                    Debug.WriteLine("Transaction Failed Validation");
                    break;
                case StatusName.ThreeDSecureRedirectRequired:
                case StatusName.WebRedirectRequired:
                    throw new ArgumentOutOfRangeException(); //These are invalid responses here so throw error
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return View(new TransactionResponse
            {
                ResultDescription = "An unknown error has occured"
            });
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}