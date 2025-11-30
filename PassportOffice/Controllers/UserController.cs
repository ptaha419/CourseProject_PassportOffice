using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassportOffice.Models;
using PassportOffice.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static PassportOffice.ViewModels.ProfileModel;

namespace PassportOffice.Controllers
{
    public class UserController : Controller
    {
        private WebAppDbContext _context;

        public UserController(WebAppDbContext context)
        {
            _context = context;
        }

        //GET: Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u =>
                u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(model.Email); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и/или пароль");
            }
            return View(model);
        }

        //GET: Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u =>
                u.Email == model.Email);
                if (user == null)
                {
                    _context.Users.Add(new User
                    {
                        Surname = model.Surname,
                        MiddleName = model.MiddleName,
                        Name = model.Name,
                        BirthDate = model.BirthDate,
                        Gender = model.Gender,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email,
                        Password = model.Password, 
                        RoleId = model.RoleId
                    });

                    await _context.SaveChangesAsync();

                    await Authenticate(model.Email);

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и/или пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }

        public async Task<IActionResult> GetProfile()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                var applicant = await _context.Applicants.FirstOrDefaultAsync(u => u.Id.ToString() == userId); // Предположим, что роль заявителя хранится отдельно

                if (applicant != null)
                {
                    return View(new ProfileModel
                    {
                        Id = applicant.Id,
                        Surname = applicant.Surname,
                        MiddleName = applicant.MiddleName,
                        Name = applicant.Name,
                        BirthDate = applicant.BirthDate,
                        Gender = applicant.Gender,
                        PhoneNumber = applicant.PhoneNumber,
                        Email = applicant.Email,
                        BirthPlace = applicant.BirthPlace,
                        TaxPayerNumber = applicant.TaxPayerNumber,
                        RegistrationAddress = applicant.RegistrationAddress,
                        Photo = applicant.Photo
                    });
                }
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile([FromForm] ProfileModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!string.IsNullOrEmpty(userId))
                {
                    var applicant = await _context.Applicants.FirstOrDefaultAsync(u => u.Id.ToString() == userId);

                    if (applicant != null)
                    {
                        applicant.Surname = model.Surname;
                        applicant.MiddleName = model.MiddleName;
                        applicant.Name = model.Name;
                        applicant.BirthDate = model.BirthDate;
                        applicant.Gender = model.Gender;
                        applicant.PhoneNumber = model.PhoneNumber;
                        applicant.Email = model.Email;
                        applicant.BirthPlace = model.BirthPlace;
                        applicant.TaxPayerNumber = model.TaxPayerNumber;
                        applicant.RegistrationAddress = model.RegistrationAddress;
                        applicant.Photo = model.Photo;

                        try
                        {
                            await _context.SaveChangesAsync();
                            TempData["SuccessMessage"] = "Профиль успешно обновлён!";
                            return RedirectToAction("GetProfile");
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("", $"Ошибка при сохранении изменений: {ex.Message}");
                        }
                    }
                }
            }

            return View(model);
        }
    }
}
