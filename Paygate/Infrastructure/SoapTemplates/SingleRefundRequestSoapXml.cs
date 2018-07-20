using System;

namespace Paygate.Infrastructure.SoapTemplates
{
    internal static class SingleRefundRequestSoapXml
    {
        internal static string Get(string merchantId, string merchantSecret, object dynamicParam, int amount)
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
                default:
                    throw new ArgumentException("Invalid parameters passed");
            }
            
            
            if (amount < 0)
            {
                throw new ArgumentException("The Amount parameter can not be less than 0");
            }
            
            if (amount < 0)
            {
                throw new ArgumentException("The Amount parameter can not be equal to 0");
            }

            return $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:pay=""http://www.paygate.co.za/PayHOST"">
                        <soapenv:Header/>
                        <soapenv:Body>
                            <pay:SingleFollowUpRequest>
                                <pay:RefundRequest>
                                    <pay:Account>
                                        <pay:PayGateId>{merchantId ?? ""}</pay:PayGateId>
                                        <pay:Password>{merchantSecret ?? ""}</pay:Password>
                                    </pay:Account>
                                    {dynamicParamXml}
                                    <pay:Amount>{amount}</pay:Amount>
                                </pay:RefundRequest>
                            </pay:SingleFollowUpRequest>
                        </soapenv:Body>
                    </soapenv:Envelope>";
        }
    }
}