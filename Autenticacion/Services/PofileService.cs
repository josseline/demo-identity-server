using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Autenticacion.Repositories;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Newtonsoft.Json;

namespace Autenticacion.Services
{

    // mas detalles aquí: http://docs.identityserver.io/en/release/reference/profileservice.html
    public class PofileService : IProfileService
    {
        private readonly UserProfileRepository userProfiles;

        public PofileService(UserProfileRepository userProfiles)
        {
            this.userProfiles = userProfiles;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var userId = context.Subject.GetSubjectId();
            Debug.WriteLine($"Usuario del cual se quiere el perfil: {userId}");
            Debug.WriteLine($"Informacion solicitada: {string.Join(", ", context.RequestedClaimTypes)}");


            var profile = userProfiles.GetSingle(userId);

            if (profile != null)
            {
                var address = new { street_address = profile.Street, locality = profile.Locality, postal_code = profile.PostalCode, country = profile.Country };

                context.IssuedClaims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.Name, profile.Name),
                    new Claim(JwtClaimTypes.GivenName, profile.GivenName),
                    new Claim(JwtClaimTypes.FamilyName, profile.FamilyName),
                    new Claim(JwtClaimTypes.Email, profile.Email),
                    new Claim(JwtClaimTypes.EmailVerified, profile.EmailVerified.ToString(), ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, profile.Website),
                    new Claim(JwtClaimTypes.Address, JsonConvert.SerializeObject(address), IdentityServer4.IdentityServerConstants.ClaimValueTypes.Json)
                };
            }

            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            Debug.WriteLine($"Usuario que se quiere saber si está activo: {context.Subject.GetSubjectId()}");
            context.IsActive = true;
            return Task.CompletedTask;
        }
    }
}
