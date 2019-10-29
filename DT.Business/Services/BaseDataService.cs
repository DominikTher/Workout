using AutoMapper;
using DT.Business.Interface.Repositories;
using DT.Business.Interface.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT.Business.Services
{
    public class BaseDataService : IDataService
    {
        private readonly IEntityRepository entityRepository;
        private readonly IMapper mapper;

        public BaseDataService(IEntityRepository entityRepository, IMapper mapper) => (this.entityRepository, this.mapper) = (entityRepository, mapper);


        public async Task<TClient> AddAsync<TClient, TBusiness>(TClient entity)
            where TClient : class
            where TBusiness : class
        {
            var businessEntity = mapper.Map<TBusiness>(entity);
            var clientEntity = mapper.Map<TClient>(await entityRepository.AddAsync(businessEntity));

            return clientEntity;
        }

        public async Task<IEnumerable<TClient>> GetAsnyc<TClient, TBusiness>()
            where TClient : class
            where TBusiness : class
        {
            var businessEntities = await entityRepository.GetAsync<TBusiness>();

            return businessEntities.Select(mapper.Map<TBusiness, TClient>);
        }

        public async Task<TClient> RemoveAsync<TClient, TBusiness>(int id)
            where TClient : class
            where TBusiness : class
        {
            return mapper.Map<TClient>(await entityRepository.RemoveAsync<TBusiness>(id));
        }

        public async Task<TClient> UpdateAsync<TClient, TBusiness>(TClient entity)
            where TClient : class
            where TBusiness : class
        {
            var businessEntity = mapper.Map<TBusiness>(entity);
            return mapper.Map<TClient>(await entityRepository.UpdateAsync(businessEntity));
        }
    }
}
