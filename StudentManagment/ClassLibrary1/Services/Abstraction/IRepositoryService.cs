using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services.Abstraction
{
    public interface IRepositoryService<T> where T : class, BaseEntity
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression, List<string> includedProps = null);

        IQueryable<T> GetAllContext();

        Task<List<T>> GetAll();

        Task <T> GetOneAsync(int id);

        Task Create(T item);

        Task Create(List<T> items);

        Task Update(T item);

        Task Delete(int id);
    }
}
