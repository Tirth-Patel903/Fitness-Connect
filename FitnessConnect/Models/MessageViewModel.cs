namespace FitnessConnect.Models
{
    public class MessageViewModel
    {
        public string? RecieverId { get; set; }
        public string? SenderId { get; set; }
        public string? Attach { get; set; }
        public string? Message { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ProfileImg { get; set; }
        public string? Avatar { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
