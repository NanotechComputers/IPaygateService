using System;
using System.Xml;
using Paygate.Infrastructure.Helpers;
using Paygate.Models.Response;

namespace Paygate.Infrastructure.Extensions
{
    internal static class VaultResponseExtensions
    {
        internal static CardVaultResponse ToCardVaultResponse(this XmlDocument src)
        {
            var error = src.GetXmlError<string>("faultstring");
            var errorDetails = src.GetXmlError<string>("detail");

            if (!string.IsNullOrWhiteSpace(error) && !string.IsNullOrWhiteSpace(errorDetails))
            {
                //Some error from PayGate - throw
                throw new Exception($"An error was returned from PayGate: {error}{Environment.NewLine}Details: {errorDetails}");
            }

            var response = new CardVaultResponse
            {
                StatusName = src.GetXml<string>("StatusName").GetStatusName(),
                VaultId = src.GetXml<string>("VaultId"),
            };

            return response;
        }
        internal static LookupVaultResponse ToLookupVaultResponse(this XmlDocument src)
        {
            var error = src.GetXmlError<string>("faultstring");
            var errorDetails = src.GetXmlError<string>("detail");

            if (!string.IsNullOrWhiteSpace(error) && !string.IsNullOrWhiteSpace(errorDetails))
            {
                //Some error from PayGate - throw
                throw new Exception($"An error was returned from PayGate: {error}{Environment.NewLine}Details: {errorDetails}");
            }

            var response = new LookupVaultResponse
            {
                StatusName = src.GetXml<string>("StatusName").GetStatusName(),
                CardNumber = src.GetXml<string>("CardNumber"),
                CardExpiryDate = src.GetXml<string>("CardExpiryDate"),
            };

            return response;
        }
        internal static DeleteVaultResponse ToDeleteVaultResponse(this XmlDocument src)
        {
            var error = src.GetXmlError<string>("faultstring");
            var errorDetails = src.GetXmlError<string>("detail");

            if (!string.IsNullOrWhiteSpace(error) && !string.IsNullOrWhiteSpace(errorDetails))
            {
                //Some error from PayGate - throw
                throw new Exception($"An error was returned from PayGate: {error}{Environment.NewLine}Details: {errorDetails}");
            }

            var response = new DeleteVaultResponse
            {
                StatusName = src.GetXml<string>("StatusName").GetStatusName(),
            };

            return response;
        }
    }
}