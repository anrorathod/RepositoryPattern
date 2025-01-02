using Demo.Core.Tables;

namespace Demo.Data.Contracts
{
    public interface IBannerRepository : IRepository<Banner>
    {
        Task<bool> CommitAsync();
    }
}
