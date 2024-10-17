using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCard_Core.Interfaces.GenericRepositoryInterface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAll();
        Task AddAsync(T newEntity);
         void Delete(T entity);
    }
}
