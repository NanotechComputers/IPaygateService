using System;
using System.Collections.Generic;
using Paygate.Models.Request;
using Paygate.Models.Response;
using Paygate.Models.Shared;

namespace Paygate
{
    public interface IPaygateService
    {
        #region Create Transaction

        /// <summary>
        /// CreateTransaction request will reserve the specified amount on the supplied credit card
        /// <remarks>
        /// This does not move the funds from the credit card account to the merchants account.<br/>
        /// To move the funds you must either process a Settlement transaction or get PayGate to turn on the AutoSettle option
        /// </remarks>
        /// </summary>
        /// <param name="requestData">The relevant data required to perform a CreateTransaction request</param>
        /// <returns>A full transaction detail response will be returned as per Paygate's documentation</returns>
        TransactionResponse CreateTransaction(CreateTransactionModel requestData);

        /// <summary>
        /// CreateTransaction request will reserve the specified amount on the supplied credit card
        /// <remarks>
        /// This does not move the funds from the credit card account to the merchants account.<br/>
        /// To move the funds you must either process a Settlement transaction or get PayGate to turn on the AutoSettle option
        /// </remarks>
        /// </summary>
        /// <param name="requestData">The relevant data required to perform a CreateTransaction request</param>
        /// <typeparam name="TUserdefined">a class containing custom user defined data</typeparam>
        /// <returns>A full transaction detail response will be returned as per Paygate's documentation</returns>
        TransactionResponse<TUserdefined> CreateTransaction<TUserdefined>(CreateTransactionModel<TUserdefined> requestData) where TUserdefined : class;

        /// <summary>
        /// CreateTokenTransaction request will reserve the specified amount on the supplied credit card
        /// <remarks>
        /// Steps in the payment request process
        /// 1. Customer is redirected to the payment encryption service provider to authorise and authenticate the transaction.
        /// 2. The payment encryption service provider sends the merchant encrypted data for the payment.
        /// 3. The merchant sends the relevant encrypted data to PayGate along with other transaction data.
        /// 4. PayGate decrypts the data received and processes the authorisation to the acquiring bank.
        /// 5. Once the acquiring bank has responded with the authorisation status PayGate sends this response to the merchant.
        /// </remarks>
        /// </summary>
        /// <param name="requestData">The relevant data required to perform a CreateTokenTransaction request</param>
        /// <returns>This is currently a mystery as Paygate does not document this very well</returns>
        TransactionResponse CreateTokenTransaction(CreateTokenTransactionModel requestData);

        /// <summary>
        /// CreateTokenTransaction request will reserve the specified amount on the supplied credit card
        /// <remarks>
        /// Steps in the payment request process
        /// 1. Customer is redirected to the payment encryption service provider to authorise and authenticate the transaction.
        /// 2. The payment encryption service provider sends the merchant encrypted data for the payment.
        /// 3. The merchant sends the relevant encrypted data to PayGate along with other transaction data.
        /// 4. PayGate decrypts the data received and processes the authorisation to the acquiring bank.
        /// 5. Once the acquiring bank has responded with the authorisation status PayGate sends this response to the merchant.
        /// </remarks>
        /// </summary>
        /// <param name="requestData">The relevant data required to perform a CreateTokenTransaction request</param>
        /// <returns>This is currently a mystery as Paygate does not document this very well</returns>
        TransactionResponse<TUserdefined> CreateTokenTransaction<TUserdefined>(CreateTokenTransactionModel<TUserdefined> requestData) where TUserdefined : class;


        /// <summary>
        /// CreateVaultTransaction request will reserve the specified amount on the supplied credit card by passing vaultId and ccv
        /// </summary>
        /// <remarks>
        /// This does not move the funds from the credit card account to the merchants account.<br/>
        /// To move the funds you must either process a Settlement transaction or get PayGate to turn on the AutoSettle option
        /// </remarks>
        /// <param name="requestData">The relevant data required to perform a CreateVaultTransaction request</param>
        /// <returns>A full transaction detail response will be returned as per Paygate's documentation</returns>
        TransactionResponse CreateVaultTransaction(CreateVaultTransactionModel requestData);

        /// <summary>
        /// CreateVaultTransaction request will reserve the specified amount on the supplied credit card by passing vaultId and ccv
        /// </summary>
        /// <remarks>
        /// This does not move the funds from the credit card account to the merchants account.<br/>
        /// To move the funds you must either process a Settlement transaction or get PayGate to turn on the AutoSettle option
        /// </remarks>
        /// <param name="requestData">The relevant data required to perform a CreateVaultTransaction request</param>
        /// <returns>A full transaction detail response will be returned as per Paygate's documentation</returns>
        TransactionResponse<TUserdefined> CreateVaultTransaction<TUserdefined>(CreateVaultTransactionModel<TUserdefined> requestData) where TUserdefined : class;

        #endregion

        #region Query Transaction

        /// <summary>
        /// The QueryTransaction request allows you to query the final status of previously processed transactions.
        /// </summary>
        /// <param name="paygateRequestId">A GUID allocated by PayGate to the request if a redirect is required</param>
        /// <returns>A full transaction detail response will be returned as per Paygate's documentation</returns>
        TransactionResponse QueryTransaction(Guid paygateRequestId);

        /// <summary>
        /// The QueryTransaction request allows you to query the final status of previously processed transactions.
        /// </summary>
        /// <param name="transactionId">A unique reference number assigned by PayGate to the original transaction in the CreateTransaction request</param>
        /// <returns>A full transaction detail response will be returned as per Paygate's documentation</returns>
        TransactionResponse QueryTransaction(int transactionId);


        /// <summary>
        /// The QueryTransaction request allows you to query the final status of previously processed transactions.
        /// </summary>
        /// <param name="reference">This is a unique reference number generated by your system for the original transaction in the CreateTransaction request<br/>
        /// e.g. your Customer, Invoice or OrderNumber
        /// </param>
        /// <returns>A full transaction detail response will be returned as per Paygate's documentation</returns>
        TransactionResponse QueryTransaction(string reference);

        #endregion

        #region Settle Transaction

        /// <summary>
        /// The SettleTransaction request allows the merchant to settle an authorisation where AutoSettle is turned off.
        /// </summary>
        /// <param name="transactionId">A unique reference number assigned by PayGate to the original transaction in the CreateTransaction request</param>
        /// <returns>A full transaction detail response will be returned as per Paygate's documentation</returns>
        TransactionResponse SettleTransaction(int transactionId);

        /// <summary>
        /// The SettleTransaction request allows the merchant to settle an authorisation where AutoSettle is turned off. 
        /// </summary>
        /// <param name="reference">This is a unique reference number generated by your system for the original transaction in the CreateTransaction request<br/>
        /// e.g. your Customer, Invoice or OrderNumber
        /// </param>
        /// <returns>A full transaction detail response will be returned as per Paygate's documentation</returns>
        TransactionResponse SettleTransaction(string reference);

        #endregion

        #region Refund Transaction

        /// <summary>
        /// The RefundTransaction request allows the merchant to refund a transaction that has already been settled.
        /// </summary>
        /// <param name="transactionId">A unique reference number assigned by PayGate to the original transaction in the CreateTransaction request</param>
        /// <param name="amount">Transaction amount in cents. e.g. R32.95 would be specified as 3295</param>
        /// <returns>A full transaction detail response will be returned as per Paygate's documentation</returns>
        TransactionResponse RefundTransaction(int transactionId, int amount);

        /// <summary>
        /// The RefundTransaction request allows the merchant to refund a transaction that has already been settled.
        /// </summary>
        /// <param name="reference">This is a unique reference number generated by your system for the original transaction in the CreateTransaction request<br/>
        /// e.g. your Customer, Invoice or OrderNumber
        /// </param>
        /// <param name="amount">Transaction amount in cents. e.g. R32.95 would be specified as 3295</param>
        /// <returns>A full transaction detail response will be returned as per Paygate's documentation</returns>
        TransactionResponse RefundTransaction(string reference, int amount);

        #endregion

        #region Void Transaction

        /// <summary>
        /// The VoidTransaction request allows merchants to void transactions that are not yet settled or refunded.
        /// <remarks>Settlements and Refunds can only be stopped using the Void request if they have not yet been submitted to the acquiring bank.</remarks>
        /// </summary>
        /// <param name="transactionId">A unique reference number assigned by PayGate to the original transaction in the CreateTransaction request</param>
        /// <param name="transactionType">Transaction type detail of the transaction you want to void. Either Settlement or Refund</param>
        /// <returns>A full transaction detail response will be returned as per Paygate's documentation</returns>
        TransactionResponse VoidTransaction(int transactionId, VoidTransactionTypes transactionType);

        #endregion

        #region Vault Transaction

        /// <summary>
        /// CardVaultRequest will allow merchants to add to data stored by the PayVault tokenisation service.
        /// <remarks>
        /// Please note that currently PayVault only supports the tokenisation of credit card data.
        /// </remarks>
        /// </summary>
        /// <returns>A Status Type will be returned as per Paygate's documentation</returns>
        CardVaultResponse CardVaultRequest(string cardNumber, string cardExpiryDate);

        /// <summary>
        /// LookupVaultRequest will allow merchants to view what is stored by the PayVault tokenisation service.
        /// <remarks>
        /// Please note that currently PayVault only supports the tokenisation of credit card data.
        /// </remarks>
        /// </summary>
        /// <returns>A Status Type will be returned as per Paygate's documentation</returns>
        LookupVaultResponse LookupVaultRequest(string vaultId);

        /// <summary>
        /// DeleteVaultRequest will allow merchants to delete what is stored by the PayVault tokenisation service.
        /// <remarks>
        /// Please note that currently PayVault only supports the tokenisation of credit card data.
        /// </remarks>
        /// </summary>
        /// <returns>A Status Type will be returned as per Paygate's documentation</returns>
        DeleteVaultResponse DeleteVaultRequest(string vaultId);

        #endregion

        #region Helper Methods

        /// <summary>
        /// VerifyTransaction method will verify the checksum returned from PayGate
        /// <remarks>This is usually used to verify a transaction after 3D Secure redirect</remarks>
        /// </summary>
        /// <param name="urlParams">The Url Parameters returned from PayGate</param>
        /// <param name="reference">The unique reference assigned to this transaction</param> //TODO: Add comment about where it's set
        /// <returns>A boolean value indicating if the transaction checksum passed verification</returns>
        bool VerifyTransaction(Dictionary<string, string> urlParams, string reference);

        #endregion
    }
}