using System.ComponentModel.DataAnnotations;

namespace FitnessConnect.Areas.Identity.Data
{
    public class BaseIdentity
    {

        [Key]
        public string Id { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedById { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedById { get; set; }
        public bool? IsActive { get; set; }
    }
}
