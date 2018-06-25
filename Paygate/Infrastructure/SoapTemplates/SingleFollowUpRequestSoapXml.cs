using System;

namespace Paygate.Infrastructure.SoapTemplates
{
    internal static class SingleFollowUpRequestSoapXml
    {
        internal static string Get(string merchantId, string merchantSecret, string payRequestId)
        {
            
            if (string.IsNullOrEmpty(payRequestId))
            {
                throw new NullReferenceException(nameof(payRequestId)); 
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
                                    <pay:PayRequestId>{payRequestId ?? ""}</pay:PayRequestId>
                                </pay:QueryRequest>
                            </pay:SingleFollowUpRequest>
                        </soapenv:Body>
                    </soapenv:Envelope>";
        }
    }
}