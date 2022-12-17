using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VSporte.Task.Infrastructure.Persistence;

namespace Application.Services
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly AppDbContext _context;

        protected RepositoryBase(AppDbContext context) => _context = context;

        public async Task CreateAsync(T entity) => await _context.Set<T>().AddAsync(entity);

        public void Delete(T entity) => _context.Set<T>().Remove(entity);

        public IQueryable<T> FindAll() => _context.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => _context.Set<T>().Where(expression).AsNoTracking();

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Update(T entity) => _context.Set<T>().Update(entity);
    }
}
