using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission6.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Mission6.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieDbContext _context;

        public HomeController(MovieDbContext context)
        {
            _context = context;
        }

        // GET: /Home/Index
        // Displays a list of all movies
        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movies
                .Include(m => m.Category)
                .Select(m => new Applications
                {
                    MovieId = m.MovieId,
                    Title = m.Title,
                    Year = m.Year,
                    Edited = m.Edited,
                    CopiedToPlex = m.CopiedToPlex,
                    Notes = m.Notes,
                    CategoryId = m.CategoryId,
                    Category = new Category
                    {
                        CategoryId = m.Category.CategoryId,
                        CategoryName = m.Category.CategoryName  // ✅ Now correctly references CategoryName
                    }

                })
                .ToListAsync();

            return View(movies);
        }

        // GET: /Home/AddMovie
        // Returns the form to add a new movie
        [HttpGet]
        public IActionResult AddMovie()
        {
            ViewBag.Categories = _context.Categories.ToList(); // Pass categories for dropdown
            return View();
        }

        // POST: /Home/AddMovie
        // Receives the movie details and adds the new movie record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMovie(Applications movie)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index"); // Redirect to Index after adding a movie
            }

            ViewBag.Categories = _context.Categories.ToList(); // Reload categories if validation fails
            return View(movie);
        }

        // GET: /Home/EditMovie/{id}
        // Returns the form to edit an existing movie
        [HttpGet]
        public async Task<IActionResult> EditMovie(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            ViewBag.Categories = _context.Categories.ToList(); // Pass categories for dropdown
            return View(movie);
        }

        // POST: /Home/EditMovie/{id}
        // Updates the movie details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMovie(int id, Applications movie)
        {
            if (id != movie.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.MovieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _context.Categories.ToList(); // Reload categories if validation fails
            return View(movie);
        }

        // GET: /Home/DeleteMovie/{id}
        // Returns the confirmation page for deleting a movie
        [HttpGet]
        public async Task<IActionResult> DeleteMovie(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.MovieId == id);

            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: /Home/DeleteMovie/{id}
        // Deletes the movie record
        [HttpPost, ActionName("DeleteMovie")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // GET: /Home/About
        // Returns the About page
        public IActionResult About()
        {
            return View();
        }

        // Helper method to check if a movie exists
        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }
    }
}
