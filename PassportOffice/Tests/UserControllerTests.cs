using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PassportOffice.Controllers;
using PassportOffice.Models;
using PassportOffice.ViewModels;

namespace PassportOffice.Tests
{
    [TestFixture]
    public class UserControllerTests
    {
        private WebAppDbContext GetInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<WebAppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new WebAppDbContext(options);

            context.Users.Add(new User
            {
                Id = Guid.NewGuid(),
                Email = "test@example.com",
                Password = "password",
                BirthPlace = "Moscow",
                Gender = "Male",
                MiddleName = "Ivanovich",
                Name = "Ivan",
                PhoneNumber = "+79161234567",
                RegistrationAddress = "Some address",
                Surname = "Ivanov",
                TaxPayerNumber = "1234567890"
            });
            context.Roles.Add(new Role { Id = 1, Name = "User" });
            context.SaveChanges();
            return context;
        }

        [Test]
        public void Login_Get_ReturnsView()
        {
            var context = GetInMemoryDb();
            var controller = new UserController(context);

            var result = controller.Login();

            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public async Task Login_Post_ValidUser_RedirectsToHomeIndex()
        {
            var context = GetInMemoryDb();
            var controller = new UserController(context);
            var model = new LoginModel
            {
                Email = "test@example.com",
                Password = "password"
            };

            var result = await controller.Login(model);

            Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
            var redirect = (RedirectToActionResult)result;
            Assert.That(redirect.ActionName, Is.EqualTo("Index"));
            Assert.That(redirect.ControllerName, Is.EqualTo("Home"));
        }

        [Test]
        public async Task Register_Get_ReturnsViewWithRoles()
        {
            var context = GetInMemoryDb();
            var controller = new UserController(context);

            var result = await controller.Register();

            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = (ViewResult)result;
            Assert.That(viewResult.ViewData["Roles"], Is.Not.Null);
        }
    }
}