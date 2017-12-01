using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autenticacion.Entidades
{

    [Table("Users")]
    public class User
    {
        [Key()]
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public Profile Profile { get; set; }

    }
}
