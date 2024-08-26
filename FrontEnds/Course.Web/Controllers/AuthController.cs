using Microsoft.AspNetCore.Mvc;

namespace Course.Web.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult SignIn()
        {
            return View();
        }
    }
}
