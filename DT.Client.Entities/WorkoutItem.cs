using System;
using System.Collections.Generic;
using System.Text;

namespace DT.Client.Entities
{
    public class WorkoutItem
    {
        public int Id { get; set; }

        public int WorkoutId { get; set; }
        public int ExerciseId { get; set; }
    }
}
