using Test.Data.Entities;

namespace Test.Dtos
{
    public class InvoiceUpdateDto
    {
        public Guid Id { get; set; }
        public required string Recipient { get; set; }
        public DateTime TimeOfPurchase { get; set; }
        public required IEnumerable<ProductUpdateDto> Products { get; set; }
    }
}
