﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DT.Business.Entities
{
    public class Workout
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public double UserWeight { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public ICollection<WorkoutItem> WorkoutItems { get; set; }
    }
}
