using Paygate.Models.Shared;

namespace Paygate.Models.Response
{
    public class LookupVaultResponse
    {
        public string CardNumber { get; set; }
        public string CardExpiryDate { get; set; }
        public StatusName StatusName { get; set; }
    }
}