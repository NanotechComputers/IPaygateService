using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using AutoMapper;
using Microsoft.Extensions.Options;
using Paygate.Extensions;
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

        public TransactionResponse CreateTransaction(CreateRequest requestData)
        {
            //Post to Paygate
            var transactionData = SinglePaymentRequestSoapXml.Get(_merchantId, _merchantSecret, requestData);

            //Get the response
            var response = _url.PostStringToUrl(transactionData, contentType: "application/xml");
            Debug.WriteLine(response);
            //Convert response to XML Document for manipulation of data
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(response);

            //Return the converted response using automapper and some helper methods for the parsing
            return Mapper.Map<TransactionResponse>(xmlDocument);
        }

        public bool VerifyTransaction(Dictionary<string, string> urlParams, string reference)
        {
            urlParams.TryGetValue("CHECKSUM", out var checksum); //Checksum from Paygate
            urlParams.TryGetValue("PAY_REQUEST_ID", out var paygateRequestId);
            urlParams.TryGetValue("TRANSACTION_STATUS", out var transactionStatus);
            
            //PAYGATE_ID+PAY_REQUEST_ID+TRANSACTION_STATUS+REFERENCE+KEY

            var checksumHash = _merchantId + paygateRequestId + transactionStatus + reference + _merchantSecret; //Calculated Checksum
            return checksumHash == checksum.ToMd5Hash(); //Verify if the two match
        }

        public TransactionResponse QueryTransaction(string paygateRequestId)
        {
            //Post to Paygate
            var transactionData = SingleFollowUpRequestSoapXml.Get(_merchantId, _merchantSecret, paygateRequestId);

            //Get the response
            var response = _url.PostStringToUrl(transactionData, contentType: "application/xml");

            //Convert response to XML Document for manipulation of data
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(response);

            //Return the converted response using automapper and some helper methods for the parsing
            return Mapper.Map<TransactionResponse>(xmlDocument);
        }
    }
}