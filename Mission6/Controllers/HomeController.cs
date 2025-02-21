using Microsoft.AspNetCore.Mvc;
using Mission6.Models;

namespace Mission6.Controllers
{
    public class HomeController : Controller
    {
        private MovieDbContext _context;
        public HomeController(MovieDbContext temp)
        {
            _context = temp; //Assigns db to _context variable
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View("About");
        }
        [HttpGet]
        public IActionResult AddMovie()
        {
            return View("AddMovie");
        }
        [HttpPost]
        //.Include(navigationPropertyPath x :Application => x.Movie)
        //test
        public IActionResult AddMovie(Applications response)
        {
            _context.Movies.Add(response); //Add record
            _context.SaveChanges();
            return View("Confirmation",response);

        }
    }
}

//i am testing this