using Microsoft.AspNetCore.Mvc;

namespace ServiceLocator.Controllers
{
    public class MethodInjectionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
