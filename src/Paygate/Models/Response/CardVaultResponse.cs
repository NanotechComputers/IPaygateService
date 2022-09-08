using Paygate.Models.Shared;

namespace Paygate.Models.Response
{
    public class CardVaultResponse
    {
        public string VaultId { get; set; }
        public StatusName StatusName { get; set; }
    }
}