using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autenticacion.Entidades;

namespace Autenticacion.Repositories
{
    public class UserProfileRepository
    {
        private static readonly Dictionary<string, Profile> userProfiles = new Dictionary<string, Profile>
        {
            {
                "00000000-0000-0000-0000-000000000000",
                new Profile
                {
                    Name = "Juancho",
                    GivenName = "Juan",
                    FamilyName = "Pérez",
                    Email = "juanp@email.com",
                    EmailVerified = true,
                    Website = "http://jp.net",
                    Street = "One Hacker Way",
                    Locality = "Heidelberg",
                    PostalCode = 69118,
                    Country = "Germany"
                }
            }
        };

        public Profile GetSingle(string userId)
        {
            return userProfiles.ContainsKey(userId) ? userProfiles[userId] : null;
        }

        internal void Add(string userId, string nombre)
        {
            userProfiles.Add(userId, new Profile { Name = nombre });
        }
    }
}
