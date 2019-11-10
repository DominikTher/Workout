using AutoMapper;

using ClientWorkout = DT.Client.Entities.Workout;
using BusinessWorkout = DT.Business.Entities.Workout;

using ClientWorkoutItem = DT.Client.Entities.WorkoutItem;
using BusinessWorkoutItem = DT.Business.Entities.WorkoutItem;

using ClientExercise = DT.Client.Entities.Exercise;
using BusinessExercise = DT.Business.Entities.Exercise;

using ClientSeries = DT.Client.Entities.Series;
using BusinessSeries = DT.Business.Entities.Series;

using ClientAppUser = DT.Client.Entities.AppUser;
using BusinessAppUser = DT.Business.Entities.AppUser;

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

                cfg.CreateMap<ClientExercise, BusinessExercise>().ForMember(e => e.ExerciseTags, opt => opt.Ignore());
                cfg.CreateMap<BusinessExercise, ClientExercise>();

                cfg.CreateMap<ClientSeries, BusinessSeries>().ForMember(s => s.WorkoutItem, opt => opt.Ignore());
                cfg.CreateMap<BusinessSeries, ClientSeries>();

                cfg.CreateMap<ClientAppUser, BusinessAppUser>();
                cfg.CreateMap<BusinessAppUser, ClientAppUser>().ForMember(u => u.Password, opt => opt.Ignore());
            });
        }
    }
}
