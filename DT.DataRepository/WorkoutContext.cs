using DT.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DT.DataRepository
{
    public class WorkoutContext : DbContext
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<WorkoutItem> WorkoutItems { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<ExerciseTag> ExerciseTags { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }

        public WorkoutContext(DbContextOptions<WorkoutContext> dbContextOptions)
            : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExerciseTag>()
                .HasKey(exerciseTag => new { exerciseTag.ExerciseId, exerciseTag.TagId });

            modelBuilder.Entity<ExerciseTag>()
                .HasOne(exerciseTag => exerciseTag.Exercise)
                .WithMany(exercise => exercise.ExerciseTags)
                .HasForeignKey(exerciseTag => exerciseTag.ExerciseId);

            modelBuilder.Entity<ExerciseTag>()
                .HasOne(exerciseTag => exerciseTag.Tag)
                .WithMany(tag => tag.ExerciseTags)
                .HasForeignKey(exerciseTag => exerciseTag.TagId);

            modelBuilder.Entity<Workout>()
                .HasOne(workout => workout.AppUser)
                .WithMany()
                .HasForeignKey(workout => workout.AppUserId);

            //modelBuilder.Entity<AppUser>()
            //    .Property(appUser => appUser.Properties)
            //    .HasConversion(
            //        f => JsonConvert.SerializeObject(f, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
            //        f => JsonConvert.DeserializeObject<IEnumerable<AppUserProperties>>(f, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
            //    );
        }
    }
}
