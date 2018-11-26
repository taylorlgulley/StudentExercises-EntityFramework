using Microsoft.AspNetCore.Mvc.Rendering;
using StudentExercisesWebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentExercisesWebApp.Models.ViewModels
{
    public class CreateStudentViewModel
    {
        public Student Student { get; set; }
        public List<SelectListItem> AvailableExercises { get; private set; }
        public List<int> SelectedExercises { get; set; }
        public List<SelectListItem> Cohorts { get; set; }

        public CreateStudentViewModel() { }
        public CreateStudentViewModel(ApplicationDbContext ctx)
        {
            List<Exercise> AllExercises = ctx.Exercises.ToList();
            AvailableExercises = AllExercises.Select(li => new SelectListItem()
            {
                Text = li.Name,
                Value = li.ExerciseId.ToString()
            }).ToList();

            Cohorts = ctx.Cohorts.Select(li => new SelectListItem()
            {
                Text = li.Name,
                Value = li.CohortId.ToString()
            }).ToList();
        }
    }
}
