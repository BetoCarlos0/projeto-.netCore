using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using VCommerce.IdentityServer.Configuration;
using VCommerce.IdentityServer.Data;

namespace VCommerce.IdentityServer.SeedDatabase;

public class DatabaseIdentityServerInitializer : IDatabaseSeedInitializer
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DatabaseIdentityServerInitializer(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void InitializeSeedRoles()
    {
        if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Admin).Result)
        {
            IdentityRole identityRole = new IdentityRole();
            identityRole.Name = IdentityConfiguration.Admin;
            identityRole.NormalizedName = IdentityConfiguration.Admin.ToUpper();
            _roleManager.CreateAsync(identityRole).Wait();
        }

        if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Client).Result)
        {
            IdentityRole identityRole = new IdentityRole();
            identityRole.Name = IdentityConfiguration.Client;
            identityRole.NormalizedName = IdentityConfiguration.Client.ToUpper();
            _roleManager.CreateAsync(identityRole).Wait();
        }
    }

    public void InitializeSeedUsers()
    {
        if (_userManager.FindByEmailAsync("admin1@com.br").Result == null)
        {
            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "admin1",
                NormalizedUserName = "ADMIN1",
                Email = "admin1@com.br",
                NormalizedEmail = "ADMIN1@COM.BR",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "85 4545-5353",
                FirstName = "Usuario",
                LastName = "Admin1",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult result = _userManager.CreateAsync(admin, "Senha@123").Result;
            if (result.Succeeded)
            {
                _userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin).Wait();


                var adminClaims = _userManager.AddClaimsAsync(admin, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
                }).Result;
            }
        }

        if (_userManager.FindByEmailAsync("client1@com.br").Result == null)
        {
            ApplicationUser client = new ApplicationUser()
            {
                UserName = "client1",
                NormalizedUserName = "CLIENT1",
                Email = "client1@com.br",
                NormalizedEmail = "CLIENT1@COM.BR",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumber = "85 4545-0000",
                FirstName = "Usuario",
                LastName = "Client1",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult resultClient = _userManager.CreateAsync(client, "Senha@123").Result;
            if (resultClient.Succeeded)
            {
                _userManager.AddToRoleAsync(client, IdentityConfiguration.Client).Wait();

                var clientClaims = _userManager.AddClaimsAsync(client, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                    new Claim(JwtClaimTypes.GivenName, client.FirstName),
                    new Claim(JwtClaimTypes.FamilyName, client.LastName),
                    new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
                }).Result;
            }
        }
    }
}
