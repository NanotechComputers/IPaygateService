namespace Paygate.Models.Shared
{
    public enum StatusName
    {
        Error,
        Pending,
        Cancelled,
        Completed,
        ValidationError,
        ThreeDSecureRedirectRequired,
        WebRedirectRequired
    }
    
}