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
        public List<ApplicationUser> getUsers()
        {
            return _context.Users.ToList();
        }
    }
}
