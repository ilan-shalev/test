using Test.Data.Entities;

namespace Test.Dtos
{
    public class InvoiceReadDto
    {
        public Guid Id { get; set; }
        public required string Recipient { get; set; }
        public DateTime TimeOfPurchase { get; set; }
        public required IEnumerable<ProductReadDto> Products { get; set; }
    }
}
