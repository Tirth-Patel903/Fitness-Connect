using FitnessConnect.Areas.Identity.Data;
using FitnessConnect.Data.Models;
using FitnessConnect.Service.Interface;

using System.Security;
using System.Security.Claims;
using System.Xml.Linq;

namespace FitnessConnect.Service.Repository
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly ILoggerService _loggerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PermissionRepository(ApplicationDBContext context, ILoggerService loggerRepository, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _loggerRepository = loggerRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public void Delete(PermissionModel model)
        {
            try
            {
                _context.Permission.Remove(model);
            }
            catch (Exception ex)
            {
                var UserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _loggerRepository.Insert(UserId, "PermissionRepository", "Delete", ex);
            }
        }

        public IEnumerable<PermissionModel> GetAllPermission()
        {
            try
            {
                return _context.Permission.ToList();
            }
            catch (Exception ex)
            {
                var UserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _loggerRepository.Insert(UserId, "PermissionRepository", "GetAllPermission", ex);
                return null;
            }
        }

        public PermissionModel GetPermissionById(string id)
        {
            try
            {
                return _context.Permission.ToList().Where(x => x.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                var UserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _loggerRepository.Insert(UserId, "PermissionRepository", "GetPermissionById", ex);
                return null;
            }
        }

        public void Insert(PermissionModel model)
        {
            try
            {
                _context.Permission.Add(model);
            }
            catch (Exception ex)
            {
                var UserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _loggerRepository.Insert(UserId, "PermissionRepository", "Insert", ex);
            }
        }

       

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                var UserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _loggerRepository.Insert(UserId, "PermissionRepository", "Save", ex);
            }
        }

        public void Update(PermissionModel model)
        {
            try
            {
                _context.Permission.Update(model);
            }
            catch (Exception ex)
            {
                var UserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _loggerRepository.Insert(UserId, "PermissionRepository", "Update", ex);
            }
        }

        
    }
}
