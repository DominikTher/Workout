using System;
using System.Collections.Generic;
using System.Text;

namespace DT.Client.Entities
{
    public class Series
    {
        public int Id { get; set; }
        public double Weight { get; set; }

        public int WorkoutItemId { get; set; }
    }
}
