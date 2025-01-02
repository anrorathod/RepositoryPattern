using Demo.Core.Tables;
using Demo.Data.Contracts;

namespace Demo.Data.Contracts
{
    public interface IStateRepository : IRepository<State>
    {
        Task<bool> CommitAsync();
    }
}
