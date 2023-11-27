namespace Suppliers.Domain
{
    public class Batch
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
    