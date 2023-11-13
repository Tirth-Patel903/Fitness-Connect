using FitnessConnect.Areas.Identity.Data;
using FitnessConnect.Data.Models;
using FitnessConnect.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FitnessConnect.Service.Repository
{
    public class RolePermissionRepository : IRolePermission
    {
        private readonly ApplicationDBContext _context;
        private readonly ILoggerService _loggerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RolePermissionRepository(ApplicationDBContext context, ILoggerService loggerRepository, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _loggerRepository = loggerRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public void Delete(RolePermission model)
        {
            try
            {
                _context.RolePermission.Remove(model);
            }
            catch (Exception ex)
            {
                var UserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _loggerRepository.Insert(UserId, "RolePermissionRepository", "Delete", ex);
            }
        }

        public IEnumerable<RolePermission> GetAll()
        {
            try
            {
                return _context.RolePermission.ToList();
            }
            catch (Exception ex)
            {
                var UserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _loggerRepository.Insert(UserId, "RolePermissionRepository", "GetAll", ex);
                return null;
            }
        }

        public RolePermission GetById(string roleId, string permissionId)
        {
            try
            {
                return _context.RolePermission.ToList().Where(x => x.RoleId == roleId && x.PermissionId == permissionId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                var UserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _loggerRepository.Insert(UserId, "RolePermissionRepository", "GetById", ex);
                return null;
            }
        }

        public void Insert(RolePermission model)
        {
            try
            {
                _context.RolePermission.Add(model);
            }
            catch (Exception ex)
            {
                var UserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _loggerRepository.Insert(UserId, "RolePermissionRepository", "Insert", ex);
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
                _loggerRepository.Insert(UserId, "RolePermissionRepository", "Save", ex);
            }
        }

        public void Update(RolePermission model)
        {
            try
            {
                _context.RolePermission.Update(model);
            }
            catch (Exception ex)
            {
                var UserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _loggerRepository.Insert(UserId, "RolePermissionRepository", "Update", ex);
            }
        }
    }
}
