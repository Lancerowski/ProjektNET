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
    public class TrainerWorkoutClassesController : Controller
    {
        private readonly ProjektNETDbContext _context;

        public TrainerWorkoutClassesController(ProjektNETDbContext context)
        {
            _context = context;
        }

        // GET: TrainerWorkoutClasses
        public async Task<IActionResult> Index()
        {
            var projektNETDbContext = _context.TrainerWorkoutClasses.Include(t => t.Trainer).Include(t => t.WorkoutClass);
            return View(await projektNETDbContext.ToListAsync());
        }

        // GET: TrainerWorkoutClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainerWorkoutClass = await _context.TrainerWorkoutClasses
                .Include(t => t.Trainer)
                .Include(t => t.WorkoutClass)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainerWorkoutClass == null)
            {
                return NotFound();
            }

            return View(trainerWorkoutClass);
        }

        // GET: TrainerWorkoutClasses/Create
        public IActionResult Create()
        {
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "Id", "FirstName");
            ViewData["WorkoutClassId"] = new SelectList(_context.WorkoutClasses, "Id", "Name");
            return View();
        }

        // POST: TrainerWorkoutClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TrainerId,WorkoutClassId")] TrainerWorkoutClass trainerWorkoutClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainerWorkoutClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "Id", "FirstName", trainerWorkoutClass.TrainerId);
            ViewData["WorkoutClassId"] = new SelectList(_context.WorkoutClasses, "Id", "Name", trainerWorkoutClass.WorkoutClassId);
            return View(trainerWorkoutClass);
        }

        // GET: TrainerWorkoutClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainerWorkoutClass = await _context.TrainerWorkoutClasses.FindAsync(id);
            if (trainerWorkoutClass == null)
            {
                return NotFound();
            }
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "Id", "FirstName", trainerWorkoutClass.TrainerId);
            ViewData["WorkoutClassId"] = new SelectList(_context.WorkoutClasses, "Id", "Name", trainerWorkoutClass.WorkoutClassId);
            return View(trainerWorkoutClass);
        }

        // POST: TrainerWorkoutClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TrainerId,WorkoutClassId")] TrainerWorkoutClass trainerWorkoutClass)
        {
            if (id != trainerWorkoutClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainerWorkoutClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainerWorkoutClassExists(trainerWorkoutClass.Id))
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
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "Id", "FirstName", trainerWorkoutClass.TrainerId);
            ViewData["WorkoutClassId"] = new SelectList(_context.WorkoutClasses, "Id", "Name", trainerWorkoutClass.WorkoutClassId);
            return View(trainerWorkoutClass);
        }

        // GET: TrainerWorkoutClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainerWorkoutClass = await _context.TrainerWorkoutClasses
                .Include(t => t.Trainer)
                .Include(t => t.WorkoutClass)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainerWorkoutClass == null)
            {
                return NotFound();
            }

            return View(trainerWorkoutClass);
        }

        // POST: TrainerWorkoutClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainerWorkoutClass = await _context.TrainerWorkoutClasses.FindAsync(id);
            if (trainerWorkoutClass != null)
            {
                _context.TrainerWorkoutClasses.Remove(trainerWorkoutClass);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainerWorkoutClassExists(int id)
        {
            return _context.TrainerWorkoutClasses.Any(e => e.Id == id);
        }
    }
}
