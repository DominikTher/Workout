using System.Collections.Generic;
using System.Threading.Tasks;

namespace DT.Business.Interface.Services
{
    public interface IDataService
    {
        Task<IEnumerable<TClient>> GetAsnyc<TClient, TBusiness>() where TClient : class where TBusiness : class;
        Task<TClient> AddAsync<TClient, TBusiness>(TClient entity) where TClient : class where TBusiness : class;
        Task<TClient> UpdateAsync<TClient, TBusiness>(TClient entity) where TClient : class where TBusiness : class;
        Task<TClient> RemoveAsync<TClient, TBusiness>(int id) where TClient : class where TBusiness : class;
    }
}
