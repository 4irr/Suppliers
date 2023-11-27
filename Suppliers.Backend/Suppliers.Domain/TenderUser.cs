namespace Suppliers.Domain
{
    public class TenderUser
    {
        public Guid TenderId { get; set; }
        public Tender? Tender { get; set; }
        public Guid UserId { get; set; }
        public string? UserDescription { get; set; }
    }
}
