using Microsoft.AspNetCore.Identity;

namespace FitnessConnect.ViewModels
{
    public class UsersViewModel : IdentityUser<string>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime birthdate { get; set; }
        public string? Gender { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int? ModifiedById { get; set; }
        public bool IsActive { get; set; }
        public string? ProfileImg { get; set; }
        public string Password { get; set; }
    }
}
