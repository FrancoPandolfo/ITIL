using Microsoft.AspNetCore.Mvc;

namespace ITIL.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet()]
        public IActionResult Index()
        {
            return View();
        }
    }
}