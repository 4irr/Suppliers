namespace Suppliers.Domain
{
    public class Product
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public float Price { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
