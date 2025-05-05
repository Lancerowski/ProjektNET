using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektNET.Data;
using ProjektNET.Models;

namespace ProjektNET.Controllers
{
    public class WorkoutClassesController : Controller
    {
        private readonly ProjektNETDbContext _context;

        public WorkoutClassesController(ProjektNETDbContext context)
        {
            _context = context;
        }

        // GET: WorkoutClasses
        public async Task<IActionResult> Index()
        {
            return View(await _context.WorkoutClasses.ToListAsync());
        }

        // GET: WorkoutClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutClass = await _context.WorkoutClasses
                .Include(w => w.UserWorkoutClasses)
                    .ThenInclude(uwc => uwc.User)
                .Include(w => w.TrainerWorkoutClasses)
                    .ThenInclude(twc => twc.Trainer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (workoutClass == null)
            {
                return NotFound();
            }

            return View(workoutClass);
        }

        // GET: WorkoutClasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkoutClasses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Schedule,Duration")] WorkoutClass workoutClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workoutClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workoutClass);
        }

        // GET: WorkoutClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutClass = await _context.WorkoutClasses.FindAsync(id);
            if (workoutClass == null)
            {
                return NotFound();
            }
            return View(workoutClass);
        }

        // POST: WorkoutClasses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Schedule,Duration")] WorkoutClass workoutClass)
        {
            if (id != workoutClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workoutClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkoutClassExists(workoutClass.Id))
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
            return View(workoutClass);
        }

        // GET: WorkoutClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutClass = await _context.WorkoutClasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workoutClass == null)
            {
                return NotFound();
            }

            return View(workoutClass);
        }

        // POST: WorkoutClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workoutClass = await _context.WorkoutClasses.FindAsync(id);
            if (workoutClass != null)
            {
                _context.WorkoutClasses.Remove(workoutClass);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkoutClassExists(int id)
        {
            return _context.WorkoutClasses.Any(e => e.Id == id);
        }
    }
}
