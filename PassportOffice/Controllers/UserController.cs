using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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
                    // Передаем в Authenticate email и user.Id для добавления в claims
                    await Authenticate(user.Email, user.Id);

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
            var roles = _context.Roles.ToList();
            ViewBag.Roles = roles;
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model, int roleId)
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
                        RoleId = roleId,
                        BirthPlace = model.BirthPlace, 
                        TaxPayerNumber = model.TaxPayerNumber,
                        RegistrationAddress = model.RegistrationAddress
                    });


                    await _context.SaveChangesAsync();

                    //await Authenticate(model.Email, user.Id);

                    return RedirectToAction("Login", "User");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и/или пароль");
            }
            //await GetRoles();
            return View(model);
        }

        private async Task Authenticate(string userName, Guid userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())  // добавляем UserId
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        //private async Task GetRoles()
        //{
        //    var roles = await _context.Roles.Select(r => new SelectListItem
        //    {
        //        Value = r.Id.ToString(),      // Значение (id роли)
        //        Text = r.Name                 // Название роли
        //    }).ToListAsync();

        //    ViewBag.Roles = roles;
        //}

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }

        // GET: User/Profile - просмотр профиля текущего пользователя
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
                return Unauthorized();

            var userId = Guid.Parse(userIdString);
            var user = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Department)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // GET: User/Edit - форма редактирования профиля
        public async Task<IActionResult> Edit()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
                return Unauthorized();

            ViewBag.Departments = await _context.Departments.ToListAsync();

            var userId = Guid.Parse(userIdString);
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: User/Edit - сохранение изменений
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User model)
        {
            if (!ModelState.IsValid)
                return View(model);

            ViewBag.Departments = await _context.Departments.ToListAsync();

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
                return Unauthorized();

            var userId = Guid.Parse(userIdString);
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
                return NotFound();

            // Обновляем поля (без пароля - если нужна смена пароля отдельный метод)
            user.Surname = model.Surname;
            user.MiddleName = model.MiddleName;
            user.Name = model.Name;
            user.BirthDate = model.BirthDate;
            user.Gender = model.Gender;
            user.PhoneNumber = model.PhoneNumber;
            user.Email = model.Email;
            user.BirthPlace = model.BirthPlace;
            user.TaxPayerNumber = model.TaxPayerNumber;
            user.RegistrationAddress = model.RegistrationAddress;
            //user.Photo = model.Photo;
            user.Position = model.Position;
            //user.DepartmentId = model.DepartmentId;

            try
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Profile));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Ошибка при сохранении данных");
                return View(model);
            }
        }
    }
}
