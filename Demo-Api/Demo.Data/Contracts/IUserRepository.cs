using Demo.Core.Tables;
using Demo.Data.Contracts;

namespace Demo.Data.Contracts
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> CommitAsync();
    }
}
