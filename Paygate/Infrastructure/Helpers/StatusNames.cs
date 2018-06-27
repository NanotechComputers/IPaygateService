using System;
using System.Collections.Generic;
using Paygate.Models.Shared;

namespace Paygate.Infrastructure.Helpers
{
    internal static class StatusNameHelpers
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