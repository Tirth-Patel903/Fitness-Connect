using FitnessConnect.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessConnect.Service.Interface
{
    public interface IPermissionRepository
    {
        IEnumerable<PermissionModel> GetAllPermission();
        PermissionModel GetPermissionById(string id);
        void Insert(PermissionModel model);
        void Update(PermissionModel model);
        void Delete(PermissionModel model);
        void Save();
    }
}
