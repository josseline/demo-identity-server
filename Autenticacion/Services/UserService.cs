using System;
using System.Collections.Generic;
using System.Security.Claims;
using Autenticacion.Entidades;
using Autenticacion.Repositories;

namespace Autenticacion.Services
{
    public class UserService
    {
        private readonly UserRepository users;
        private readonly UserProviderRepository userProviders;
        private readonly HashPasswordService hashPasswordService;

        public UserService(UserRepository users, UserProviderRepository userProviders)
        {
            this.users = users;
            this.userProviders = userProviders;
        }
        public bool ValidateCredentials(string username, string password)
        {
            var user = users.FindByUsername(username);

            if (user == null)
            {
                return false;
            }

            password = hashPasswordService.HashPassword(password);

            return user.Password.Equals(password);
        }

        public User FindByUsername(string username)
        {
            return users.FindByUsername(username);
        }

        /// <summary>
        /// Obtiene el usuario cuando se autentica con google o facebook u otro.
        /// </summary>
        /// <param name="provider">Nombre del proveedor (facebook, google, etc)</param>
        /// <param name="userId">Id del usuario segun la base de datos del proveedor</param>
        /// <returns></returns>
        public User FindByExternalProvider(string provider, string userId)
        {
            var id = userProviders.FindUserId(provider, userId);

            if (id == null)
            {
                return null;
            }

            return users.GetSingle(userId);
        }

        /// <summary>
        /// Registra al usuario en nuestra base de datos cuando se autentica con un proveedor externo (facebook, google)
        /// </summary>
        /// <param name="provider">Nombre del proveedor (facebook, google, etc)</param>
        /// <param name="userId">Id del usuario segun la base de datos del proveedor</param>
        /// <param name="claims">Información del usuario que nos envía el proveedor</param>
        /// <returns></returns>
        public User AutoProvisionUser(string provider, string userId, List<Claim> claims)
        {
            var name = "Nuevo Usuario"; // este debe sacarse de claims;

            // como es externo no tenemos username ni password, entonces generamos uno aleatorio
             // a menos que se le solicite al usuario que ingrese un username y un password para
            // registrarlo en el sistema.

            var username = Guid.NewGuid().ToString(); 
            var password = Guid.NewGuid().ToString();

            var user = users.Create(username, password, name);
            userProviders.Add(user.Id, provider, userId);

            return user;
        }
    }
}
