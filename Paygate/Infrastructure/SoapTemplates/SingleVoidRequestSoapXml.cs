using System;

namespace Paygate.Infrastructure.SoapTemplates
{
    internal static class SingleVoidRequestSoapXml
    {
        public static string Get(string merchantId, string merchantSecret, int transactionId, string transactionType)
        {
            if (transactionId <= 0)
            {
                throw new ArgumentException("The TransactionId parameter is invalid");
            }
            
            if (string.IsNullOrEmpty(transactionType))
            {
                throw new NullReferenceException(nameof(transactionType));
            }

            return $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:pay=""http://www.paygate.co.za/PayHOST"">
                        <soapenv:Header/>
                        <soapenv:Body>
                            <pay:SingleFollowUpRequest>
                                <pay:VoidRequest>
                                    <pay:Account>
                                        <pay:PayGateId>{merchantId ?? ""}</pay:PayGateId>
                                        <pay:Password>{merchantSecret ?? ""}</pay:Password>
                                    </pay:Account>
                                    <pay:TransactionId>{transactionId}</pay:TransactionId>
                                    <pay:TransactionType>{transactionType}</pay:TransactionType>
                                </pay:VoidRequest>
                            </pay:SingleFollowUpRequest>
                        </soapenv:Body>
                    </soapenv:Envelope>";
        }
    }
}