using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesWebApp.Models
{
    public class Cohort
    {
        [Key]
        public int CohortId { get; set; }

        [Display(Name="Cohort Name")]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}