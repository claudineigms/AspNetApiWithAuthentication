using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ApiWithAuthentication.Models
{
    public static class Inicializador
    {
        public static void InicializarPerfis(RoleManager<IdentityRole<int>> roleManager)
        {
            if (!roleManager.RoleExistsAsync("adminsitrador").Result)
            {
                var perfil = new IdentityRole<int>();
                perfil.Name = "administrador";
                roleManager.CreateAsync(perfil).Wait();
            }
        }

        private static void InicializarUsuarios(UserManager<UserModel> userManager)
        {
            if (userManager.FindByNameAsync("admin@email.com").Result == null)
            {
                var usuario = new UserModel();
                usuario.UserName = "admin@email.com";
                usuario.Email = "admin@email.com";
                usuario.NomeCompleto = "Administrador do sistema";
                usuario.DataNascimento = "01/02/2003";
                usuario.PhoneNumber = "999999999";
                usuario.CPF = "0000000000";
                var resultado = userManager.CreateAsync(usuario, "123Aa@").Result;

                if (resultado.Succeeded)
                {
                    userManager.AddToRoleAsync(usuario, "administrador").Wait();
                }
            }
        }

        public static async Task InicializarIdentity(RoleManager<IdentityRole<int>> roleManager,UserManager<UserModel> userManager)
        {
            InicializarPerfis(roleManager);
            InicializarUsuarios(userManager);
        }
    }
}