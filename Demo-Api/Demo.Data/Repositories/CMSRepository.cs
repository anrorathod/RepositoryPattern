using Demo.Core.Tables;
using Demo.Data;
using Demo.Data.Contracts;
using Demo.Data.Repositories;

namespace Demo.Data.Repositories
{
    public class CMSRepository : Repository<CMS>, ICMSRepository
    {
        public CMSRepository(DemoDbContext context) : base(context)
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
