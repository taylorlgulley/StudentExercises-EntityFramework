using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesWebApp.Models
{
    public class StudentExercise
    {
        [Key]
        public int StudentExerciseId { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int ExerciseId { get; set; }

        public Student Student { get; set; }
        public Exercise Exercise { get; set; }
    }
}
