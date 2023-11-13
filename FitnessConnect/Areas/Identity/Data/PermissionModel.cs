using FitnessConnect.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessConnect.Data.Models
{
    public class PermissionModel : BaseIdentity
    {
        public string Name { get; set; }

        public List<RolePermission> rolePermissions { get; set; }
    }
}
