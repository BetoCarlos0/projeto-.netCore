using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public DashboardController(UserManager<UserCustom> userManager, RoleManager<IdentityRole> roleManager, SistemaDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = dbContext;
        }

        public async Task<IActionResult> Home()
        {
            var calls = _context.Calls.AsQueryable();

            if (User.IsInRole("Administrador"))
            {
                var viewData = new Viewdata()
                {
                    Count = calls.Count(),
                    Aberto = calls.Where(x => x.Status.Equals("Aberto")).Count(),
                    Andamnto = calls.Where(x => x.Status.Equals("Andamento")).Count(),
                    Fechado = calls.Where(x => x.Status.Equals("Fechado")).Count(),
                };
                return View(viewData);
            }
            if (User.IsInRole("Operador"))
            {
                var user = await _userManager.GetUserAsync(User);
                calls = calls.Where(x => x.Operador.Equals(user.Name));

                var viewData = new Viewdata()
                {
                    Count = calls.Count(),
                    Andamnto = calls.Where(x => x.Status.Equals("Andamento")).Count(),
                    Fechado = calls.Where(x => x.Status.Equals("Fechado")).Count(),
                };
                return View(viewData);
            }
            return RedirectToAction("ListCalls");
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ListUsers()
        {
            ViewBag.Roles = _roleManager.Roles.ToList();

            return View(await _userManager.Users.ToListAsync());
        }

        public async Task<IActionResult> detailsUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            user.Role = role;
            return View(user);
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult CreateUser()
        {
            ViewBag.Roles = _roleManager.Roles.ToList();

            return View();
        }

        [Authorize(Roles = "Administrador")]
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

            ViewBag.Roles = _roleManager.Roles.ToList();

            return View(model);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public async Task<IActionResult> EditUser(string? id)
        {
            ViewBag.Roles = _roleManager.Roles.ToList();

            var user = await _userManager.FindByIdAsync(id);

            user.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            return View(user);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> EditUser([Bind("Id, Name, CpfNumber, Email, BirthDate, Role, Supervisor, Department, Ramal, PhoneNumber, LockoutEnabled")] UserCustom model)
        {
            ViewBag.Roles = _roleManager.Roles.ToList();

            var user = await _userManager.FindByIdAsync(model.Id);

            if (model.LockoutEnabled == true)
            {
                user.LockoutEnd = new DateTime(3000,01,01);
            }
            else {
                user.LockoutEnd = (DateTime?)null;
            }

            user.Name = model.Name;
            user.UserName = model.CpfNumber;
            user.CpfNumber = model.CpfNumber;
            user.BirthDate = model.BirthDate;
            user.Department = model.Department;
            user.Supervisor = model.Supervisor;
            user.PhoneNumber = model.PhoneNumber;
            user.Ramal = model.Ramal;
            user.Email = model.Email;
            user.LockoutEnabled = model.LockoutEnabled;

            if ((await _userManager.UpdateAsync(user)).Succeeded)
            {
                if (model.Role != (await _userManager.GetRolesAsync(user)).FirstOrDefault())
                {
                    var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

                    await _userManager.RemoveFromRoleAsync(user, role);
                    await _userManager.AddToRoleAsync(user, model.Role);
                }
                return RedirectToAction(nameof(EditUser), new {user.Id});
            }
            return View(model);
        }


        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserCustom model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.Password);
            if (result.Succeeded)
                return RedirectToAction(nameof(EditUser), new { model.Id });
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return RedirectToAction(nameof(EditUser), new { model.Id });
        }

        public async Task<IActionResult> ListCalls(int searcheId)
        {
            var calls = _context.Calls.AsQueryable();
            var user = await _userManager.GetUserAsync(User);

            if (User.IsInRole("Operador"))
            {
                calls = calls.Where(x => x.Operador == user.Name || x.Operador == string.Empty).AsQueryable();

            }
            if (User.IsInRole("Usuario"))
            {
                calls = _context.Calls.Where(x => x.AspNetUsersId == user.Id).AsQueryable();
            }
            if (searcheId != 0)
            {
                ViewData["CurrentFilter"] = searcheId;
                var call = calls.Where(s => s.CallsId.Equals(searcheId));
                return View(call);
            }
            else
            {
                ViewData["CurrentFilter"] = null;
                return View(calls.AsEnumerable());
            }
        }

        public async Task<IActionResult> DetailsCalls(int id)
        {
            var call = await _context.Calls.FindAsync(id);
            return View(call);
        }

        [HttpGet]
        public  IActionResult CreateCall()
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

                var Getuser = await _userManager.FindByIdAsync(model.AspNetUsersId);

                if ((await _userManager.GetRolesAsync(Getuser)).FirstOrDefault() != "Usuario")
                    return RedirectToAction("ListCalls");
                else
                    return RedirectToAction("Home");

            }

            return View(model);
        }

        [Authorize(Roles = "Administrador, Operador")]
        [HttpGet]
        public async Task<IActionResult> EditCall(int id)
        {
            ViewBag.Status = Enum.GetNames(typeof(Status));
            var call = await _context.Calls.FindAsync(id);
            var operadores = await _userManager.GetUsersInRoleAsync("Operador");

            ViewBag.Operador = (await _userManager.GetUsersInRoleAsync("Operador")).Select(x => x.Name).ToList();

            return View(call);
        }

        [Authorize(Roles = "Administrador, Operador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCall([Bind("CallsId, AspNetUsersId, Name, Ramal, Phone, Status, Decription, Title, Solution, Operador")] Calls model)
        {
            ViewBag.Status = Enum.GetNames(typeof(Status));

            if (model.Operador == "Selecionar o Operador")
                model.Operador = string.Empty;

            if (ModelState.IsValid)
            {
                _context.Update(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("ListCalls");
            }
            return View(model);
        }
    }
}
