using Test.Data.Entities;

namespace Test.Data.Repositories
{
    public class InvoicesRepository : Repository<Invoice>
    {
        public InvoicesRepository(AppDbContext context) : base(context)
        {
        }

        // invoices logic goes here
    }
}
