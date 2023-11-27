namespace Suppliers.Domain
{
    public class Tender
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime Beginning { get; set; }
        public DateTime Ending { get; set; }
        public bool IsOpen { get; set; }
        public Guid? ExecutorId { get; set; }
        public List<TenderUser>? tenderUsers { get; set; }
    }
}
