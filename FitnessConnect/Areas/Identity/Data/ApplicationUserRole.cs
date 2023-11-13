using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessConnect.Data.Models
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public string CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
