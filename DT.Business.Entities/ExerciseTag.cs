using System;
using System.Collections.Generic;
using System.Text;

namespace DT.Business.Entities
{
    public class ExerciseTag
    {
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
