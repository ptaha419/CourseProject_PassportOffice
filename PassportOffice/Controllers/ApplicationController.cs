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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
                return Unauthorized();

            Guid userId = Guid.Parse(userIdString);
            var currentUser = await _context.Users.FindAsync(userId);

            if (currentUser == null)
                return Unauthorized();

            var application = await _context.Applications
                .Include(a => a.TypeOfApplication)
                .Include(a => a.Status)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (application == null)
                return NotFound();

            if (!IsEmployee(currentUser))
            {
                if (application.UserId != userId || application.StatusId != 1)
                    return Forbid();
            }

            ViewBag.IsEmployee = currentUser.RoleId == 2;

            // Получаем все статусы для выпадающего списка
            ViewBag.StatusList = await _context.Statuses.ToListAsync();

            return View(application);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Application model)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized();
            }
            Guid userId = Guid.Parse(userIdString);

            var currentUser = await _context.Users.FindAsync(userId);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            var existingApplication = await _context.Applications.FindAsync(model.Id);
            if (existingApplication == null)
            {
                return NotFound();
            }

            if (!IsEmployee(currentUser))
            {
                // Обычный пользователь может редактировать только свои заявки со статусом 1
                if (existingApplication.UserId != userId || existingApplication.StatusId != 1)
                {
                    return Forbid();
                }

                // Редактируем только Description и StartDate
                existingApplication.Description = model.Description;
                existingApplication.StartDate = model.StartDate;
                // Не меняем StatusId, EndDate, ApplicationReview
            }
            else
            {
                // Сотрудник может менять StatusId, EndDate, ApplicationReview, а также описание и дату
                existingApplication.StatusId = model.StatusId;
                existingApplication.EndDate = model.EndDate;
                existingApplication.ApplicationReview = model.ApplicationReview;
                //existingApplication.Description = model.Description;
                //existingApplication.StartDate = model.StartDate;
            }

            _context.Applications.Update(existingApplication);
            await _context.SaveChangesAsync();

            return RedirectToAction("AllApplications");
        }
    }
}
