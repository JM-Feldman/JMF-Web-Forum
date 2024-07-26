using Microsoft.AspNetCore.Mvc;

namespace JMF_Web_Forum_API.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
