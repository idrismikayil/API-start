using Data.DAL;
using Data.Services.Abstraction;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services.Implementation
{
    public class EfRepositoryService<T> : IRepositoryService<T> where T : class, BaseEntity
    {
        protected readonly AppDbContext _context;

        public EfRepositoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(T item)
        {
            await _context.Set<T>().AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public Task Create(List<T> items)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            T item = await GetOneAsync(id);
            _context.Set<T>().Remove(item);
            await _context.SaveChangesAsync();

        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression, List<string> includedProps = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includedProps != null)
            {
                foreach (var item in includedProps)
                {
                    query = query.Include(item);
                }
            }

            return query.Where(expression).ToListAsync();
        }

        public IQueryable<T> GetAllContext()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task<T> GetOneAsync(int id)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task Update(T item)
        {
            _context.Set<T>().Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
