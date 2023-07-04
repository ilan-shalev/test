using Test.Data.Entities;

namespace Test.Dtos
{
    public class InvoiceCreateDto
    {
        public required string Recipient { get; set; }
        public required IEnumerable<ProductCreateDto> Products { get; set; }
    }
}
