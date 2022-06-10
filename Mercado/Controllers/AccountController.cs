using Mercado.Data;
using Mercado.Data.Identity;
using Mercado.Models.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Mercado.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserCustom> _userManager;
        private readonly SignInManager<UserCustom> _signInManager;
        private readonly MercadoDbContext _mercadoDb;

        public AccountController(UserManager<UserCustom> userManager,
            SignInManager<UserCustom> signInManager,
            MercadoDbContext mercadoDb)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mercadoDb = mercadoDb;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                if (Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("index", "home");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new UserCustom
            {
                Name = model.Name,
                Email = model.Email,
                UserName = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("index", "home");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsEmailUse(string email)
        {
            var user = await _mercadoDb.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));

            if (user == null)
                return Json(true);
            else
                return Json($"{email} já utilizado");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
