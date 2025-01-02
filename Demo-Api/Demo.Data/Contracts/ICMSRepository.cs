using Demo.Core.Tables;

namespace Demo.Data.Contracts
{
    public interface ICMSRepository : IRepository<CMS>
    {
        Task<bool> CommitAsync();
    }
}
