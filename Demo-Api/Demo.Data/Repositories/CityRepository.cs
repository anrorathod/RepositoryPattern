using Demo.Core.Tables;
using Demo.Data;
using Demo.Data.Contracts;
using Demo.Data.Repositories;

namespace Demo.Data.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(DemoDbContext context) : base(context)
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
