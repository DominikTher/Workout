using System;
using System.Collections.Generic;
using System.Text;

namespace DT.Business.Entities
{
    public class WorkoutItem
    {
        public int Id { get; set; }
        public ICollection<Series> Series { get; set; }

        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
    }
}
