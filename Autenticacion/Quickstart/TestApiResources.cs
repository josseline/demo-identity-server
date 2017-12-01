using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityModel;

namespace Autenticacion.Quickstart
{
    public static class TestApiResources
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API") { UserClaims = { JwtClaimTypes.Address }}
            };
        }
    }
}
