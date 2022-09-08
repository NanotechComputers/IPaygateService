using System;
using System.Linq;
using Paygate.Infrastructure.Extensions;
using Paygate.Models.Request;

namespace Paygate.Infrastructure.SoapTemplates
{
    internal static class SingleVaultPaymentRequestSoapXml
    {
        private static string Get(string merchantId, string merchantSecret, CreateVaultTransactionModel data, string userDefinedFieldXml)
        {
            //TODO: Remove the validation below and use data attributes and custom Validator
            if (data == null)
            {
                throw new NullReferenceException(nameof(data));
            }

            if (data.Customer == null)
            {
                throw new NullReferenceException(nameof(data.Customer));
            }

            if (string.IsNullOrEmpty(data.Customer.FirstName))
            {
                throw new NullReferenceException(nameof(data.Customer.FirstName));
            }

            if (string.IsNullOrEmpty(data.Customer.LastName))
            {
                throw new NullReferenceException(nameof(data.Customer.LastName));
            }

            if (string.IsNullOrEmpty(data.Customer.Email))
            {
                throw new NullReferenceException(nameof(data.Customer.Email));
            }

            if (string.IsNullOrEmpty(data.Card.VaultId))
            {
                throw new NullReferenceException(nameof(data.Card.VaultId));
            }

            if (string.IsNullOrEmpty(data.Card.Cvv))
            {
                throw new NullReferenceException(nameof(data.Card.Cvv));
            }

            var productItemsXml = "";
            if (data.Order.Items.Any())
            {
                productItemsXml = data.Order.Items.Aggregate(productItemsXml, (current, orderItem) => current + $@"<pay:OrderItems>
                                                <pay:ProductCode>{orderItem?.ProductCode ?? ""}</pay:ProductCode>
                                                <pay:ProductDescription>{orderItem?.ProductDescription ?? ""}</pay:ProductDescription>
                                                <pay:ProductCategory>{orderItem?.ProductCategory ?? ""}</pay:ProductCategory>
                                                <pay:ProductRisk>{orderItem?.ProductRisk ?? ""}</pay:ProductRisk>
                                                <pay:OrderQuantity>{orderItem?.OrderQuantity}</pay:OrderQuantity>
                                                <pay:UnitPrice>{orderItem?.UnitPrice}</pay:UnitPrice>
                                                <pay:Currency>{orderItem?.Currency?.ToString() ?? ""}</pay:Currency>
                                            </pay:OrderItems>");
            }

            return $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:pay=""http://www.paygate.co.za/PayHOST"">
                        <soapenv:Header/>
                        <soapenv:Body>
                            <pay:SinglePaymentRequest>
                                <pay:CardPaymentRequest>
                                    <pay:Account>
                                        <pay:PayGateId>{merchantId ?? ""}</pay:PayGateId>
                                        <pay:Password>{merchantSecret ?? ""}</pay:Password>
                                    </pay:Account>
                                    <pay:Customer>
                                        <pay:FirstName>{data.Customer?.FirstName ?? ""}</pay:FirstName>
                                        <pay:LastName>{data.Customer?.LastName ?? ""}</pay:LastName>
                                        <pay:Mobile>{data.Customer?.Mobile ?? ""}</pay:Mobile>
                                        <pay:Email>{data.Customer?.Email ?? ""}</pay:Email>
                                    </pay:Customer>
                                    <pay:VaultId>{data.Card?.VaultId ?? ""}</pay:VaultId>
                                    <pay:CVV>{data.Card?.Cvv ?? ""}</pay:CVV>
                                    <pay:BudgetPeriod>{data.Card?.BudgetPeriod}</pay:BudgetPeriod>
                                    <pay:Order>
                                        <pay:MerchantOrderId>{data.Order?.MerchantOrderId ?? ""}</pay:MerchantOrderId>
                                        <pay:Currency>{data.Order?.Currency}</pay:Currency>
                                        <pay:Amount>{data.Order?.Amount}</pay:Amount>
                                        {productItemsXml}
                                    </pay:Order>
                                    {userDefinedFieldXml}   
                                </pay:CardPaymentRequest>
                            </pay:SinglePaymentRequest>
                        </soapenv:Body>
                    </soapenv:Envelope>";
        }
        
        internal static string Get(string merchantId, string merchantSecret, CreateVaultTransactionModel data)
        {
            return Get(merchantId, merchantSecret, data, "");
        }

        internal static string Get<TUserdefined>(string merchantId, string merchantSecret, CreateVaultTransactionModel<TUserdefined> data) where TUserdefined : class
        {
            
            var userdefinedFieldsAvailable = 5;
            var totalTextLengthPerField = 254;
            var stringData = StringExtensions.Serialize(data.UserdefinedData);
            var compressed = stringData.CompressString();

            if (compressed.Length + 1 > (userdefinedFieldsAvailable*totalTextLengthPerField))
            {
                throw new Exception("User Defined Field exceeds available length");
            }

            var userDefinedFieldXml = "";
            if (string.IsNullOrEmpty(compressed))
            {
                return Get(merchantId, merchantSecret, data, userDefinedFieldXml);
            }
            
            // Add it to the xml
            var splitString = compressed.SplitInChunks(254);
            var idx = 1;
            foreach (var x in splitString)
            {
                userDefinedFieldXml += $@"<pay:UserDefinedFields><pay:key>{idx}</pay:key><pay:value>{x}</pay:value></pay:UserDefinedFields>";
                idx ++;
            }

            return Get(merchantId, merchantSecret, data, userDefinedFieldXml);

            
        }
    }
}