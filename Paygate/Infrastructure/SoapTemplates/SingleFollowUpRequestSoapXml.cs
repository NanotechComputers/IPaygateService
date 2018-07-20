using System;

namespace Paygate.Infrastructure.SoapTemplates
{
    internal static class SingleFollowUpRequestSoapXml
    {
        internal static string Get(string merchantId, string merchantSecret, object dynamicParam)
        {
            var dynamicParamXml = "";
            switch (dynamicParam)
            {
                case string reference:
                    if (string.IsNullOrWhiteSpace(reference))
                    {
                        throw new ArgumentException("The Reference parameter can not be empty");
                    }

                    dynamicParamXml = $"<pay:MerchantOrderId>{reference}</pay:MerchantOrderId>";
                    break;
                case int transactionId:
                    if (transactionId <= 0)
                    {
                        throw new ArgumentException("The TransactionId parameter is invalid");
                    }

                    dynamicParamXml = $"<pay:TransactionId>{transactionId}</pay:TransactionId>";
                    break;
                case Guid payRequestId:
                    if (payRequestId == Guid.Empty || payRequestId == Guid.NewGuid())
                    {
                        throw new ArgumentException("The PayRequestId parameter is not a valid GUID");
                    }

                    dynamicParamXml = $"<pay:PayRequestId>{payRequestId.ToString("D").ToUpper() ?? ""}</pay:PayRequestId>";
                    break;
                default:
                    throw new ArgumentException("Invalid parameters passed");
            }

            return $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:pay=""http://www.paygate.co.za/PayHOST"">
                        <soapenv:Header/>
                        <soapenv:Body>
                            <pay:SingleFollowUpRequest>
                                <pay:QueryRequest>
                                    <pay:Account>
                                        <pay:PayGateId>{merchantId ?? ""}</pay:PayGateId>
                                        <pay:Password>{merchantSecret ?? ""}</pay:Password>
                                    </pay:Account>
                                    {dynamicParamXml}
                                </pay:QueryRequest>
                            </pay:SingleFollowUpRequest>
                        </soapenv:Body>
                    </soapenv:Envelope>";
        }
    }
}