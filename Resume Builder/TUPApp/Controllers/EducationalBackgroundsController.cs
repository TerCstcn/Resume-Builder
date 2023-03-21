using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TUPApp.Models;

namespace TUPApp.Controllers
{
    public class EducationalBackgroundsController : Controller
    {
        public class Dropdown
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private readonly TupContext _context;

        public EducationalBackgroundsController(TupContext context)
        {
            _context = context;
        }

        // GET: EducationalBackgrounds
        public async Task<IActionResult> Index()
        {
            var tupContext = _context.EducationalBackgrounds.Include(e => e.Student);
            return View(await tupContext.ToListAsync());
        }

        // GET: EducationalBackgrounds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EducationalBackgrounds == null)
            {
                return NotFound();
            }

            var educationalBackground = await _context.EducationalBackgrounds
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educationalBackground == null)
            {
                return NotFound();
            }

            return View(educationalBackground);
        }

        // GET: EducationalBackgrounds/Create
        public IActionResult Create()
        {
            //ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id");
            var dropdown = new List<Dropdown>();
            var students = _context.Students.ToList();

            foreach (var item in students)
            {
                dropdown.Add(new Dropdown
                {
                    Id = item.Id,
                    Name = item.FirstName + " " + item.LastName

                });
            }
            ViewData["StudentId"] = new SelectList(dropdown, "Id", "Name");
            return View();
        }

        // POST: EducationalBackgrounds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,School,Year,StudentId")] EducationalBackground educationalBackground)
        {
            if (ModelState.IsValid)
            {
                _context.Add(educationalBackground);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", educationalBackground.StudentId);
            return View(educationalBackground);
        }

        // GET: EducationalBackgrounds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EducationalBackgrounds == null)
            {
                return NotFound();
            }

            var educationalBackground = await _context.EducationalBackgrounds.FindAsync(id);
            if (educationalBackground == null)
            {
                return NotFound();
            }
            var dropdown = new List<Dropdown>();
            var students = _context.Students.Where(x => x.Id == id).ToList();

            foreach (var item in students)
			{
				dropdown.Add(new Dropdown
				{
					Id = item.Id,
					Name = item.FirstName + " " + item.LastName

				});
			}
			ViewData["StudentId"] = new SelectList(dropdown, "Id", "Name");
			return View();
        }

        // POST: EducationalBackgrounds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,School,Year,StudentId")] EducationalBackground educationalBackground)
        {
            if (id != educationalBackground.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(educationalBackground);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationalBackgroundExists(educationalBackground.Id))
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
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", educationalBackground.StudentId);
            return View(educationalBackground);
        }

        // GET: EducationalBackgrounds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EducationalBackgrounds == null)
            {
                return NotFound();
            }

            var educationalBackground = await _context.EducationalBackgrounds
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educationalBackground == null)
            {
                return NotFound();
            }

            return View(educationalBackground);
        }

        // POST: EducationalBackgrounds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EducationalBackgrounds == null)
            {
                return Problem("Entity set 'TupContext.EducationalBackgrounds'  is null.");
            }
            var educationalBackground = await _context.EducationalBackgrounds.FindAsync(id);
            if (educationalBackground != null)
            {
                _context.EducationalBackgrounds.Remove(educationalBackground);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EducationalBackgroundExists(int id)
        {
          return (_context.EducationalBackgrounds?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
