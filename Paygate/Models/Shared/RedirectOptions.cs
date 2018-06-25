using System.Collections.Generic;

namespace Paygate.Models.Shared
{
    public class RedirectOptions
    {
        public string RedirectUrl { get; set; }
        public Dictionary<string, string> UrlParams { get; set; }
        public string Form { get; set; }
    }
}