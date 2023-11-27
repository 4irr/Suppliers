namespace Suppliers.Domain
{
    public class Contract
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public DateTime ConclusionDate { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
