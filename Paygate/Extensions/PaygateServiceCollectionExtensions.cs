using System;
using Microsoft.Extensions.DependencyInjection;

namespace Paygate.Extensions
{
    // ReSharper disable once UnusedMember.Global
    public static class PaygateServiceCollectionExtensions
    {
        // ReSharper disable once UnusedMember.Global
        public static IServiceCollection AddPaygate(this IServiceCollection collection, Action<PaygateServiceOptions> setupAction)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (setupAction == null) throw new ArgumentNullException(nameof(setupAction));

            collection.Configure(setupAction);
            AutoMapperConfig.CreateMaps();

            return collection.AddTransient<IPaygateService, PaygateService>();
        }
    }
}