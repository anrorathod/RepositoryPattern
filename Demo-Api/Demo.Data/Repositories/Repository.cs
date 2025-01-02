using Demo.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;

namespace Demo.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class, new()
    {
        private readonly DemoDbContext context;
        internal DbSet<TEntity> dbSet;

        public Repository(DemoDbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {

            var data = this.context.Set<TEntity>().AsNoTracking();
            return data;

        }

        public virtual Task<List<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int pageNumber = 1, int pageSize = 10,
            string includeProperties = "", CancellationToken cancellationToken = default, bool trackable = true)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = trackable ? query.Where(filter).AsNoTracking() : query.Where(filter).AsNoTracking();
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty).AsNoTracking();
            }

            if (orderBy != null)
            {
                query = orderBy(query).AsNoTracking();
            }

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            
            return query.AsNoTracking().ToListAsync();
            

            
        }

        public virtual ValueTask<TEntity> GetByIdAsync(object id)
        {
            return dbSet.FindAsync(id);
        }

        public TEntity Add([NotNull] TEntity entity)
        {
            var entityEntryOfTEntity = dbSet.Add(entity);
            return entityEntryOfTEntity.Entity;
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void UpdateStateAlone(TEntity entityToUpdate)
        {
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void DetachEntities(TEntity entityToDetach)
        {
            context.Entry(entityToDetach).State = EntityState.Detached;
        }

        public void DetachEntities(List<TEntity> entitiesToDetach)
        {
            foreach (var entity in entitiesToDetach)
            {
                context.Entry(entity).State = EntityState.Detached;
            }
        }

        public async Task<TEntity> GetSingleDataAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", CancellationToken cancellationToken = default, bool trackable = true)
        {
            IQueryable<TEntity> query = dbSet;
            
            if (filter != null)
            {
                query = trackable ? query.Where(filter).AsNoTracking() : query.Where(filter).AsNoTracking();
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty).AsNoTracking();
            }

            if (orderBy != null)
            {
                query = orderBy(query).AsNoTracking();
            }


            TEntity? entity = await query.AsNoTracking().FirstOrDefaultAsync();
            return entity;

        }

        public async Task<List<TEntity>> ExecuteSP(string SPName, List<SqlParameter> parameters)
        {
            return await Task.Run(() =>
            {
                var datas = this.context.Set<TEntity>().FromSqlRaw<TEntity>(SPName, parameters.ToArray()).ToList();
                return datas;
            });
        }
    }
}
