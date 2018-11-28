using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentExercisesWebApp.Data;
using StudentExercisesWebApp.Models;
using StudentExercisesWebApp.Models.ViewModels;

namespace StudentExercisesWebApp.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Students.Include(s => s.Cohort);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // return not found if no id is passed in the URL
            if (id == null)
            {
                return NotFound();
            }

            // Telling the database interface(context) to go get all Students from the database. Then include the cohort based on the model as well as the StudentExercises. The include in Entity is like a Join in SQL. The ThenInclude is attaching to the include above it to go deeper and join the exercise to the StudentExercise. Last only include the student who matches the id passed in the URL. The where statement to match up ids in sql is implicit in Include.
            var student = await _context.Students
                .Include(s => s.Cohort)
                .Include(s => s.StudentExercises)
                    .ThenInclude(se => se.Exercise)
                // You can use the commented out code instead of the .ThenInclude above to get the same result
                //.Include("StudentExercises.Exercise")
                .FirstOrDefaultAsync(s => s.StudentId == id);

            // return not found if no student matches the detail id passed in the URL
            if (student == null)
            {
                return NotFound();
            }

            // Take the student that you placed the information you want to display and return it to the view
            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            CreateStudentViewModel model = new CreateStudentViewModel(_context);
            return View(model);
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateStudentViewModel model)
        {
            // If statment saying only continue if the required fields are filled out meaning the ModelState is Valid
            if (ModelState.IsValid)
            {
                // Telling the Database Interface to Add the Student from the view model to the database
                _context.Add(model.Student);

                // A foreach looping through the SelectedExercises to create the instances of the StudentExercise for every one in the list. Then adding them to the database. The foreach works because each entry in the list is an int representing the exerciseId then it takes the student Id from the created student in the model to make the StudentExercise. Needs to be in an if statement so a person can create a student not assign them an exercise.
                if (model.SelectedExercises != null)
                {
                    foreach (int exerciseId in model.SelectedExercises)
                    {
                        StudentExercise newSE = new StudentExercise()
                        {
                            StudentId = model.Student.StudentId,
                            ExerciseId = exerciseId
                        };
                        _context.Add(newSE);
                    }
                }
                // wait for changes before commiting them to the database. This is like how copy works. Add is like copying something to the clipboard but to put it somewhere you need to paste which is the SaveChangesAsync part.
                await _context.SaveChangesAsync();
                // Once the new Student is created and saved redirect to the index view
                return RedirectToAction(nameof(Index));
            }
            // Getting the data from the database needed to make the list of cohorts to choose from. Only need if you don't do it in the view model which we did so this is not needed.
            // ViewData["CohortId"] = new SelectList(_context.Cohorts, "CohortId", "Name", model.Student.CohortId);
            return View(model);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["CohortId"] = new SelectList(_context.Cohorts, "CohortId", "Name", student.CohortId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,FirstName,LastName,CohortId")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CohortId"] = new SelectList(_context.Cohorts, "CohortId", "Name", student.CohortId);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Cohort)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
