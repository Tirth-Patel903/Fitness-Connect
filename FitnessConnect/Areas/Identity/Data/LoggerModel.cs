using System.ComponentModel.DataAnnotations;

namespace FitnessConnect.Areas.Identity.Data
{
    public class LoggerModel
    {
        [Key]
        public string Id { get; set; }
        public string ControllerName { get; set; }
        public string MethodName { get; set; }
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedById { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int? ModifiedById { get; set; }
        public bool IsActive { get; set; }

    }
}
