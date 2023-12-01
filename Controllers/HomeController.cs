using Microsoft.AspNetCore.Mvc;

namespace almondCoveApi.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
