using System;
using System.Xml;
using Paygate.Infrastructure.Helpers;
using Paygate.Models.Response;
using Paygate.Models.Shared;

namespace Paygate.Infrastructure.Extensions
{
    internal static class TransactionResponseExtensions
    {
        internal static TransactionResponse ToTransactionResponse(this XmlDocument src)
        {
            var error = src.GetXmlError<string>("faultstring");
            var errorDetails = src.GetXmlError<string>("detail");
            
            if(!string.IsNullOrWhiteSpace(error) && !string.IsNullOrWhiteSpace(errorDetails))
            {
                //Some error from PayGate - throw
                throw new Exception($"An error was returned from PayGate: {error}{Environment.NewLine}Details: {errorDetails}");
            }
            
            Guid.TryParse(src.GetXml<string>("PayRequestId"), out var payRequestId);
            var response = new TransactionResponse
            {
                TransactionId = src.GetXml<int>("TransactionId"),
                Reference = src.GetXml<string>("Reference"),
                AcquirerCode = src.GetXml<string>("AcquirerCode"),
                StatusName = src.GetXml<string>("StatusName").GetStatusName(),
                AuthCode = src.GetXml<string>("AuthCode"),
                PayRequestId = payRequestId,
                TransactionStatusCode = src.GetXml<int>("TransactionStatusCode"),
                TransactionStatusDescription = src.GetXml<string>("TransactionStatusDescription"),
                ResultCode = src.GetXml<int>("ResultCode"),
                ResultDescription = src.GetXml<string>("ResultDescription"),
                Currency = src.GetXml<string>("Currency"),
                Amount = src.GetXml<decimal>("Amount"),
                RiskIndicator = src.GetXml<string>("RiskIndicator"),
                Type = new PaymentType
                {
                    Detail = src.GetXml<string>(new[] {"PaymentType", "Detail"}),
                    Method = src.GetXml<string>(new[] {"PaymentType", "Method"})
                },
                Redirect = new RedirectOptions
                {
                    RedirectUrl = src.GetXml<string>(new[] {"Redirect", "RedirectUrl"}),
                    UrlParams = src.GetXmlNode("Redirect").GetUrlParams(src),
                    Form = src.GetXmlNode("Redirect").GetUrlParams(src).BuildForm(src.GetXml<string>(new[] {"Redirect", "RedirectUrl"}))
                }
            };
            return response;
        }

        internal static TransactionResponse<TUserdefined> ToTransactionResponse<TUserdefined>(this XmlDocument src) where TUserdefined : class
        {
            var error = src.GetXmlError<string>("faultstring");
            var errorDetails = src.GetXmlError<string>("detail");
            
            if(!string.IsNullOrWhiteSpace(error) && !string.IsNullOrWhiteSpace(errorDetails))
            {
                //Some error from PayGate - throw
                throw new Exception($"An error was returned from PayGate: {error}{Environment.NewLine}Details: {errorDetails}");
            }
            
            Guid.TryParse(src.GetXml<string>("PayRequestId"), out var payRequestId);
            var response = new TransactionResponse<TUserdefined>
            {
                TransactionId = src.GetXml<int>("TransactionId"),
                Reference = src.GetXml<string>("Reference"),
                AcquirerCode = src.GetXml<string>("AcquirerCode"),
                StatusName = src.GetXml<string>("StatusName").GetStatusName(),
                AuthCode = src.GetXml<string>("AuthCode"),
                PayRequestId = payRequestId,
                TransactionStatusCode = src.GetXml<int>("TransactionStatusCode"),
                TransactionStatusDescription = src.GetXml<string>("TransactionStatusDescription"),
                ResultCode = src.GetXml<int>("ResultCode"),
                ResultDescription = src.GetXml<string>("ResultDescription"),
                Currency = src.GetXml<string>("Currency"),
                Amount = src.GetXml<decimal>("Amount"),
                RiskIndicator = src.GetXml<string>("RiskIndicator"),
                Type = new PaymentType
                {
                    Detail = src.GetXml<string>(new[] {"PaymentType", "Detail"}),
                    Method = src.GetXml<string>(new[] {"PaymentType", "Method"})
                },
                Redirect = new RedirectOptions
                {
                    RedirectUrl = src.GetXml<string>(new[] {"Redirect", "RedirectUrl"}),
                    UrlParams = src.GetXmlNode("Redirect").GetUrlParams(src),
                    Form = src.GetXmlNode("Redirect").GetUrlParams(src).BuildForm(src.GetXml<string>(new[] {"Redirect", "RedirectUrl"}))
                },
                UserdefinedData = src.GetXmlNode("UserDefinedFields").GetUserDefinedFields<TUserdefined>(src)
            };
            return response;
        }
    }
}