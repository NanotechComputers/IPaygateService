using System;
using System.Linq;
using Paygate.Models.Request;
using static Paygate.Infrastructure.Extensions.StringExtensions;

namespace Paygate.Infrastructure.SoapTemplates
{
    internal static class SingleCardVaultRequestSoapXml
    {
        internal static string Get(string merchantId, string merchantSecret, string cardNumber, string cardExpiryDate)
        {
            if (string.IsNullOrEmpty(cardNumber))
            {
                throw new NullReferenceException(nameof(cardNumber));
            }

            if (string.IsNullOrEmpty(cardExpiryDate))
            {
                throw new NullReferenceException(nameof(cardExpiryDate));
            }

            return $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:pay=""http://www.paygate.co.za/PayHOST"">
                        <soapenv:Header/>
                        <soapenv:Body>
                            <pay:SingleVaultRequest>
                                <pay:CardVaultRequest>
                                    <pay:Account>
                                        <pay:PayGateId>{merchantId ?? ""}</pay:PayGateId>
                                        <pay:Password>{merchantSecret ?? ""}</pay:Password>
                                    </pay:Account>
                                    <pay:CardNumber>{cardNumber ?? ""}</pay:CardNumber>
                                    <pay:CardExpiryDate>{cardExpiryDate ?? ""}</pay:CardExpiryDate>
                                </pay:CardVaultRequest>
                            </pay:SingleVaultRequest>
                        </soapenv:Body>
                    </soapenv:Envelope>";
        }

    }
}