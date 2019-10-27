using DT.Business.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DT.Business.Interface.Repositories
{
    public interface IEntityRepository
    {
        Task<IEnumerable<T>> GetAsync<T>() where T: class;
        Task<T> AddAsync<T>(T entity) where T : class;
        Task<T> UpdateAsync<T>(T entity) where T : class;
        Task<T> RemoveAsync<T>(int id) where T : class;
    }
}
