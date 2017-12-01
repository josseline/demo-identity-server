using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autenticacion.Entidades;

namespace Autenticacion.Repositories
{
    public class UserRepository
    {

        private static readonly List<User> users = new List<User>
        {
            new User { Id = "00000000-0000-0000-0000-000000000000", Username = "juan", Password = "juan" }
        };

        private readonly UserProfileRepository userProfiles;
       

        public UserRepository(UserProfileRepository userProfiles)
        {
            this.userProfiles = userProfiles;
        }

        public User FindByUsername(string username)
        {
            var user = users.FirstOrDefault(x => x.Username == username);

            if (user != null)
            {
                user.Profile = userProfiles.GetSingle(user.Id);
            }

            return users.FirstOrDefault(x => x.Username == username);
        }
        public User GetSingle(string id)
        {
            var user = users.SingleOrDefault(x => x.Id == id);

            if (user != null)
            {
                user.Profile = userProfiles.GetSingle(user.Id);
            }
            
            return user;
        }

        internal User Create(string username, string password, string nombre)
        {
            var id = Guid.NewGuid().ToString();

            var user = new User
            {
                Id = id,
                Username = username,
                Password = password
            };

            users.Add(user);
            userProfiles.Add(user.Id, nombre);

            return user;
        }

    }
}
