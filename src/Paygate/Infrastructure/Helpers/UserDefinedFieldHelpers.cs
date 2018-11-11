using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Paygate.Infrastructure.Extensions;

namespace Paygate.Infrastructure.Helpers
{

    internal static class UserDefinedFieldHelpers
    {
        internal static TUserdefined GetUserDefinedFields<TUserdefined>(this XmlNode node, XmlDocument doc) where TUserdefined : class
        {
            // TODO: WTF are we doing, this is horrible. Improve this code
            var nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("SOAP-ENV", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("ns2", "http://www.paygate.co.za/PayHOST");

            var dict = new Dictionary<string, string>();

            var elements = node.ChildNodes;
            var key = "";
            var value = "";
            for (var i = 0; i < elements.Count; i++)
            {
                if (i % 2 != 0)
                {
                    //This is the value
                    value = elements[i]?.InnerText ?? "";
                    dict.Add(key, value);
                }
                else
                {
                    key = elements[i].InnerText ?? "";
                }
            }


            var ordered = dict.OrderBy(x => x.Key);
            var compressed = new StringBuilder();
            foreach (var x in ordered)
            {
                compressed.Append(x.Value);
            }

            var decompressed = compressed.ToString().DecompressString();
            return StringExtensions.Deserialize<TUserdefined>(decompressed);
        }
    }
}