using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MovieApplication.Core.Repositories.Interfaces
{
    public interface IMovieApplicationBaseRepository<T> where T : class
    {
        Task<T> FindById(object EntityId);

        Task<T> Update(T entity);

        Task<EntityEntry<T>> InsertAsync(T entity);

        T Delete(object Id);

        IQueryable<T> GetAll();

        Task<List<T>> GetList(Expression<Func<T, bool>> Filter = null);

        Task<T> Get(Expression<Func<T, bool>> predicate);

    }
}
