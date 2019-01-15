using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MovieApplication.Core.Data;
using MovieApplication.Core.Repositories.Interfaces;

namespace MovieApplication.Core.Repositories
{

    public class MovieApplicationBaseRepository<T> : IMovieApplicationBaseRepository<T> where T : class
    {
        public readonly MovieApplicationDbContext _dbContext;
        private static DbSet<T> _dbSet;


        public MovieApplicationBaseRepository(MovieApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }


        public virtual async Task<T> FindById(object EntityId)
        {
            return await _dbSet.FindAsync(EntityId);
        }

        public virtual T Delete(object Id)
        {
            T entityToDelete = _dbSet.Find(Id);
            bool result = Delete(entityToDelete);
            if (result)
                return entityToDelete;
            else
                return null;
        }

        public virtual bool Delete(T Entity)
        {
            bool result = false;
            try
            {
                if (_dbContext.Entry(Entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(Entity);
                }
                _dbSet.Remove(Entity);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public virtual async Task<EntityEntry<T>> InsertAsync(T entity)
        {
            return await _dbSet.AddAsync(entity);
        }

        public virtual async Task<ICollection<T>> InsertRange(ICollection<T> entities)
        {
            foreach (var entity in entities)
            {
                await _dbSet.AddAsync(entity);
            }

            return entities;
        }

        public virtual async Task<T> Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public virtual async Task<ICollection<T>> UpdateRange(ICollection<T> entities)
        {
            foreach (var entity in entities)
            {
                _dbSet.Attach(entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
            }

            return entities;
        }
        public virtual IQueryable<T> GetAll()
        {

            IQueryable<T> query = _dbSet.AsQueryable();
            return query;
        }

        public virtual async Task<List<T>> GetList(Expression<Func<T, bool>> Filter = null)
        {
            if (Filter != null)
            {
                return await _dbSet.Where(Filter).OrderBy(t => (true)).ToListAsync();
            }
            return await _dbSet.OrderBy(t => (true)).ToListAsync();
        }

        public virtual async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).SingleOrDefaultAsync();
        }

        public virtual async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).FirstOrDefaultAsync();

        }
        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

        public virtual IQueryable<T> QueryWithEagerLoad(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] children)
        {
            IQueryable<T> query = _dbSet;
            foreach (var inc in children)
            {
                query = query.Include(inc);
            }

            return query.Where(filter).OrderBy(t => (true));
        }
    }
}
