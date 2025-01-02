using Demo.Core.Tables;
using Demo.Data.Contracts;

namespace Demo.Data.Repositories
{
    public class FaqRepository : Repository<Faq>, IFaqRepository
    {       
        public FaqRepository(DemoDbContext context) : base(context)
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

