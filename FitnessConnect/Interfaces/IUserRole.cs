using FitnessConnect.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessConnect.Service.Interface
{
    public interface IUserRole
    {
        IEnumerable<ApplicationUserRole> GetAll();
        ApplicationUserRole GetById(string Id);
        ApplicationUserRole GetByRoleId(string Id);
        public void Insert(ApplicationUserRole model);
        public void Update(ApplicationUserRole model);
        public void Delete(ApplicationUserRole model);
        public bool Save();
    }
}
