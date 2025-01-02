using Demo.Core.StoreProcedure;

namespace Demo.Data.Contracts
{
    public interface IStoreProcedureRepository : IRepository<SPData>
    {
        Task<bool> CommitAsync(); 
    }
}
