using System;

namespace Paygate.Infrastructure.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsPrimitiveType<T>(T obj)
        {
            var type = obj.GetType();
            return (type == typeof(object) || Type.GetTypeCode(type) != TypeCode.Object);
        }
    }
}
