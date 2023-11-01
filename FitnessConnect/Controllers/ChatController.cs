using FitnessConnect.Areas.Identity.Data;
using FitnessConnect.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitnessConnect.Controllers
{
    public class ChatController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUsersRepository _usersrepo;
        private readonly IChatboxRepository _chatboxrepo;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public ChatController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUsersRepository usersrepo, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, IChatboxRepository chatboxrepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _environment = environment;
            _usersrepo = usersrepo;
            _chatboxrepo = chatboxrepo;
        }
        public async Task<IActionResult> Index()
        {
            string currentUserName = User.Identity.Name;
            IdentityUser currentUser = await _userManager.FindByNameAsync(currentUserName);

            // Now you can use currentUser to access user properties
            string userEmail = currentUser.Email;
            var userList = _usersrepo.getUsers().Where(a=>a.Email != userEmail).ToList();
            ViewBag.userList= userList;
            ViewBag.currentUser = userEmail;
            return View();
        }
        public IActionResult getMessages(string id)
        {
            try
            {
                var allMessages = _chatboxrepo.GetAllMessages(id);
                return Json(allMessages);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        public IActionResult saveMessages(string Message,string RecieverId)
        {
            try
            {
                MessageModel model = new MessageModel();
                model.Message = Message;
                model.RecieverId = RecieverId;
                _chatboxrepo.saveMessages(model);
                return View();
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
    }
}
