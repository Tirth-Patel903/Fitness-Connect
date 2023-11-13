using FitnessConnect.Data.Models;
using FitnessConnect.Service.Interface;
using FitnessConnect.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security;
using System.Security.Claims;

namespace FitnessConnect.Controllers
{
    public class RolesAndPermissions : Controller
    {
        private readonly ILoggerService _loggerRepository;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IRolePermission _rolePermissionRepository;

        public RolesAndPermissions(ILoggerService loggerRepository, RoleManager<ApplicationRole> roleManager, IPermissionRepository permissionRepository, IRolePermission rolePermissionRepository)
        {
            _loggerRepository = loggerRepository;
            _roleManager = roleManager;
            _permissionRepository = permissionRepository;
            _rolePermissionRepository = rolePermissionRepository;
        }
        //[PermissionAuthorize(Roles = "Senior Accountant")]
        public IActionResult Index()
        {
            try
            {
                var roles = _roleManager.Roles.ToList();
                ViewBag.Roles = roles;
                return View();
            }
            catch (Exception ex)
            {
                var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _loggerRepository.Insert(UserId, "RolesAndPermissions", "Index", ex);
                return View(ex);
            }
        }

        [HttpPost]
        //[PermissionAuthorize(Roles = "Senior Accountant")]
        public IActionResult Index(IFormCollection fc)
        {
            try
            {
                var permissions = _permissionRepository.GetAllPermission();
                var rolePermissions = _rolePermissionRepository.GetAll().Where(x => x.RoleId == fc["RoleId"]);
                if (rolePermissions.Any())
                {
                    foreach (var item in rolePermissions)
                    {
                        _rolePermissionRepository.Delete(item);
                        _rolePermissionRepository.Save();
                    }
                }

                var collection = fc.ToList();
                foreach (var i in collection)
                {
                    foreach (var j in permissions)
                    {
                        if (i.Key == j.Name)
                        {
                            RolePermission model = new RolePermission();
                            model.RoleId = fc["RoleId"];
                            model.PermissionId = j.Id;
                            model.CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                            model.CreatedOn = DateTime.Now;
                            model.ModifiedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
                            model.ModifiedOn = DateTime.Now;
                            model.IsActive = true;
                            _rolePermissionRepository.Insert(model);
                            _rolePermissionRepository.Save();
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _loggerRepository.Insert(UserId, "RolesAndPermissions", "Index", ex);
                return View(ex);
            }
        }

        public JsonResult GetPermissionsForRole(string RoleId)
        {
            try
            {
                var result = from rp in _rolePermissionRepository.GetAll()
                             join p in _permissionRepository.GetAllPermission()
                             on rp.RoleId equals RoleId
                             where rp.PermissionId == p.Id
                             select new
                             {
                                 p.Name
                             };
                return Json(result, new System.Text.Json.JsonSerializerOptions());
            }
            catch (Exception ex)
            {
                var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _loggerRepository.Insert(UserId, "RolesAndPermissions", "GetPermissionsForRole", ex);
                return Json("");
            }
        }

        public IActionResult tempPermission()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _loggerRepository.Insert(UserId, "RolesAndPermissions", "tempPermission", ex);
                return View(ex);
            }
        }
        [HttpPost]
        public IActionResult tempPermission(PermissionViewModel model)
        {
            try
            {
                var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                PermissionModel permission = new PermissionModel();
                permission.Name = model.PermissionName;
                permission.CreatedById = UserId;
                permission.CreatedOn = DateTime.Now;
                permission.ModifiedById = UserId;
                permission.ModifiedOn = DateTime.Now;
                permission.IsActive = true;
                _permissionRepository.Insert(permission);
                _permissionRepository.Save();
                return RedirectToAction("tempPermission");
            }
            catch (Exception ex)
            {
                var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _loggerRepository.Insert(UserId, "RolesAndPermissions", "tempPermission", ex);
                return View(ex);
            }
        }
    }
}
