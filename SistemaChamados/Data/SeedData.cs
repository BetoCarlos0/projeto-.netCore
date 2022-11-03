using Microsoft.AspNetCore.Identity;
using SistemaChamados.Data.Identity;
using SistemaChamados.Models.Account;

namespace SistemaChamados.Data
{
    public class SeedData
    {
        public static async Task SeedRolesAsync(UserManager<UserCustom> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(ERoles.Administrador.ToString()));
            await roleManager.CreateAsync(new IdentityRole(ERoles.Operador.ToString()));
            await roleManager.CreateAsync(new IdentityRole(ERoles.Usuario.ToString()));
        }
        public static async Task SeedSuperAdminAsync(UserManager<UserCustom> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new UserCustom
            {
                Name = "user name",
                CpfNumber = "123.123.123.12",
                UserName = "123.123.123.45",
                BirthDate = new DateTime(2000, 08, 21),
                Email = "user@user.com",
                Ramal = 2,
                Department = "DDD",
                Supervisor = "Super",
                PhoneNumber = "111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123456");
                    await userManager.AddToRoleAsync(defaultUser, ERoles.Administrador.ToString());
                }
            }
        }
    }
}
