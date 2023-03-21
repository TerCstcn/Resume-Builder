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
    public class StudentsController : Controller
    {
        private readonly TupContext _context; //null pa laman ng _context

        public StudentsController(TupContext context)
        {
            _context = context; //yung laman ng context at mapupunta sa _context dahil di pwede gamitin ang context dahil parameters sya
        }

        // GET: Students
        public IActionResult Index()
        //public async Task<IActionResult> Index()
        {
            //return _context.Students != null ? 
            //            View(await _context.Students.ToListAsync()) :
            //            Problem("Entity set 'TupContext.Students'  is null.");
            //SQL Select * from Students
            //in Linq
            var student = _context.Students
                                        //.Where (x => x.Id == 1)    
                                        .ToList(); //pag gusto mo iretrieve ang lahat ng data, gagamit ng tolist //_context is data base, kukunin ang students table, tapos ililist

            //var student1 = _context.Students
            //                            .Where(x => x.Id == 1)
            //                            .FirstOrDefault(); // nireretrieve mo lang ang top 1 dun sa table. pinaka una. wala syang condition na where.


            return View(student);
        }

        // GET: Students/Details/5

        //sychronous - step by step (kunwari nagsubmit ka, lag ka. wala kang ibang magagawa sa page
        //asynchronous - nag wowork sabay sabay (kunwari nag submit ka, pwede ka pang gumawa sa iba or scroll
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,MiddleName,LastName,Address,Contact")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student); //syntax for adding
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,MiddleName,LastName,Address,Contact")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student); //syntax for updating
                    await _context.SaveChangesAsync(); // need to
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
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
            if (_context.Students == null)
            {
                return Problem("Entity set 'TupContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
