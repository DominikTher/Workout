using AutoMapper;

using ClientWorkout = DT.Client.Entities.Workout;
using BusinessWorkout = DT.Business.Entities.Workout;

using ClientWorkoutItem = DT.Client.Entities.WorkoutItem;
using BusinessWorkoutItem = DT.Business.Entities.WorkoutItem;

namespace DT.Business.Configuration
{
    public class AutoMapperConfiguration
    {
        public MapperConfiguration SetUpAutoMapper()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClientWorkout, BusinessWorkout>().ForMember(w => w.WorkoutItems, opt => opt.Ignore());
                cfg.CreateMap<BusinessWorkout, ClientWorkout>();
                cfg.CreateMap<ClientWorkoutItem, BusinessWorkoutItem>().ForMember(wi => wi.Series, opt => opt.Ignore());
                cfg.CreateMap<BusinessWorkoutItem, ClientWorkoutItem>();
            });
        }
    }
}
