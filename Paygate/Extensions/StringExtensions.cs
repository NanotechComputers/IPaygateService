using System.Security.Cryptography;
using System.Text;
using Paygate.Models.Shared;

namespace Paygate.Extensions
{
    public static class StringExtensions
    {
        internal static string ToMd5Hash(this string input)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            foreach (var t in hash)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString();
        }
        
    }
}