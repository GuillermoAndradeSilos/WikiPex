using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WikiPex.Areas.Administrador.Controllers
{
    [Area("Administrador")]
    [Authorize]
    public class HomeController : Controller
    {
        [Route("/Administrador")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
