using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaChamados.Data.Identity;
using SistemaChamados.Models.Account;
using System.Data;

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

        public IActionResult Home()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ListUsers()
        {
            ViewBag.Roles = _roleManager.Roles.ToList();

            return View(await _userManager.Users.ToListAsync());
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult CreateUser()
        {
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
                return RedirectToAction("ListUsers", "Dashboard");
            }

            //await GetRole();
            ViewBag.Roles = _roleManager.Roles.ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string? id)
        {
            ViewBag.Roles = _roleManager.Roles.ToList();

            if (id == null) return RedirectToAction("ListUsers");

            var user = await _userManager.FindByIdAsync(id);

            var result = new RegisterViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                Cpf = user.CpfNumber,
                BirthDate = user.BirthDate,
                Department = user.Department,
                Supervisor = user.Supervisor,
                Phone = user.PhoneNumber,
                Ramal = user.Ramal,
                Email = user.Email,
            };
            ViewBag.RoleUserEdit = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(RegisterViewModel model)
        {
            ViewBag.Roles = _roleManager.Roles.ToList();

            var user = await _userManager.FindByIdAsync(model.Id);

            user.Name = model.Name;
            user.CpfNumber = model.Cpf;
            user.BirthDate = model.BirthDate;
            user.Department = model.Department;
            user.Supervisor = model.Supervisor;
            user.PhoneNumber = model.Phone;
            user.Ramal = model.Ramal;
            user.Email = model.Email;

            //var result = await _userManager.UpdateAsync(userCustom);
            if ((await _userManager.UpdateAsync(user)).Succeeded)
            {
                if (model.Role != (await _userManager.GetRolesAsync(user)).FirstOrDefault())
                    await _userManager.AddToRoleAsync(user, model.Role);

                return RedirectToAction("ListUsers");
            }

            return View(model);
        }

        public async Task<IActionResult> EditPassword()
        {
            return View();
        }

        public async Task<IActionResult> Calls(CallsViewModel model)
        {
            return View(model);
        }
    }
}
