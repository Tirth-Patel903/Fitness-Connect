using FitnessConnect.Areas.Identity.Data;
using FitnessConnect.Data.Models;
using FitnessConnect.Service.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FitnessConnect.Service.Repository
{
    public class UserRoleRepository : IUserRole
    {
        public readonly ApplicationDBContext _context;
        private readonly ILoggerService _loggerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserRoleRepository(ApplicationDBContext context, ILoggerService loggerRepository, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _loggerRepository = loggerRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public void Delete(ApplicationUserRole model)
        {
            try
            {
                _context.UserRoles.Remove(model);
            }
            catch (Exception ex)
            {
                var UserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _loggerRepository.Insert(UserId, "UserRoleRepository", "Delete", ex);
            }
        }

        public IEnumerable<ApplicationUserRole> GetAll()
        {
            try
            {
                return _context.UserRoles.ToList();
            }
            catch (Exception ex)
            {
                var UserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _loggerRepository.Insert(UserId, "UserRoleRepository", "GetAll", ex);
                return null;
            }
        }

        public ApplicationUserRole GetByRoleId(string Id)
        {
            try
            {
                return _context.UserRoles.Where(x => x.RoleId == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                var UserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _loggerRepository.Insert(UserId, "UserRoleRepository", "GetByRoleId", ex);
                return null;
            }
        }
        public ApplicationUserRole GetById(string Id)
        {
            try
            {
                return _context.UserRoles.Where(x => x.UserId == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                var UserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _loggerRepository.Insert(UserId, "UserRoleRepository", "GetById", ex);
                return null;
            }
        }

        public void Insert(ApplicationUserRole model)
        {
            try
            {
                _context.UserRoles.Add(model);
            }
            catch (Exception ex)
            {
                var UserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _loggerRepository.Insert(UserId, "UserRoleRepository", "Insert", ex);
            }
        }

        public bool Save()
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                var UserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _loggerRepository.Insert(UserId, "UserRoleRepository", "Save", ex);
                return false;
            }
        }
        public void Update(ApplicationUserRole model)
        {
            try
            {
                _context.UserRoles.Update(model);
            }
            catch (Exception ex)
            {
                var UserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _loggerRepository.Insert(UserId, "UserRoleRepository", "Update", ex);
            }
        }
    }
}
