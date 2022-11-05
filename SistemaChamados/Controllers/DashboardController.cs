using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaChamados.Data.Identity;
using SistemaChamados.Models.Account;

namespace SistemaChamados.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly UserManager<UserCustom> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly SignInManager<UserCustom> _signInManager;

        public DashboardController(UserManager<UserCustom> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Home()
        {
            await GetRole();

            return View();
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ListUsers()
        {
            await GetRole();
            ViewBag.Roles = _roleManager.Roles.ToList();

            var user = await _userManager.Users.ToListAsync();
            return View(user);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> CreateUser()
        {
            await GetRole();
            ViewBag.Roles = _roleManager.Roles.ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new UserCustom()
            {
                Name = model.Name,
                UserName = model.Cpf,
                CpfNumber = model.Cpf,
                BirthDate = model.BirthDate,
                Department = model.Department,
                Supervisor = model.Supervisor,
                PhoneNumber = model.Phone,
                Ramal = model.Ramal,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.Role);
                return RedirectToAction("Home", "Dashboard");
            }

            await GetRole();
            ViewBag.Roles = _roleManager.Roles.ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string? id)
        {
            await GetRole();
            ViewBag.Roles = _roleManager.Roles.ToList();

            if (id == null) return RedirectToAction("ListUsers");

            var user = await _userManager.FindByIdAsync(id);

            var result = new RegisterViewModel()
            {
                Name = user.Name,
                Cpf = user.CpfNumber,
                BirthDate = user.BirthDate,
                Department = user.Department,
                Supervisor = user.Supervisor,
                Phone = user.PhoneNumber,
                Ramal = user.Ramal,
                Email = user.Email,
            };

            return View(result);
        }

        public async Task<IActionResult> Calls(CallsViewModel model)
        {
            return View(model);
        }

        private async Task GetRole()
        {
            var user = await _userManager.GetUserAsync(User);
            var rolename = await _userManager.GetRolesAsync(user);
            ViewBag.Rolename = rolename[0];
        }
    }
}
