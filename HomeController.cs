using Microsoft.AspNetCore.Mvc;
using Mission6.Models; // Make sure the Movie model is included
using System.Linq;

namespace Mission6.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieDbContext _context;

        public HomeController(MovieDbContext context)
        {
            _context = context;
        }

        // Display the home page
        public IActionResult Index()
        {
            // Fetch all movies from the database and pass them to the view
            var movies = _context.Movies.ToList();
            return View(movies); // Pass the list of movies to the view
        }

        // Display the About page
        public IActionResult About()
        {
            return View();
        }

        // Display the AddMovie form
        public IActionResult AddMovie()
        {
            return View();
        }

        // Handle the AddMovie form submission
        [HttpPost]
        public IActionResult AddMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                // Add the new movie to the database
                _context.Movies.Add(movie);
                _context.SaveChanges(); // Save the changes to the database

                return RedirectToAction(nameof(Index)); // Redirect to the Index page to show the updated list of movies
            }

            return View(movie); // Return the view with the movie object if model state is invalid
        }
    }
}
