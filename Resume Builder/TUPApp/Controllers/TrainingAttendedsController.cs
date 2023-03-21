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
    public class TrainingAttendedsController : Controller
    {
        public class Dropdown
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private readonly TupContext _context;

        public TrainingAttendedsController(TupContext context)
        {
            _context = context;
        }

        // GET: TrainingAttendeds
        public async Task<IActionResult> Index()
        {
            var tupContext = _context.TrainingAttendeds.Include(t => t.Student);
            return View(await tupContext.ToListAsync());
        }

        // GET: TrainingAttendeds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TrainingAttendeds == null)
            {
                return NotFound();
            }

            var trainingAttended = await _context.TrainingAttendeds
                .Include(t => t.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingAttended == null)
            {
                return NotFound();
            }

            return View(trainingAttended);
        }

        // GET: TrainingAttendeds/Create
        public IActionResult Create()
        {
            
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
            //ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id");
            return View();
        }

        // POST: TrainingAttendeds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TrainingName,Year,Address,StudentId")] TrainingAttended trainingAttended)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingAttended);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", trainingAttended.StudentId);
            return View(trainingAttended);
        }

        // GET: TrainingAttendeds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TrainingAttendeds == null)
            {
                return NotFound();
            }

            var trainingAttended = await _context.TrainingAttendeds.FindAsync(id);
            if (trainingAttended == null)
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

        // POST: TrainingAttendeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TrainingName,Year,Address,StudentId")] TrainingAttended trainingAttended)
        {
            if (id != trainingAttended.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingAttended);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingAttendedExists(trainingAttended.Id))
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
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", trainingAttended.StudentId);
            return View(trainingAttended);
        }

        // GET: TrainingAttendeds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TrainingAttendeds == null)
            {
                return NotFound();
            }

            var trainingAttended = await _context.TrainingAttendeds
                .Include(t => t.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingAttended == null)
            {
                return NotFound();
            }

            return View(trainingAttended);
        }

        // POST: TrainingAttendeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TrainingAttendeds == null)
            {
                return Problem("Entity set 'TupContext.TrainingAttendeds'  is null.");
            }
            var trainingAttended = await _context.TrainingAttendeds.FindAsync(id);
            if (trainingAttended != null)
            {
                _context.TrainingAttendeds.Remove(trainingAttended);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingAttendedExists(int id)
        {
          return (_context.TrainingAttendeds?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
