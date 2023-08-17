using Microsoft.AspNetCore.Mvc;

namespace ServiceLocator.Controllers
{
    [Route("/method-injection")]
    public class MethodInjectionController : Controller
    {
        public IActionResult GetUsingMethodInjection([FromServices] IMyService myService)
        {
            ArgumentNullException.ThrowIfNull(myService, nameof(myService));
            myService.Execute();
            return Ok("Success!");
        }
    }
}
    