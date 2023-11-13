using FitnessConnect.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessConnect.Service.Interface
{
    public interface IRolePermission
    {
        IEnumerable<RolePermission> GetAll();
        RolePermission GetById(string roleId,string permissionId);
        void Insert(RolePermission model);
        void Update(RolePermission model);
        void Delete(RolePermission model);
        void Save();
    }
}
