using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autenticacion.Entidades
{
    public class Profile
    {
        public string Name { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
        public string Website { get; set; }

        public string Street { get; set; }
        public string Locality { get; set; }
        public int PostalCode { get; set; }
        public string Country { get; set; }

    }
}
