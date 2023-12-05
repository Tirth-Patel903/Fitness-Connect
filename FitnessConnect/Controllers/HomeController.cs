using FitnessConnect.Areas.Identity.Data;
using FitnessConnect.Interfaces;
using FitnessConnect.Models;
using FitnessConnect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FitnessConnect.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICommonRepository _commonrepo;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        
        public HomeController(ILogger<HomeController> logger, ICommonRepository commonrepo, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _commonrepo = commonrepo;
            _environment = environment;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ContactUs(Contact contact)
        {
            try
            {
                _commonrepo.ContactUsSubmission(contact);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _commonrepo.AddLogger("Home", "ContactUs", ex.Message);
                return null;
            }
        }
        [PermissionAuthorize(Roles = "Admin", Permissions = "ManageContactCreate")]
        public IActionResult ManageContact()
        {
            try
            {
                var contactList = _commonrepo.GetContacts();
                ViewBag.ContactsList = contactList;
                return View();
            }
            catch (Exception ex)
            {
                _commonrepo.AddLogger("Home", "ContactUs", ex.Message);
                throw;
            }
        }
        public IActionResult AboutUs()
        {
            return View();
        }
    }
}