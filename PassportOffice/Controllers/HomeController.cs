using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PassportOffice.Models;

namespace PassportOffice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WebAppDbContext _context;

        public HomeController(ILogger<HomeController> logger, WebAppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId.HasValue)
            {
                var currentUser = await _context.Users.FindAsync(userId.Value);
                return View(currentUser); // передаем текущего пользовател€ в представление
            }
            else
            {
                return View(null); // если пользователь не аутентифицирован, передаем null
            }
        } 

        public IActionResult Privacy()
        {
            return View();
        }

        // ќбработка событи€ нажати€ кнопки "ѕодать за€вление"
        public ActionResult RedirectToApplicationForm()
        {
            return View("../Application/ApplicationForm");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
