using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Labb2LinQ2.Data;
using Labb2LinQ2.Models;

namespace Labb2LinQ2.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Enrollments.Include(e => e.Class).Include(e => e.Student).Include(e => e.Teacher);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Class)
                .Include(e => e.Student)
                .Include(e => e.Teacher)
                .FirstOrDefaultAsync(m => m.EnrollmentId == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Enrollments/Create
        public IActionResult Create()
        {
            ViewData["FkClassId"] = new SelectList(_context.Classes, "ClassId", "ClassName");
            ViewData["FkStudentId"] = new SelectList(_context.Students, "StudentId", "StudentName");
            ViewData["FkTeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherName");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrollmentId,Grade,FkClassId,FkStudentId,FkTeacherId")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", enrollment.FkClassId);
            ViewData["FkStudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", enrollment.FkStudentId);
            ViewData["FkTeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId", enrollment.FkTeacherId);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["FkClassId"] = new SelectList(_context.Classes, "ClassId", "ClassName", enrollment.FkClassId);
            ViewData["FkStudentId"] = new SelectList(_context.Students, "StudentId", "StudentName", enrollment.FkStudentId);
            ViewData["FkTeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherName", enrollment.FkTeacherId);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnrollmentId,Grade,FkClassId,FkStudentId,FkTeacherId")] Enrollment enrollment)
        {
            if (id != enrollment.EnrollmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.EnrollmentId))
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
            ViewData["FkClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", enrollment.FkClassId);
            ViewData["FkStudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", enrollment.FkStudentId);
            ViewData["FkTeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId", enrollment.FkTeacherId);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Class)
                .Include(e => e.Student)
                .Include(e => e.Teacher)
                .FirstOrDefaultAsync(m => m.EnrollmentId == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollments.Any(e => e.EnrollmentId == id);
        }


        // get students/teachers
        public IActionResult StudentsTeachers()
        {
            var query = from enrollment in _context.Enrollments
                        join s in _context.Students on enrollment.FkStudentId equals s.StudentId
                        join t in _context.Teachers on enrollment.FkTeacherId equals t.TeacherId
                        select new
                        {
                            StudentName = s.StudentName,
                            TeacherName = t.TeacherName
                        };

            var groupedByStudent = query.GroupBy(item => item.StudentName)
                                        .Select(group => new
                                        {
                                            StudentName = group.Key,
                                            TeacherNames = string.Join(", ", group.Select(item => item.TeacherName))
                                        });

            return View(groupedByStudent);

        }

        //get specific class/teacehers and students
        public IActionResult Programmering1()
        {
            var query = from enrollment in _context.Enrollments
                        join s in _context.Students on enrollment.FkStudentId equals s.StudentId
                        join t in _context.Teachers on enrollment.FkTeacherId equals t.TeacherId
                        join c in _context.Classes on enrollment.FkClassId equals c.ClassId
                        where c.ClassName == "Programmering 1" // Filter by course name
                        select new
                        {
                            StudentName = s.StudentName,
                            TeacherName = t.TeacherName,
                            ClassName = c.ClassName
                        };

            var groupedByStudent = query.GroupBy(item => item.StudentName)
                                        .Select(group => new
                                        {
                                            StudentName = group.Key,
                                            TeacherNames = string.Join(", ", group.Select(item => item.TeacherName)),
                                            ClassName = group.First().ClassName
                                        });

            return View(groupedByStudent);
        }
    }
}
