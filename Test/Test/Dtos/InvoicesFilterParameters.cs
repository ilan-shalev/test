using Test.Data.Entities;

namespace Test.Dtos
{
    public class InvoicesFilterParameters
    {
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set;}
        public string? Recipient { get; set; }
    }
}
