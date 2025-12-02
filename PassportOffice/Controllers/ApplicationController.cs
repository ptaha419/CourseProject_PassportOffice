using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public ApplicationController(WebAppDbContext context, 
            UserManager<User> userManager, 
            RoleManager<IdentityRole<int>> roleManager)
        {
            _context = context;
            _statuses = _context.Statuses.ToList();
            _userManager = userManager;
            _roleManager = roleManager;
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
        public async Task<IActionResult> AllApplications(int? statusId, int? typeOfApplicationId)
        {
            IQueryable<Application> applicationsQuery = _context.Applications.AsQueryable();

            // Фильтрация по статусу, если выбран статус
            if (statusId.HasValue && statusId.Value > 0)
            {
                applicationsQuery = applicationsQuery.Where(app => app.StatusId == statusId.Value);
            }

            // Фильтрация по типу заявки, если выбран тип
            if (typeOfApplicationId.HasValue && typeOfApplicationId.Value > 0)
            {
                applicationsQuery = applicationsQuery.Where(app => app.TypeOfApplicationId == typeOfApplicationId.Value);
            }

            var applications = await applicationsQuery.Include(app => app.Status).Include(app => app.TypeOfApplication).ToListAsync();

            ViewData["Statuses"] = await _context.Statuses.OrderBy(s => s.Name).ToListAsync(); 
            ViewData["TypesOfApplication"] = await _context.TypesOfApplication.OrderBy(t => t.Name).ToListAsync(); 

            return View(applications); 
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var application = await _context.Applications.FindAsync(id);

            if (application == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null || !_roleManager.RoleExistsAsync("Сотрудник").Result)
            {
                return Forbid();
            }

            var employeeRole = await _roleManager.FindByNameAsync("Сотрудник");
            if (employeeRole == null)
            {
                return Forbid();
            }

            bool isEmployee = currentUser.RoleId == employeeRole.Id;

            if (!isEmployee && application.StatusId != 1)
            {
                return View("Details", application);
            }

            return View(application);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Application application)
        {
            if (id != application.Id)
            {
                return BadRequest();
            }

            var existingApplication = await _context.Applications.FindAsync(id);

            if (existingApplication == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null || !_roleManager.RoleExistsAsync("Сотрудник").Result)
            {
                return Forbid();
            }

            var employeeRole = await _roleManager.FindByNameAsync("Сотрудник");
            if (employeeRole == null)
            {
                return Forbid();
            }

            bool isEmployee = currentUser.RoleId == employeeRole.Id;

            if (!isEmployee && existingApplication.StatusId != 1)
            {
                ModelState.AddModelError("", "Вы можете редактировать только те заявления, у которых статус 'Новое'.");
                return View(existingApplication);
            }

            if (isEmployee)
            {
                existingApplication.EndDate = application.EndDate;
                existingApplication.ApplicationReview = application.ApplicationReview;
                existingApplication.StatusId = application.StatusId;
            }
            else
            {
                existingApplication.Description = application.Description;
                existingApplication.StartDate = application.StartDate;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Ошибка сохранения: {ex.Message}");
                return View(application);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
