using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassportOffice.Models;
using System.Diagnostics;

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
            var userEmail = User.Identity?.Name;

            if (!string.IsNullOrEmpty(userEmail))
            {
                var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
                return View(currentUser); 
            }
            else
            {
                return View(null); 
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Обработка события нажатия кнопки "Подать заявление"
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
