using System.ComponentModel.DataAnnotations;

namespace FitnessConnect.ViewModel
{
    public class PermissionViewModel
    {
        [Required]
        public string PermissionName { get; set; }
    }
}
