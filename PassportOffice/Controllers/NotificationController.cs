using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassportOffice.Models;
using System.Security.Claims;

namespace PassportOffice.Controllers
{
    public class NotificationController : Controller
    {
        private readonly WebAppDbContext _context;

        public NotificationController(WebAppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> AllNotifications()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
                return Unauthorized();

            Guid userId = Guid.Parse(userIdString);
            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.Id)
                .ToListAsync();

            return View(notifications);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
                return NotFound();

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || notification.UserId != Guid.Parse(userIdString))
                return Forbid();

            notification.IsRead = true;
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync();

            return RedirectToAction("AllNotifications");
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
                return Unauthorized();

            var notification = await _context.Notifications
                .Include(n => n.Application)
                    .ThenInclude(a => a.TypeOfApplication)
                .Include(n => n.Application)
                    .ThenInclude(a => a.Status)
                .Include(n => n.User)   // если уведомление содержит связь с сотрудником
                .FirstOrDefaultAsync(n => n.Id == id);

            if (notification == null)
                return NotFound();

            if (notification.UserId != Guid.Parse(userIdString))
                return Forbid();

            return View(notification);
        }
    }
}
