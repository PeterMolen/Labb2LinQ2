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
    public class TeachingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeachingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Teachings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Teachings.Include(t => t.Class).Include(t => t.Teacher);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Teachings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teaching = await _context.Teachings
                .Include(t => t.Class)
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(m => m.TeachingId == id);
            if (teaching == null)
            {
                return NotFound();
            }

            return View(teaching);
        }

        // GET: Teachings/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassName");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherName");
            return View();
        }

        // POST: Teachings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeachingId,TeacherId,ClassId")] Teaching teaching)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teaching);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", teaching.ClassId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId", teaching.TeacherId);
            return View(teaching);
        }

        // GET: Teachings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teaching = await _context.Teachings.FindAsync(id);
            if (teaching == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassName", teaching.ClassId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherName", teaching.TeacherId);
            return View(teaching);
        }

        // POST: Teachings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeachingId,TeacherId,ClassId")] Teaching teaching)
        {
            if (id != teaching.TeachingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teaching);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeachingExists(teaching.TeachingId))
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
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", teaching.ClassId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "TeacherId", teaching.TeacherId);
            return View(teaching);
        }

        // GET: Teachings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teaching = await _context.Teachings
                .Include(t => t.Class)
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(m => m.TeachingId == id);
            if (teaching == null)
            {
                return NotFound();
            }

            return View(teaching);
        }

        // POST: Teachings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teaching = await _context.Teachings.FindAsync(id);
            if (teaching != null)
            {
                _context.Teachings.Remove(teaching);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeachingExists(int id)
        {
            return _context.Teachings.Any(e => e.TeachingId == id);
        }



        //Programmering 1 View  and all classes/teachers
        
    }
}
