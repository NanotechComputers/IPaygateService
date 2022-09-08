using System;
using Paygate.Models.Shared;

namespace Paygate.Models.Response
{
    public class TransactionResponse
    {
        /// <summary>
        /// The unique reference number assign by PayGate to this transaction
        /// </summary>
        public int TransactionId { get; set; }
        /// <summary>
        /// This is your reference number for use by your internal systems.
        /// <remarks>We return the MerchantOrderId you passed to us in the payment request. e.g. Your Customer, Invoice or Order Number.</remarks>
        /// </summary>
        public string Reference { get; set; }
        /// <summary>
        /// This is the transaction status code returned by the acquirer
        /// </summary>
        public string AcquirerCode { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public StatusName StatusName { get; set; }
        /// <summary>
        /// Returns further detail relating to the StatusName returned
        /// </summary>
        public string StatusDetail { get; set; }
        /// <summary>
        /// The Authorisation code returned by the acquirer (bank)
        /// </summary>
        public string AuthCode { get; set; }
        /// <summary>
        /// The unique reference for the payment request
        /// </summary>
        public Guid PayRequestId { get; set; }
        /// <summary>
        /// This is the PayVault token associated to the card used to make the payment. This Vault ID can be re-used to process payments on the card. Only the PAN and Expiry Date are linked to this token.
        /// <remarks>This is an optional field and is only returned if PayVault tokenisation is requested</remarks>
        /// </summary>
        public string VaultId { get; set; }
        /// <summary>
        /// This field contains information on the credit card or e-wallet account linked to the PayVault token for the purpose of managing the use of the token
        /// <remarks>This is an optional field and is only returned if PayVault tokenisation is requested</remarks>
        /// </summary>
        public string VaultData1 { get; set; }
        /// <summary>
        /// This field contains information on the credit card or e-wallet account linked to the PayVault token for the purpose of managing the use of the token
        /// <remarks>This is an optional field and is only returned if PayVault tokenisation is requested</remarks>
        /// </summary>
        public string VaultData2 { get; set; }
        /// <summary>
        /// Transaction status. Refer to the transaction status table
        /// </summary>
        public int TransactionStatusCode { get; set; }
        /// <summary>
        /// Transaction status description
        /// </summary>
        public string TransactionStatusDescription { get; set; }
        /// <summary>
        /// Result Code. Refer to the result code table
        /// </summary>
        public int ResultCode { get; set; }
        /// <summary>
        /// Result Code description
        /// </summary>
        public string ResultDescription { get; set; }
        /// <summary>
        /// Currency code of the currency the customer is paying in. Refer to appendix A for valid currency codes
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Transaction Amount in cents
        /// <remarks>e.g. R32.95 would be specified as 3295</remarks>
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// This is a 2-character field containing a risk indicator for this transaction. The first character describes Verified-by-Visa / MasterCard SecureCode authentication
        /// <remarks>Refer to the Authentication Indicator table for possible values. The second character is for future use and will be set to ‘X’.</remarks>
        /// </summary>
        public string RiskIndicator { get; set; }
        /// <summary>
        /// The payment method type used in the transaction
        /// </summary>
        public PaymentType Type { get; set; }
        /// <summary>
        /// The data in a Redirect Response message will contain a URL that the Merchant will be required to redirect the Customer to. There will also be a list of Key/Value pairs returned.
        /// <remarks>A merchant should iterate through all the Key/Value pairs as all these will need to be included in the POST message when redirecting the Customer to the PayGate Payment Page</remarks>
        /// </summary>
        public RedirectOptions Redirect { get; set; }
    }

    public class TransactionResponse<TUserdefined> : TransactionResponse
    {
        public TUserdefined UserdefinedData { get; set; }
    }
}