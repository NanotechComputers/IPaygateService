using System;

namespace Paygate.Infrastructure.SoapTemplates
{
    internal static class SingleDeleteVaultRequestSoapXml
    {
        internal static string Get(string merchantId, string merchantSecret, string vaultId)
        {
            if (string.IsNullOrEmpty(vaultId))
            {
                throw new NullReferenceException(nameof(vaultId));
            }

            return $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:pay=""http://www.paygate.co.za/PayHOST"">
                        <soapenv:Header/>
                        <soapenv:Body>
                            <pay:SingleVaultRequest>
                                <pay:DeleteVaultRequest>
                                    <pay:Account>
                                        <pay:PayGateId>{merchantId ?? ""}</pay:PayGateId>
                                        <pay:Password>{merchantSecret ?? ""}</pay:Password>
                                    </pay:Account>
                                    <pay:VaultId>{vaultId ?? ""}</pay:VaultId>
                                </pay:DeleteVaultRequest>
                            </pay:SingleVaultRequest>
                        </soapenv:Body>
                    </soapenv:Envelope>";
        }

    }
}