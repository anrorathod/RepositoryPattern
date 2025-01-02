using Demo.Core.Tables;
using Demo.Data.Contracts;

namespace Demo.Data.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {       
        public CustomerRepository(DemoDbContext context) : base(context)
        {
            Context = context;
        }

        public DemoDbContext Context { get; }

        public async Task<bool> CommitAsync()
        {
            return await Context.SaveChangesAsync() > 0;
        }

    }
}

