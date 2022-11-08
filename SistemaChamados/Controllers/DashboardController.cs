using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaChamados.Data;
using SistemaChamados.Data.Identity;
using SistemaChamados.Models;
using SistemaChamados.Models.Account;
using System.Data;

namespace SistemaChamados.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly UserManager<UserCustom> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SistemaDbContext _context;
        private readonly SignInManager<UserCustom> _signInManager;

        public DashboardController(UserManager<UserCustom> userManager, RoleManager<IdentityRole> roleManager, SistemaDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = dbContext;
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

            if ((await _userManager.UpdateAsync(user)).Succeeded)
            {
                if (model.Role != (await _userManager.GetRolesAsync(user)).FirstOrDefault())
                {
                    var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

                    await _userManager.RemoveFromRoleAsync(user, role);
                    await _userManager.AddToRoleAsync(user, model.Role);
                }
                return RedirectToAction("ListUsers");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ChangePassword(string id)
        {
            var change = new ChangePasswordViewModel()
            {
                UserId = id
            };
            return View(change);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
                return RedirectToAction("ListUsers");
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            ViewBag.GetUserRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if ((await _userManager.DeleteAsync(user)).Succeeded)
                return RedirectToAction("ListUsers");

            return View(id);
        }

        public async Task<IActionResult> ListCalls()
        {
            var calls = await _context.Calls.ToListAsync();

            return View(calls);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCall()
        {            
            ViewBag.Status = Enum.GetNames(typeof(Status));

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCall([Bind("AspNetUsersId, Name, Ramal, Phone, Status, Decription, Title")] Calls model)
        {
            ViewBag.Status = Enum.GetNames(typeof(Status));

            if (ModelState.IsValid)
            {
                await _context.AddAsync(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("ListCalls");
            }

            return View(model);
        }
    }
}
