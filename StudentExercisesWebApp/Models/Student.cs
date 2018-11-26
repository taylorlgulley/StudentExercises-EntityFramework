using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesWebApp.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Student's Cohort")]
        [Required(ErrorMessage ="Please select a corhort")]
        public int CohortId { get; set; }

        public virtual Cohort Cohort { get; set; }
        public virtual ICollection<StudentExercise> StudentExercises { get; set; }
    }
}