using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PassportOffice.Models;
using System.Security.Claims;

namespace PassportOffice.Controllers
{
    public class DocumentController : Controller
    {
        private WebAppDbContext _context;

        public DocumentController(WebAppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AddDocument()
        {
            ViewBag.TypeOfDocuments = _context.TypesOfDocument.ToList(); // Типы документов передаются в представление
            return View();
        }

        // POST: /Documents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDocument(Document documentModel)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier); // Получаем идентификатор текущего пользователя

            if (string.IsNullOrEmpty(userIdString)) // Проверяем наличие идентификатора
            {
                return Unauthorized();
            }

            try
            {
                Guid userId = Guid.Parse(userIdString); // Преобразуем строку идентификатора в GUID

                // Заполняем внешний ключ на пользователя
                documentModel.UserId = userId;

                // Добавляем новый документ в базу данных
                _context.Documents.Add(documentModel);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index"); // Перенаправляем на список всех документов
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ошибка при создании документа." + ex.Message);
                ViewBag.TypesOfDocument = _context.TypesOfDocument.ToList(); // Повторно загружаем типы документов
                return View(documentModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UserDocuments()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
                return Unauthorized();

            Guid userId = Guid.Parse(userIdString);

            var documents = await _context.Documents
                .Where(d => d.UserId == userId)
                .Include(d => d.TypeOfDocument) // если нужно показать название типа документа
                .ToListAsync();

            return View(documents);
        }
    }
}
