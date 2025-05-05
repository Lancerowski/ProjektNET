using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektNET.Data;
using ProjektNET.Models;
using Microsoft.Extensions.Logging;

namespace ProjektNET.Controllers
{
    public class UserWorkoutClassesController : Controller
    {
        private readonly ProjektNETDbContext _context;
        private readonly ILogger<UserWorkoutClassesController> _logger;

        public UserWorkoutClassesController(ProjektNETDbContext context, ILogger<UserWorkoutClassesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: UserWorkoutClasses
        public async Task<IActionResult> Index()
        {
            var projektNETDbContext = _context.UserWorkoutClasses.Include(u => u.User).Include(u => u.WorkoutClass);
            return View(await projektNETDbContext.ToListAsync());
        }

        // GET: UserWorkoutClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userWorkoutClass = await _context.UserWorkoutClasses
                .Include(u => u.User)
                .Include(u => u.WorkoutClass)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userWorkoutClass == null)
            {
                return NotFound();
            }

            return View(userWorkoutClass);
        }

        // GET: UserWorkoutClasses/Create
        public IActionResult Create()
        {
            // Populate the dropdown lists for UserId and WorkoutClassId
            ViewBag.UserId = new SelectList(
                _context.Users.Select(u => new { Id = u.Id, Name = u.FirstName + " " + u.Surname }).ToList(),
                "Id",
                "Name"
            ); 
            ViewBag.WorkoutClassId = new SelectList(_context.WorkoutClasses, "Id", "Name"); // Assuming "Name" is used as a display field for the WorkoutClass model

            return View();
        }

        // POST: UserWorkoutClasses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,WorkoutClassId")] UserWorkoutClass userWorkoutClass)
        {
            _logger.LogInformation("Create method triggered. UserId: {UserId}, WorkoutClassId: {WorkoutClassId}",
                                    userWorkoutClass.UserId, userWorkoutClass.WorkoutClassId);

            // Validate if both UserId and WorkoutClassId are valid
            if (_context.Users.Any(u => u.Id == userWorkoutClass.UserId) &&
                _context.WorkoutClasses.Any(w => w.Id == userWorkoutClass.WorkoutClassId))
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Add(userWorkoutClass);
                        await _context.SaveChangesAsync();
                        _logger.LogInformation("UserWorkoutClass created successfully with ID: {Id}", userWorkoutClass.Id);
                        return RedirectToAction(nameof(Index)); // Redirect to Index page after successful creation
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Error occurred while saving: {Error}", ex.Message);
                        ModelState.AddModelError(string.Empty, "An error occurred while saving the data.");
                    }
                }
                else
                {
                    _logger.LogWarning("ModelState is not valid. Errors: {Errors}", string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
                }
            }
            else
            {
                _logger.LogError("Invalid UserId or WorkoutClassId. UserId: {UserId}, WorkoutClassId: {WorkoutClassId}",
                                  userWorkoutClass.UserId, userWorkoutClass.WorkoutClassId);
                ModelState.AddModelError(string.Empty, "Invalid UserId or WorkoutClassId.");
            }

            // Log the state if the model is not valid
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                _logger.LogError("ModelState error: {ErrorMessage}", error.ErrorMessage);
            }

            // Repopulate the dropdowns on error
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", userWorkoutClass.UserId);
            ViewData["WorkoutClassId"] = new SelectList(_context.WorkoutClasses, "Id", "Name", userWorkoutClass.WorkoutClassId);
            return View(userWorkoutClass);
        }

        // GET: UserWorkoutClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userWorkoutClass = await _context.UserWorkoutClasses.FindAsync(id);
            if (userWorkoutClass == null)
            {
                return NotFound();
            }

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", userWorkoutClass.UserId);
            ViewData["WorkoutClassId"] = new SelectList(_context.WorkoutClasses, "Id", "Name", userWorkoutClass.WorkoutClassId);
            return View(userWorkoutClass);
        }

        // POST: UserWorkoutClasses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,WorkoutClassId")] UserWorkoutClass userWorkoutClass)
        {
            if (id != userWorkoutClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userWorkoutClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserWorkoutClassExists(userWorkoutClass.Id))
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

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "FirstName", userWorkoutClass.UserId);
            ViewData["WorkoutClassId"] = new SelectList(_context.WorkoutClasses, "Id", "Name", userWorkoutClass.WorkoutClassId);
            return View(userWorkoutClass);
        }

        // GET: UserWorkoutClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userWorkoutClass = await _context.UserWorkoutClasses
                .Include(u => u.User)
                .Include(u => u.WorkoutClass)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userWorkoutClass == null)
            {
                return NotFound();
            }

            return View(userWorkoutClass);
        }

        // POST: UserWorkoutClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userWorkoutClass = await _context.UserWorkoutClasses.FindAsync(id);
            if (userWorkoutClass != null)
            {
                _context.UserWorkoutClasses.Remove(userWorkoutClass);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserWorkoutClassExists(int id)
        {
            return _context.UserWorkoutClasses.Any(e => e.Id == id);
        }
    }
}
