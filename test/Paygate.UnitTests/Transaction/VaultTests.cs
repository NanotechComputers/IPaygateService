using Paygate.Models.Shared;
using Xunit;

namespace Paygate.UnitTests.Vault
{
    public class VaultTests
    {
        PaygateService _paygateService;
        public VaultTests()
        {
            const string url = "https://secure.paygate.co.za/payhost/process.trans";
            const string merchantId = "10011064270"; //No 3d secure redirect on this merchantId
            const string merchantSecret = "test"; //This is the merchantSecret paired with above merchantId
            _paygateService = new PaygateService(url, merchantId, merchantSecret);
        }

        [Theory]
        [InlineData("4661184492608207", "122026")]
        public void CardVaultRequestTest(string cardNumber, string cardExpiryDate)
        {
            var response = _paygateService.CardVaultRequest(cardNumber, cardExpiryDate);
            Assert.True(response.StatusName == StatusName.Completed);
        }

        [Theory]
        [InlineData("2b6629f0-a15b-4c4c-8767-adeb0dbc9456")]
        public void LookupVaultRequestTest(string vaultId)
        {
            var response = _paygateService.LookupVaultRequest(vaultId);
            Assert.True(response.StatusName == StatusName.Completed);
        }
    }
}