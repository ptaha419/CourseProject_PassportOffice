using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> Register()
        {
            await GetRoles();
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
                    // добавляем пользователя в бд
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
                        RoleId = model.RoleId,

                        // Если RoleId = 2, то заполняются доп. поля для сотрудника 
                        Position = model.RoleId == 2 ? model.Position : "",
                        DepartmentId = model.RoleId == 2 ? model.DepartmentId : default(int),
                    });

                    await _context.SaveChangesAsync();

                    await Authenticate(model.Email);

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и/или пароль");
            }
            await GetRoles();
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

        private async Task GetRoles()
        {
            var roles = await _context.Roles.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),      // Значение (id роли)
                Text = r.Name                 // Название роли
            }).ToListAsync();

            ViewBag.Roles = roles;
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }
    }
}
