using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using StudentExercisesWebApp.Models;

namespace StudentExercisesWebApp.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Student> Students {get; set;}
        public DbSet<Cohort> Cohorts { get; set;}
        public DbSet<Exercise> Exercises { get; set;}
        public DbSet<StudentExercise> StudentExercise { get; set;}
    }
}