internal class MovieDbContext
{
}

public class HomeController : Controller
{
    private readonly MovieDbContext _context;

    public HomeController(MovieDbContext context)
    {
        _context = context;
    }

    // GET: Home/Index
    public IActionResult Index()
    {
        return View(); // This will search for the Index view in /Views/Home/Index.cshtml
    }

    public IActionResult About()
    {
        return View();
    }
}
