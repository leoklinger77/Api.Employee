using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
        Task Insert(T entity);
        Task<T> FindById(int id);
        Task<List<T>> FindAlls();
        Task Update(T entity);
        Task Delete(int id);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
        Task<int> SaveChanges();
    }
}
