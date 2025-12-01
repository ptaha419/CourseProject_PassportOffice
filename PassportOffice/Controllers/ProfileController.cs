using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassportOffice.Models;
using PassportOffice.ViewModels;
using System.Security.Claims;

namespace PassportOffice.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly WebAppDbContext _context;

        public ProfileController(IWebHostEnvironment env, WebAppDbContext context)
        {
            _env = env;
            _context = context;
        }

        private Guid? GetCurrentUserId()
        {
            var sessionValue = HttpContext.Session.GetString("UserId");

            if (!string.IsNullOrEmpty(sessionValue) && Guid.TryParse(sessionValue, out var sessionUserId))
            {
                return sessionUserId;
            }

            var claimId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                        ?? User?.FindFirst("sub")?.Value
                        ?? User?.Identity?.Name;

            if (!string.IsNullOrEmpty(claimId) && Guid.TryParse(claimId, out var claimUserId))
            {
                return claimUserId;
            }

            return null;
        }

        // GET: /Profile
        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();
            User user = null;
            Applicant applicant = null;

            if (userId.HasValue)
            {
                user = await _context.Users
                                     .Include(u => u.Applicants)
                                     .FirstOrDefaultAsync(u => u.Id == userId.Value);

                if (user != null && user.Applicants.Any())
                {
                    applicant = user.Applicants.First(); 
                }
            }

            if (user == null || applicant == null)
            {
                return RedirectToAction("Login", "User"); 
            }

            var profile = new ProfileModel
            {
                Id = user.Id.ToString(),
                Surname = user.Surname,
                MiddleName = user.MiddleName,
                Name = user.Name,
                BirthDate = user.BirthDate,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                BirthPlace = applicant.BirthPlace,
                TaxPayerNumber = applicant.TaxPayerNumber,
                RegistrationAddress = applicant.RegistrationAddress,
                Photo = applicant.Photo
            };

            return View(profile);
        }

        // GET: /Profile/Edit 
        public async Task<IActionResult> Edit()
        {
            var userId = GetCurrentUserId();
            User user = null;
            Applicant applicant = null;

            if (userId.HasValue)
            {
                user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId.Value);
            }

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new ProfileModel
            {
                Id = user.Id.ToString(),
                Surname = user.Surname,
                MiddleName = user.MiddleName,
                Name = user.Name,
                BirthDate = user.BirthDate,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                BirthPlace = applicant.BirthPlace,
                TaxPayerNumber = applicant.TaxPayerNumber,
                RegistrationAddress = applicant.RegistrationAddress,
                Photo = applicant.Photo
            };

            return View(model);
        }

        // POST: /Profile/Edit 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProfileModel model)
        {
            User user = null;

            if (Guid.TryParse(model.Id, out var guid))
            {
                user = await _context.Users.FirstOrDefaultAsync(u => u.Id == guid);
            }
            else
            {
                var currentId = GetCurrentUserId();

                if (currentId.HasValue)
                {
                    user = await _context.Users.FirstOrDefaultAsync(user => user.Id == currentId.Value);
                }
            }

            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            user.Surname = model.Surname;
            user.MiddleName = model.MiddleName;
            user.Name = model.Name;
            user.BirthDate = model.BirthDate;
            user.Gender = model.Gender;
            user.PhoneNumber = model.PhoneNumber;
            user.Email = model.Email;

            await _context.SaveChangesAsync();

            TempData["ProfileSaved"] = "Данные профиля успешно сохранены.";
            return RedirectToAction("Index");
        }
    }
}
