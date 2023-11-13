using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessConnect.Data.Models
{
    public class RolePermission
    {
        public string PermissionId { get; set; }
        [ForeignKey("PermissionId")]
        public virtual PermissionModel permission { get; set; }

        public string RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual ApplicationRole applicationRole { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
