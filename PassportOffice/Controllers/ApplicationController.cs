using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassportOffice.Models;
using PassportOffice.ViewModels;
using System.Security.Claims;

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
        public IActionResult MakeApplication(int id, int typeOfApplicationId, int statusId, DateTime startDate, string description)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ViewBag.Id = id;
            ViewBag.TypeOfApplicationId = typeOfApplicationId;
            ViewBag.StatusId = statusId;
            ViewBag.StartDate = startDate;
            ViewBag.Description = description;
            ViewBag.UserId = userIdString;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MakeApplication(int typeOfApplicationId, DateTime startDate, string description)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized(); // пользователь не аутентифицирован
            }

            Guid userId = Guid.Parse(userIdString);

            // Создаём модель заявления
            var application = new Application
            {
                TypeOfApplicationId = typeOfApplicationId,
                StartDate = startDate,
                Description = description,
                UserId = userId,
                StatusId = 1 // например, статус "новое"
            };

            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            return RedirectToAction("AllApplications");
        }

        private async Task GetTypesOfApplication() 
        {
            var types = await _context.TypesOfApplication.ToListAsync();
            ViewBag.Types = types;
        }

        // Статус заявления по умолчанию "Новое"
        //private async Task<int> GetDefaultStatusId() =>
        //    (await _context.Statuses.FirstOrDefaultAsync())?.Id ?? 0;

        [HttpGet]
        public IActionResult RedirectToMakeApplication()
        {
            return RedirectToAction("MakeApplication", "Application");
        }

        private bool IsEmployee(User currentUser)
        {
            return currentUser.RoleId == 2;
        }

        // Метод получения всех заявок с учётом ограничений прав доступа
        [HttpGet]
        public async Task<IActionResult> AllApplications(int? statusId, int? typeOfApplicationId)
        {
            // Получаем текущего пользователя
            var currentUser = await GetCurrentUserAsync(HttpContext.User.Identity.Name);

            // Запрашиваем доступные заявки
            IQueryable<Application> applicationsQuery = _context.Applications.AsQueryable();

            // Если пользователь НЕ администратор, фильтруем заявки по его UserId
            if (!IsEmployee(currentUser))
            {
                applicationsQuery = applicationsQuery.Where(app => app.UserId == currentUser.Id);
            }

            // Дополнительные фильтры (статус и тип заявки)
            if (statusId.HasValue && statusId.Value > 0)
            {
                applicationsQuery = applicationsQuery.Where(app => app.StatusId == statusId.Value);
            }

            if (typeOfApplicationId.HasValue && typeOfApplicationId.Value > 0)
            {
                applicationsQuery = applicationsQuery.Where(app => app.TypeOfApplicationId == typeOfApplicationId.Value);
            }

            // Выполняем запрос с включёнными зависимостями
            var applications = await applicationsQuery
                .Include(app => app.Status)
                .Include(app => app.TypeOfApplication)
                .ToListAsync();

            // Передача данных в представление
            ViewData["Statuses"] = await _context.Statuses.OrderBy(s => s.Name).ToListAsync();
            ViewData["TypesOfApplication"] = await _context.TypesOfApplication.OrderBy(t => t.Name).ToListAsync();

            return View(applications);
        }

        // Вспомогательный метод для получения текущего пользователя
        private async Task<User> GetCurrentUserAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == username);
        }

        //[HttpGet]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var application = await _context.Applications.FindAsync(id);

        //    if (application == null)
        //    {
        //        return NotFound();
        //    }

        //    var currentUser = await _userManager.GetUserAsync(User);
        //    if (currentUser == null || !_roleManager.RoleExistsAsync("Сотрудник").Result)
        //    {
        //        return Forbid();
        //    }

        //    var employeeRole = await _roleManager.FindByNameAsync("Сотрудник");
        //    if (employeeRole == null)
        //    {
        //        return Forbid();
        //    }

        //    bool isEmployee = currentUser.RoleId == employeeRole.Id;

        //    if (!isEmployee && application.StatusId != 1)
        //    {
        //        return View("Details", application);
        //    }

        //    return View(application);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, Application application)
        //{
        //    if (id != application.Id)
        //    {
        //        return BadRequest();
        //    }

        //    var existingApplication = await _context.Applications.FindAsync(id);

        //    if (existingApplication == null)
        //    {
        //        return NotFound();
        //    }

        //    var currentUser = await _userManager.GetUserAsync(User);
        //    if (currentUser == null || !_roleManager.RoleExistsAsync("Сотрудник").Result)
        //    {
        //        return Forbid();
        //    }

        //    var employeeRole = await _roleManager.FindByNameAsync("Сотрудник");
        //    if (employeeRole == null)
        //    {
        //        return Forbid();
        //    }

        //    bool isEmployee = currentUser.RoleId == employeeRole.Id;

        //    if (!isEmployee && existingApplication.StatusId != 1)
        //    {
        //        ModelState.AddModelError("", "Вы можете редактировать только те заявления, у которых статус 'Новое'.");
        //        return View(existingApplication);
        //    }

        //    if (isEmployee)
        //    {
        //        existingApplication.EndDate = application.EndDate;
        //        existingApplication.ApplicationReview = application.ApplicationReview;
        //        existingApplication.StatusId = application.StatusId;
        //    }
        //    else
        //    {
        //        existingApplication.Description = application.Description;
        //        existingApplication.StartDate = application.StartDate;
        //    }

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", $"Ошибка сохранения: {ex.Message}");
        //        return View(application);
        //    }

        //    return RedirectToAction(nameof(Index));
        //}
    }
}
