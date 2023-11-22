using FitnessConnect.Areas.Identity.Data;

namespace FitnessConnect.Interfaces
{
    public interface IUsersRepository
    {
        List<ApplicationUser> GetUsers();
        bool DeleteUser(string id);
        ApplicationUser GetUserById(string id);
        void Save();
    }
}
