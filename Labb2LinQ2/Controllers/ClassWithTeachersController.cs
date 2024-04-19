using Labb2LinQ2.Data;
using Labb2LinQ2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Labb2LinQ2.Controllers
{
    public class ClassWithTeachersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ClassWithTeachersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string className)
        {
            var teachers = await _context.Teachers.ToListAsync();

            var classesTeachers = await (
                from teaching in _context.Teachings
                join c in _context.Classes on teaching.ClassId equals c.ClassId
                join teacher in _context.Teachers on teaching.TeacherId equals teacher.TeacherId
                where string.IsNullOrEmpty(className) || c.ClassName == className
                group new { teaching, c, teacher } by c.ClassName into grouped
                select new
                {
                    ClassName = grouped.Key,
                    TeacherName = string.Join(", ", grouped.Select(x => x.teacher.TeacherName))
                }
            ).ToListAsync();

            var classes = classesTeachers.Select(group => new ClassWithTeachers
            {
                ClassName = group.ClassName,
                TeacherName = group.TeacherName
            }).ToList();

            var viewModel = new ClassWithTeachersVM
            {
                Teachers = teachers,
                Classes = classes
            };

            return View(viewModel);
        }
    }
}
