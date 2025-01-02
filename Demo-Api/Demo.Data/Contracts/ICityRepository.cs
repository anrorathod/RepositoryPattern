using Demo.Core.Tables;
using Demo.Data.Contracts;

namespace Demo.Data.Contracts
{
    public interface ICityRepository : IRepository<City>
    {
        Task<bool> CommitAsync();
    }
}
