using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassportOffice.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PassportOffice.Controllers
{
    public class UserController : Controller
    {
        private readonly WebAppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserController(WebAppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // Метод для проверки существования почтового адреса
        private async Task<bool> IsEmailExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        //GET: Login 
        public ActionResult Login()
        {
            return View("LoginForm");
        }

        //POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string login, string password)
        {
            if (ModelState.IsValid)
            {
                var hashedPassword = GetMd5Hash(password);
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login && u.Password == hashedPassword);

                if (user != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, $"{user.Surname} {user.MiddleName} {user.Name}"),
                    new Claim(ClaimTypes.Role, "User")
                };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal,
                        new AuthenticationProperties { ExpiresUtc = DateTime.UtcNow.AddHours(2) });

                    // Сохраняем Id пользователя в сессии
                    _httpContextAccessor.HttpContext.Session.SetString("UserId", user.Id.ToString());

                    return RedirectToAction(nameof(Index));
                }
            }

            ModelState.AddModelError("", "Неверный логин или пароль.");
            return View("LoginForm");
        }

        //GET: Register
        public ActionResult Register()
        {
            return View("RegistrationForm");
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User model)
        {
            if (ModelState.IsValid)
            {
                if (await IsEmailExists(model.Email))
                {
                    ModelState.AddModelError("", "Пользователь с таким email уже существует.");
                    return View("RegistrationForm", model);
                }

                model.Password = GetMd5Hash(model.Password);

                try
                {
                    Debug.WriteLine($"Регистрация пользователя: {model.Name}, {model.Email}, {model.Password}");
                    _context.Users.Add(model);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Ошибка при сохранении в базу: {ex.Message}. Стэк-трейс: {ex.StackTrace}");
                    return View("RegistrationForm", model);
                }

                return RedirectToAction("Login");
            }

            return View("RegistrationForm", model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _httpContextAccessor.HttpContext.Session.Remove("UserId");
            return RedirectToAction(nameof(Index));
        }

        // Вспомогательная функция для хэширования пароля
        private static string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                    sBuilder.Append(data[i].ToString("x2"));

                return sBuilder.ToString();
            }
        }
    }
}
