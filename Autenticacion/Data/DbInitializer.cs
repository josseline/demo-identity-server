using Autenticacion.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autenticacion.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AutenticacionContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            var users = new User[]
            {
                new User{Id = "U1", Username = "userTest", Password = "userTest"},
                 new User{Id = "U2", Username = "userTest2", Password = "userTest2"}
            };

            foreach (User u in users)
            {
                context.Users.Add(u);
            }

            context.SaveChanges();

       
        }
    }
}
