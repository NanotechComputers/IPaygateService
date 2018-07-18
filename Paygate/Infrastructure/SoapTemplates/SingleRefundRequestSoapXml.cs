using System;

namespace Paygate.Infrastructure.SoapTemplates
{
    internal static class SingleRefundRequestSoapXml
    {
        internal static string Get(string merchantId, string merchantSecret, string transactionId, int amount)
        {
            
            if (string.IsNullOrEmpty(transactionId))
            {
                throw new NullReferenceException(nameof(transactionId)); 
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
                                    <pay:TransactionId>{transactionId ?? ""}</pay:TransactionId>
                                    <pay:Amount>{amount}</pay:Amount>
                                </pay:RefundRequest>
                            </pay:SingleFollowUpRequest>
                        </soapenv:Body>
                    </soapenv:Envelope>";
        }
    }
}