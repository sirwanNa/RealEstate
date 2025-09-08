using Microsoft.AspNetCore.Mvc;

namespace RealEstate.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
