using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesWebApp.Models
{
    public class Exercise
    {
        [Key]
        public int ExerciseId { get; set; }

        [Display(Name="Exercise Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Exercise Language")]
        [Required]
        public string Language { get; set; }

        public virtual ICollection<StudentExercise> StudentExercises { get; set; }
    }
}
