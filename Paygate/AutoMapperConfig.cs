using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using AutoMapper;
using Paygate.Extensions;
using Paygate.Models.Response;
using Paygate.Models.Shared;

namespace Paygate
{
    public class AutoMapperConfig
    {
        public static void CreateMaps()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<XmlDocument, TransactionResponse>()
                    .ForMember(m => m.TransactionId, opt => opt.MapFrom(src => src.GetXml<int>("TransactionId")))
                    .ForMember(m => m.Reference, opt => opt.MapFrom(src => src.GetXml<string>("Reference")))
                    .ForMember(m => m.AcquirerCode, opt => opt.MapFrom(src => src.GetXml<string>("AcquirerCode")))
                    .ForMember(m => m.StatusName, opt => opt.MapFrom(src => src.GetXml<string>("StatusName").GetStatusName()))
                    .ForMember(m => m.AuthCode, opt => opt.MapFrom(src => src.GetXml<string>("AuthCode")))
                    .ForMember(m => m.PayRequestId, opt => opt.MapFrom(src => Guid.Parse(src.GetXml<string>("PayRequestId"))))
                    .ForMember(m => m.TransactionStatusCode, opt => opt.MapFrom(src => src.GetXml<int>("TransactionStatusCode")))
                    .ForMember(m => m.TransactionStatusDescription, opt => opt.MapFrom(src => src.GetXml<string>("TransactionStatusDescription")))
                    .ForMember(m => m.ResultCode, opt => opt.MapFrom(src => src.GetXml<int>("ResultCode")))
                    .ForMember(m => m.ResultDescription, opt => opt.MapFrom(src => src.GetXml<string>("ResultDescription")))
                    .ForMember(m => m.Currency, opt => opt.MapFrom(src => src.GetXml<string>("Currency")))
                    .ForMember(m => m.Amount, opt => opt.MapFrom(src => src.GetXml<decimal>("Amount")))
                    .ForMember(m => m.RiskIndicator, opt => opt.MapFrom(src => src.GetXml<string>("RiskIndicator")))
                    .ForMember(m => m.Type, opt => opt.MapFrom(src =>
                        new PaymentType
                        {
                            Detail = src.GetXml<string>(new[] {"PaymentType", "Detail"}),
                            Method = src.GetXml<string>(new[] {"PaymentType", "Method"})
                        }))
                    .ForMember(m => m.Redirect, opt => opt.MapFrom(src =>
                        new RedirectOptions
                        {
                            RedirectUrl = src.GetXml<string>(new[] {"Redirect", "RedirectUrl"}),
                            // RedirectUrl = src.GetXml<string>(new []{"PaymentType","Detail"}),
                            UrlParams = src.GetXmlNode("Redirect").GetUrlParams(src),
                            Form = src.GetXmlNode("Redirect").GetUrlParams(src).BuildForm(src.GetXml<string>(new[] {"Redirect", "RedirectUrl"}))
                        }))
                    ;
            });
        }
    }


    internal static class RedirectUrlParams
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
            if(urlParams.Count == 0)
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

    internal static class StatusNames
    {
        public static StatusName GetStatusName(this string statusName)
        {
            if (Items.ContainsKey(statusName))
            {
                return Items[statusName];
            }

            throw new InvalidOperationException("Specified Status Name does not exist");
        }

        private static Dictionary<string, StatusName> Items { get; } = new Dictionary<string, StatusName>
        {
            {"Error", StatusName.Error},
            {"Pending", StatusName.Pending},
            {"Cancelled", StatusName.Cancelled},
            {"Completed", StatusName.Completed},
            {"ValidationError", StatusName.ValidationError},
            {"ThreeDSecureRedirectRequired", StatusName.ThreeDSecureRedirectRequired},
            {"WebRedirectRequired", StatusName.WebRedirectRequired}
        };
    }
}