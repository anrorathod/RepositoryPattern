using Demo.Core.Tables;
using Demo.Data.Contracts;

namespace Demo.Data.Contracts
{
    public interface ICountryRepository : IRepository<Country>
    {
        Task<bool> CommitAsync();
    }
}
