namespace YourApp.Models
{
    public class PartnerRequest
    {
        public int Id { get; set; }
        public int RequesterId { get; set; }
        public int ReceiverId { get; set; }
        public string Status { get; set; }

        public DateTime RequestedAt { get; set; }
    }
}
