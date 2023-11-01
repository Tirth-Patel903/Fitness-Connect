namespace FitnessConnect.Areas.Identity.Data
{
    public class MessageModel :BaseIdentity
    {
        public string? RecieverId { get; set; }
        public string? SenderId { get; set; }
        public string? Attach { get; set; }
        public string? Message { get; set; }

    }
}
