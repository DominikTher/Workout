using System;
using System.Collections.Generic;
using System.Text;

namespace DT.Business.Entities
{
    public class Series
    {
        public int Id { get; set; }
        public double Weight { get; set; }
        public int Repetitions { get; set; }

        public int WorkoutItemId { get; set; }
        public WorkoutItem WorkoutItem { get; set; }
    }
}
