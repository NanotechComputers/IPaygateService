using System.Collections.Generic;
using System.Xml;
using Microsoft.Extensions.Options;
using Paygate.Infrastructure.Extensions;
using Paygate.Infrastructure.SoapTemplates;
using Paygate.Models.Request;
using Paygate.Models.Response;
using ServiceStack;

namespace Paygate
{
    public class PaygateService : IPaygateService
    {
        // ReSharper disable once InconsistentNaming
        private string _merchantSecret { get; set; }

        // ReSharper disable once InconsistentNaming
        private string _url { get; set; }

        // ReSharper disable once InconsistentNaming
        private string _merchantId { get; set; }

        public PaygateService(IOptions<PaygateServiceOptions> options)
        {
            _url = options.Value.Url;
            _merchantId = options.Value.MerchantId;
            _merchantSecret = options.Value.MerchantSecret;
        }

        public PaygateService(string url, string merchantId, string merchantSecret)
        {
            _url = url;
            _merchantId = merchantId;
            _merchantSecret = merchantSecret;
        }

        public TransactionResponse CreateTransaction(CreateTransactionModel requestData)
        {
            //Post to Paygate
            var transactionData = SinglePaymentRequestSoapXml.Get(_merchantId, _merchantSecret, requestData);

            //Get the response
            var response = _url.PostStringToUrl(transactionData, "application/xml");

            //Convert response to XML Document for manipulation of data
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(response);

            return xmlDocument.ToTransactionResponse();
        }

        public TransactionResponse<TUserdefined> CreateTransaction<TUserdefined>(CreateTransactionModel<TUserdefined> requestData) where TUserdefined : class
        {
            //Post to Paygate
            var transactionData = SinglePaymentRequestSoapXml.Get(_merchantId, _merchantSecret, requestData);

            //Get the response
            var response = _url.PostStringToUrl(transactionData, "application/xml");

            //Convert response to XML Document for manipulation of data
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(response);

            return xmlDocument.ToTransactionResponse<TUserdefined>();
        }

        public bool VerifyTransaction(Dictionary<string, string> urlParams, string reference)
        {
            urlParams.TryGetValue("CHECKSUM", out var checksum); //Checksum from Paygate
            urlParams.TryGetValue("PAY_REQUEST_ID", out var paygateRequestId);
            urlParams.TryGetValue("TRANSACTION_STATUS", out var transactionStatus);

            //PAYGATE_ID+PAY_REQUEST_ID+TRANSACTION_STATUS+REFERENCE+KEY

            var checksumHash = (_merchantId + paygateRequestId + transactionStatus + reference + _merchantSecret).ToMd5Hash(); //Calculated Checksum
            var matches = checksumHash == (checksum?.ToUpper() ?? ""); //Verify if the two match
            return matches;
        }

        public TransactionResponse QueryTransaction(string paygateRequestId)
        {
            //Post to Paygate
            var transactionData = SingleFollowUpRequestSoapXml.Get(_merchantId, _merchantSecret, paygateRequestId);

            //Get the response
            var response = _url.PostStringToUrl(transactionData, "application/xml");

            //Convert response to XML Document for manipulation of data
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(response);

            //Return the converted response using automapper and some helper methods for the parsing
            return xmlDocument.ToTransactionResponse();
        }

        public TransactionResponse SettleTransaction(string transactionId, int amount)
        {
            //Post to Paygate
            var transactionData = SingleSettleRequestSoapXml.Get(_merchantId, _merchantSecret, transactionId, amount);

            //Get the response
            var response = _url.PostStringToUrl(transactionData, "application/xml");

            //Convert response to XML Document for manipulation of data
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(response);

            //Return the converted response using automapper and some helper methods for the parsing
            return xmlDocument.ToTransactionResponse();
        }

        public TransactionResponse RefundTransaction(string transactionId, int amount)
        {
            //Post to Paygate
            var transactionData = SingleRefundRequestSoapXml.Get(_merchantId, _merchantSecret, transactionId, amount);

            //Get the response
            var response = _url.PostStringToUrl(transactionData, "application/xml");

            //Convert response to XML Document for manipulation of data
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(response);

            //Return the converted response using automapper and some helper methods for the parsing
            return xmlDocument.ToTransactionResponse();
        }
    }
}