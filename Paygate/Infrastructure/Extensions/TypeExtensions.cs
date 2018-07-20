using System;

namespace Paygate.Infrastructure.Extensions
{
    internal static class TypeExtensions
    {
        internal static bool IsPrimitiveType<T>(T obj)
        {
            var type = obj.GetType();
            return (type == typeof(object) || Type.GetTypeCode(type) != TypeCode.Object);
        }
    }
}
