using FitnessConnect.Areas.Identity.Data;

namespace FitnessConnect.Interfaces
{
    public interface IUsersRepository
    {
        List<ApplicationUser> getUsers();
    }
}
