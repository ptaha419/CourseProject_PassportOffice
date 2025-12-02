using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PassportOffice.Models;
using PassportOffice.ViewModels;

namespace PassportOffice.Controllers
{
    [Authorize]
    public class ApplicationController : Controller
    {
        private WebAppDbContext _context;
        private readonly IEnumerable<Status> _statuses;

        public ApplicationController(WebAppDbContext context)
        {
            _context = context;
            _statuses = _context.Statuses.ToList();
        }

        [HttpGet]
        public async Task<IActionResult> MakeApplication()
        {
            await GetTypesOfApplication();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeApplication(ApplicationModel model)
        {
            if (ModelState.IsValid)
            {
                string currentUserEmail = User.Identity.Name; 

                var applicant = await _context.Applicants.FirstOrDefaultAsync(a => a.Email == currentUserEmail);

                if (applicant == null)
                {
                    throw new Exception("Заявитель не найден.");
                }

                var application = new Application
                {
                    TypeOfApplicationId = model.TypeOfApplicationId,
                    StatusId = GetDefaultStatusId(),
                    ApplicantId = applicant.Id, 
                    StartDate = model.StartDate,
                    Description = model.Description
                };

                _context.Applications.Add(application);
                await _context.SaveChangesAsync();

                return RedirectToAction("AllApplications", "Application");
            }

            await GetTypesOfApplication();
            return View(model);
        }

        private async Task GetTypesOfApplication() 
        {
            var types = await _context.TypesOfApplication.ToListAsync();
            ViewBag.Types = types;
        }

        // Статус заявления по умолчанию "Новое"
        private int GetDefaultStatusId() => _statuses.First().Id;

        [HttpGet]
        public IActionResult RedirectToMakeApplication()
        {
            return View("MakeApplication");
        }

        [HttpGet]
        public IActionResult AllApplications()
        {
            return View("AllApplications");
        } 

        [HttpPost]
        public IActionResult AllApplications(Application application)
        {
            return View();
        }
    }
}
