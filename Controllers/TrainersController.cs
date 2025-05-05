using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektNET.Data;
using ProjektNET.Models;

namespace ProjektNET.Controllers
{
    public class TrainersController : Controller
    {
        private readonly ProjektNETDbContext _context;

        public TrainersController(ProjektNETDbContext context)
        {
            _context = context;
        }

        // GET: Trainers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Trainers.ToListAsync());
        }

        // GET: Trainers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var trainer = await _context.Trainers
                .Include(t => t.TrainerWorkoutClasses)
                    .ThenInclude(twc => twc.WorkoutClass)
                        .ThenInclude(wc => wc.TrainerWorkoutClasses)
                            .ThenInclude(twc => twc.Trainer)
                .Include(t => t.TrainerWorkoutClasses)
                    .ThenInclude(twc => twc.WorkoutClass)
                        .ThenInclude(wc => wc.UserWorkoutClasses)
                            .ThenInclude(uwc => uwc.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (trainer == null)
                return NotFound();

            return View(trainer);
        }

        // GET: Trainers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trainers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,Surname,Age,Phone")] Trainer trainer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }

        // GET: Trainers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer == null)
                return NotFound();

            return View(trainer);
        }

        // POST: Trainers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,Surname,Age,Phone")] Trainer trainer)
        {
            if (id != trainer.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainerExists(trainer.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }

        // GET: Trainers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var trainer = await _context.Trainers
                .FirstOrDefaultAsync(m => m.Id == id);

            if (trainer == null)
                return NotFound();

            return View(trainer);
        }

        // POST: Trainers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer != null)
                _context.Trainers.Remove(trainer);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainerExists(int id)
        {
            return _context.Trainers.Any(e => e.Id == id);
        }
    }
}
