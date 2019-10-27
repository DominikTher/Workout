using AutoMapper;
using DT.Business.Interface.Repositories;
using DT.Business.Interface.Services;
using DT.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DT.Business.Services
{
    public class WorkoutItemDataService : BaseDataService, IWorkoutItemDataService
    {
        private readonly IWorkoutItemRepository workoutItemRepository;
        private readonly IMapper mapper;

        public WorkoutItemDataService(IWorkoutItemRepository workoutItemRepository, IMapper mapper) : base(workoutItemRepository as IEntityRepository, mapper)
        {
            this.workoutItemRepository = workoutItemRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<WorkoutItem>> GetAsync(int workoutId)
        {
            var businessEntities = await workoutItemRepository.GetAsync(workoutId);

            return businessEntities.Select(mapper.Map<DT.Business.Entities.WorkoutItem, WorkoutItem>);
        }
    }
}
