using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autenticacion.Repositories
{
    public class UserProviderRepository
    {
        private static readonly Dictionary<string, string> userProviders = new Dictionary<string, string>();
        public void Add(string userId, string provider, string providerUserId)
        {
            userProviders.Add($"{provider}-{providerUserId}", userId);
        }

        public string FindUserId(string provider, string providerUserId)
        {
            var key = $"{provider}-{providerUserId}";
            if (userProviders.ContainsKey(key))
            {
                return userProviders[key];
            }

            return null;
        }
    }
}
