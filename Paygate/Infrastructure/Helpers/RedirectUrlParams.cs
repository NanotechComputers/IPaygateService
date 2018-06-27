using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Paygate.Infrastructure.Helpers
{
    internal static class RedirectUrlParameterHelpers
    {
        internal static Dictionary<string, string> GetUrlParams(this XmlNode node, XmlDocument doc)
        {
            var nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("SOAP-ENV", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("ns2", "http://www.paygate.co.za/PayHOST");
            const string ns = "ns2";

            var dict = new Dictionary<string, string>();

            if (node == null)
            {
                return dict;
            }

            var elements = node.SelectNodes($"//{ns}:UrlParams", nsmgr);
            if (elements == null)
            {
                return dict;
            }

            for (int i = 0; i < elements.Count; i++)
            {
                var key = elements?.Item(i)["ns2:key"]?.InnerText ?? "";
                var value = elements?.Item(i)["ns2:value"]?.InnerText ?? "";
                dict.Add(key, value);
            }


            return dict;
        }


        internal static string BuildForm(this Dictionary<string, string> urlParams, string redirectUrl)
        {
            if (urlParams.Count == 0)
            {
                return "";
            }

            var sb = new StringBuilder();
            sb.AppendLine($@"<form name=""PayGate3DSecureForm"" id=""PayGate3DSecureForm"" action=""{redirectUrl}"" method=""post"">");

            foreach (var param in urlParams)
            {
                sb.AppendLine($@"<input type=""hidden"" name=""{param.Key}"" value=""{param.Value}"" />");
            }

            sb.AppendLine(@"<noscript><input type=""submit""></noscript>");
            sb.AppendLine(@"</form>");
            return sb.ToString();
        }
    }
}