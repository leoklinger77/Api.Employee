using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly DataContext _context;
        public readonly DbSet<T> DbSet;

        public Repository(DataContext context)
        {
            _context = context;
            DbSet = context.Set<T>();
        }

        public async Task Delete(int id)
        {
            DbSet.Remove(new T { Id = id });
            await SaveChanges();
        }

        public void Dispose()
        {
            _context?.DisposeAsync();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<List<T>> FindAlls()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T> FindById(int id)
        {
            return await DbSet.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task Insert(T entity)
        {
            await DbSet.AddAsync(entity);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task Update(T entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }
    }
}
