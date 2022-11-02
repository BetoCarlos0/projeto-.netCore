using Microsoft.AspNetCore.Mvc;

namespace SistemaChamados.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Display()
        {
            return View();
        }
    }
}
