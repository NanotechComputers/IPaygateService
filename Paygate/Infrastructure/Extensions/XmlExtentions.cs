using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Paygate.Infrastructure.Extensions
{
    internal static class XmlExtentions
    {
        internal static T GetXml<T>(this XmlDocument document, string nodeName)
        {
            var nsmgr = new XmlNamespaceManager(document.NameTable);
            nsmgr.AddNamespace("SOAP-ENV", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("ns2", "http://www.paygate.co.za/PayHOST");
            const string ns = "ns2";
            var element = document.SelectSingleNode($"//{ns}:{nodeName}", nsmgr)?.InnerText ?? "";
            if (string.IsNullOrWhiteSpace(element))
            {
                return default(T);
            }

            return (T) Convert.ChangeType(element, typeof(T));
        }
        
        internal static T GetXmlError<T>(this XmlDocument document, string nodeName)
        {
            var element = document.SelectSingleNode($"//{nodeName}")?.InnerText ?? "";
            if (string.IsNullOrWhiteSpace(element))
            {
                return default(T);
            }

            return (T) Convert.ChangeType(element, typeof(T));
        }

        internal static XmlNode GetXmlNode(this XmlDocument document, string nodeName)
        {
            var nsmgr = new XmlNamespaceManager(document.NameTable);
            nsmgr.AddNamespace("SOAP-ENV", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("ns2", "http://www.paygate.co.za/PayHOST");
            const string ns = "ns2";
            var element = document.SelectSingleNode($"//{ns}:{nodeName}", nsmgr);
            return element;
        }

        internal static T GetXml<T>(this XmlDocument document, IEnumerable<string> nodeNames)
        {
            var nsmgr = new XmlNamespaceManager(document.NameTable);
            nsmgr.AddNamespace("SOAP-ENV", "http://schemas.xmlsoap.org/soap/envelope/");
            nsmgr.AddNamespace("ns2", "http://www.paygate.co.za/PayHOST");
            const string ns = "ns2";
            var nodeName = nodeNames.Aggregate((i, j) => $"{i}//{ns}:{j}");
            var element = document.SelectSingleNode($"//{ns}:{nodeName}", nsmgr)?.InnerText ?? "";
            if (string.IsNullOrWhiteSpace(element))
            {
                return default(T);
            }

            return (T) Convert.ChangeType(element, typeof(T));
        }
    }
}