using FitnessConnect.Areas.Identity.Data;
using FitnessConnect.Interfaces;
using FitnessConnect.Service.Interface;
using FitnessConnect.Services;
using FitnessConnect.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitnessConnect.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersRepository _usersrepo;
        private UserManager<ApplicationUser> _userManager;
        private readonly ILoggerService _loggerRepository;
        public UsersController(ILoggerService loggerRepository, IUsersRepository usersrepo, UserManager<ApplicationUser> userManager)
        {
            _loggerRepository = loggerRepository;
            _usersrepo = usersrepo;
        }
        [PermissionAuthorize(Roles = "Admin", Permissions = "UserRead")]
        public IActionResult Index()
        {
            var users = _usersrepo.GetUsers().ToList();
            ViewBag.Users = users;
            return View();
        }

        [HttpPost]
        [PermissionAuthorize(Roles = "Admin", Permissions = "UserDelete")]
        public IActionResult DeleteUser(string id)
        {
            try
            {
                ApplicationUser user = _usersrepo.GetUserById(id);
                if (user != null)
                {
                    var status = _usersrepo.DeleteUser(user.Id);
                    if (status == true)
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false });
                    }

                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
