using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemaChamados.Data.Identity;

namespace SistemaChamados.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly UserManager<UserCustom> _userManager;
        private readonly SignInManager<UserCustom> _signInManager;

        public DashboardController(UserManager<UserCustom> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet(Name = "Home")]
        public async Task<IActionResult> Home()
        {
            var user = await _userManager.GetUserAsync(User);
            var rolename = await _userManager.GetRolesAsync(user);
            ViewBag.Rolename = rolename[0];

            return View();
        }
    }
}
