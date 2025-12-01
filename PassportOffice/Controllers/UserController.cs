using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassportOffice.Models;
using PassportOffice.ViewModels;
using System.Security.Claims;

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
                // Проверка, что пользователь с таким email еще не существует
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Пользователь с таким Email уже зарегистрирован");
                    return View(model);
                }

                // Создаём нового заявителя (Applicant)
                var applicant = new Applicant
                {
                    Surname = model.Surname,
                    MiddleName = model.MiddleName,
                    Name = model.Name,
                    BirthDate = model.BirthDate,
                    Gender = model.Gender,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    Password = model.Password,  
                    RoleId = model.RoleId,      

                    // Поля Applicant
                    BirthPlace = model.BirthPlace,
                    TaxPayerNumber = model.TaxPayerNumber,
                    RegistrationAddress = model.RegistrationAddress,
                    Photo = model.Photo
                };

                _context.Applicants.Add(applicant);
                await _context.SaveChangesAsync();

                await Authenticate(applicant.Email); // аутентификация после регистрации

                return RedirectToAction("Index", "Home");
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
    }
}
