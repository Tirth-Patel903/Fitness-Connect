using FitnessConnect.Areas.Identity.Data;
using FitnessConnect.Interfaces;

namespace FitnessConnect.Services
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsersRepository(ApplicationDBContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public List<ApplicationUser> GetUsers()
        {
            return _context.Users.ToList();
        }
        public bool DeleteUser(string id)
        {
            try
            {
                var deleteduser = _context.Users.Where(x => x.Id == id).FirstOrDefault();
                _context.Users.Remove(deleteduser);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public ApplicationUser GetUserById(string id)
        {
            return _context.Users.Find(id);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
