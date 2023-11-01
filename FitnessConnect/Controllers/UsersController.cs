using Microsoft.AspNetCore.Mvc;

namespace FitnessConnect.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
