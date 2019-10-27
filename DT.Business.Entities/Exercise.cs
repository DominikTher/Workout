using System;
using System.Collections.Generic;
using System.Text;

namespace DT.Business.Entities
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ExerciseTag> ExerciseTags { get; set; }
    }
}
