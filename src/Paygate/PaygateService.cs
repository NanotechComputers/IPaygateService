using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Extensions.Options;
using Paygate.Infrastructure.Extensions;
using Paygate.Infrastructure.SoapTemplates;
using Paygate.Models.Request;
using Paygate.Models.Response;
using Paygate.Models.Shared;
using ServiceStack;

namespace Paygate
{
    public class PaygateService : IPaygateService
    {
        // ReSharper disable once InconsistentNaming
        private string _merchantSecret { get; set; }

        // ReSharper disable once InconsistentNaming
        private const string _contentType = "application/xml";

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

        /// <summary>
        /// CreateXmlDocument method will convert input string to xml document
        /// </summary>
        /// <param name="webResponse">The string data we received from web request to Paygate Server</param>
        /// <returns>XmlDocument</returns>
        private XmlDocument CreateXmlDocument(string webResponse)
        {
            //Convert response to XML Document for manipulation of data
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(webResponse);
            return xmlDocument;
        }

        /// <inheritdoc />
        public TransactionResponse CreateTransaction(CreateTransactionModel requestData)
        {
            //Post to Paygate
            var transactionData = SinglePaymentRequestSoapXml.Get(_merchantId, _merchantSecret, requestData);

            //Get the response
            var response = _url.PostStringToUrl(transactionData, _contentType);

            return CreateXmlDocument(response).ToTransactionResponse();
        }

        /// <inheritdoc />
        public TransactionResponse<TUserdefined> CreateTransaction<TUserdefined>(CreateTransactionModel<TUserdefined> requestData) where TUserdefined : class
        {
            //Post to Paygate
            var transactionData = SinglePaymentRequestSoapXml.Get(_merchantId, _merchantSecret, requestData);

            //Get the response
            var response = _url.PostStringToUrl(transactionData, _contentType);

            return CreateXmlDocument(response).ToTransactionResponse<TUserdefined>();
        }


        /// <inheritdoc />
        public TransactionResponse CreateTokenTransaction(CreateTokenTransactionModel requestData)
        {
            //Post to Paygate
            var transactionData = SingleTokenPaymentRequestSoapXml.Get(_merchantId, _merchantSecret, requestData);

            //Get the response
            var response = _url.PostStringToUrl(transactionData, _contentType);

            return CreateXmlDocument(response).ToTransactionResponse();
        }

        /// <inheritdoc />
        public TransactionResponse<TUserdefined> CreateTokenTransaction<TUserdefined>(CreateTokenTransactionModel<TUserdefined> requestData) where TUserdefined : class
        {
            //Post to Paygate
            var transactionData = SingleTokenPaymentRequestSoapXml.Get(_merchantId, _merchantSecret, requestData);

            //Get the response
            var response = _url.PostStringToUrl(transactionData, _contentType);

            return CreateXmlDocument(response).ToTransactionResponse<TUserdefined>();
        }

        public TransactionResponse CreateVaultTransaction(CreateVaultTransactionModel requestData)
        {
            //Post to Paygate
            var transactionData = SingleVaultPaymentRequestSoapXml.Get(_merchantId, _merchantSecret, requestData);

            //Get the response
            var response = _url.PostStringToUrl(transactionData, _contentType);

            return CreateXmlDocument(response).ToTransactionResponse();
        }

        public TransactionResponse<TUserdefined> CreateVaultTransaction<TUserdefined>(CreateVaultTransactionModel<TUserdefined> requestData) where TUserdefined : class
        {
            throw new NotImplementedException();
        }


        /// <inheritdoc />
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

        /// <inheritdoc />
        public TransactionResponse QueryTransaction(Guid paygateRequestId)
        {
            //Post to Paygate
            var transactionData = SingleFollowUpRequestSoapXml.Get(_merchantId, _merchantSecret, paygateRequestId);

            //Get the response
            var response = _url.PostStringToUrl(transactionData, _contentType);

            return CreateXmlDocument(response).ToTransactionResponse();
        }

        /// <inheritdoc />
        public TransactionResponse QueryTransaction(int transactionId)
        {
            //Post to Paygate
            var transactionData = SingleFollowUpRequestSoapXml.Get(_merchantId, _merchantSecret, transactionId);

            //Get the response
            var response = _url.PostStringToUrl(transactionData, _contentType);

            return CreateXmlDocument(response).ToTransactionResponse();
        }

        /// <inheritdoc />
        public TransactionResponse QueryTransaction(string reference)
        {
            //Post to Paygate
            var transactionData = SingleFollowUpRequestSoapXml.Get(_merchantId, _merchantSecret, reference);

            //Get the response
            var response = _url.PostStringToUrl(transactionData, _contentType);

            return CreateXmlDocument(response).ToTransactionResponse();
        }

        /// <inheritdoc />
        public TransactionResponse SettleTransaction(string reference)
        {
            //Post to Paygate
            var transactionData = SingleSettleRequestSoapXml.Get(_merchantId, _merchantSecret, reference);

            //Get the response
            var response = _url.PostStringToUrl(transactionData, _contentType);

            return CreateXmlDocument(response).ToTransactionResponse();
        }

        /// <inheritdoc />
        public TransactionResponse SettleTransaction(int transactionId)
        {
            //Post to Paygate
            var transactionData = SingleSettleRequestSoapXml.Get(_merchantId, _merchantSecret, transactionId);

            //Get the response
            var response = _url.PostStringToUrl(transactionData, _contentType);

            return CreateXmlDocument(response).ToTransactionResponse();
        }

        /// <inheritdoc />
        public TransactionResponse RefundTransaction(int transactionId, int amount)
        {
            //Post to Paygate
            var transactionData = SingleRefundRequestSoapXml.Get(_merchantId, _merchantSecret, transactionId, amount);

            //Get the response
            var response = _url.PostStringToUrl(transactionData, _contentType);

            return CreateXmlDocument(response).ToTransactionResponse();
        }

        /// <inheritdoc />
        public TransactionResponse RefundTransaction(string reference, int amount)
        {
            //Post to Paygate
            var transactionData = SingleRefundRequestSoapXml.Get(_merchantId, _merchantSecret, reference, amount);

            //Get the response
            var response = _url.PostStringToUrl(transactionData, _contentType);

            return CreateXmlDocument(response).ToTransactionResponse();
        }

        /// <inheritdoc />
        public TransactionResponse VoidTransaction(int transactionId, VoidTransactionTypes transactionType)
        {
            //Post to Paygate
            var transactionData = SingleVoidRequestSoapXml.Get(_merchantId, _merchantSecret, transactionId, transactionType.ToString());

            //Get the response
            var response = _url.PostStringToUrl(transactionData, _contentType);

            return CreateXmlDocument(response).ToTransactionResponse();
        }

        /// <inheritdoc />
        public CardVaultResponse CardVaultRequest(string cardNumber, string cardExpiryDate)
        {
            //Post to Paygate
            var transactionData = SingleCardVaultRequestSoapXml.Get(_merchantId, _merchantSecret, cardNumber, cardExpiryDate);

            //Get the response
            var response = _url.PostStringToUrl(transactionData, _contentType);

            return CreateXmlDocument(response).ToCardVaultResponse();
        }

        /// <inheritdoc />
        public LookupVaultResponse LookupVaultRequest(string vaultId)
        {
            //Post to Paygate
            var transactionData = SingleLookUpVaultRequestSoapXml.Get(_merchantId, _merchantSecret, vaultId);

            //Get the response
            var response = _url.PostStringToUrl(transactionData, _contentType);

            return CreateXmlDocument(response).ToLookupVaultResponse();
        }

        /// <inheritdoc />
        public DeleteVaultResponse DeleteVaultRequest(string vaultId)
        {
            //Post to Paygate
            var transactionData = SingleDeleteVaultRequestSoapXml.Get(_merchantId, _merchantSecret, vaultId);

            //Get the response
            var response = _url.PostStringToUrl(transactionData, _contentType);

            return CreateXmlDocument(response).ToDeleteVaultResponse();
        }
    }
}