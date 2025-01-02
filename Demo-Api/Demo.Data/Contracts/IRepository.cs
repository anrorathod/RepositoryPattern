using Microsoft.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Demo.Data.Contracts
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> GetAll();
        Task<List<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int pageNumber=1, int pageSize=10,
            string includeProperties = "", CancellationToken cancellationToken = default, bool trackable = true);

        Task<TEntity> GetSingleDataAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,            
            string includeProperties = "", CancellationToken cancellationToken = default, bool trackable = true);

        ValueTask<TEntity> GetByIdAsync(object id);

        //IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
        void UpdateStateAlone(TEntity entityToUpdate);
        void DetachEntities(TEntity entityToDetach);
        void DetachEntities(List<TEntity> entitiesToDetach);
        TEntity Add([NotNull] TEntity entity);

        Task<List<TEntity>> ExecuteSP(string SPName, List<SqlParameter> parameters);
    }
}
