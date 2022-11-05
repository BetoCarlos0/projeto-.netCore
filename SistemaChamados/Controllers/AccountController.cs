using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemaChamados.Data;
using SistemaChamados.Data.Identity;
using SistemaChamados.Models.Account;
using System.Threading.Tasks;


namespace SistemaChamados.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserCustom> _userManager;
        private readonly SignInManager<UserCustom> _signInManager;
        private readonly SistemaDbContext _context;

        public AccountController(UserManager<UserCustom> userManager,
            SignInManager<UserCustom> signInManager, SistemaDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        [HttpGet(Name = "Login")]
        public async Task<IActionResult> Login()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null) return RedirectToAction("Home", "DashBoard");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _signInManager.PasswordSignInAsync(model.CpfNumber, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                if (Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Home", "Dashboard");
            }

            return View(model);
        }
        

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsEmailUse(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

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
