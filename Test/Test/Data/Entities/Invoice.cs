namespace Test.Data.Entities
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public required string Recipient  { get; set; }
        public DateTime TimeOfPurchase { get; set; }
        public required IEnumerable<Product> Products { get; set; }
    }
}
