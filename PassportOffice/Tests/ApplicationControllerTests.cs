using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PassportOffice.Controllers;
using PassportOffice.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PassportOffice.Tests
{
    public class ApplicationControllerTests
    {
        private WebAppDbContext _context;
        private ApplicationController _controller;
        private Guid testUserId = Guid.NewGuid();

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<WebAppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new WebAppDbContext(options);

            // Добавим тестовые данные в контекст
            _context.Documents.AddRange(new[]
            {
                new Document { Id = 1, UserId = testUserId, Authority = "Passport Office", TypeOfDocument = new TypeOfDocument { Id = 1, Name = "Passport" } },
                new Document { Id = 2, UserId = testUserId, Authority = "DMV", TypeOfDocument = new TypeOfDocument { Id = 2, Name = "Driver License" } }
            });

            _context.TypesOfApplication.Add(new TypeOfApplication { Id = 1, Name = "Study" });
            _context.SaveChanges();

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, testUserId.ToString())
            }, "mock"));

            _controller = new ApplicationController(_context)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };
        }

        [Test]
        public async Task MakeApplication_Get_ReturnsViewWithCorrectViewBag()
        {
            // Arrange
            int id = 123;
            int typeOfApplicationId = 1;
            int statusId = 1;
            DateTime startDate = new DateTime(2024, 1, 1);
            string description = "Test description";

            // Act
            var result = await _controller.MakeApplication(id, typeOfApplicationId, statusId, startDate, description) as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(_controller.ViewBag.Id, Is.EqualTo(id));
            Assert.That(_controller.ViewBag.StatusId, Is.EqualTo(statusId));
            Assert.That(_controller.ViewBag.StartDate, Is.EqualTo(startDate));
            Assert.That(_controller.ViewBag.Description, Is.EqualTo(description));
            Assert.That(_controller.ViewBag.UserId, Is.EqualTo(testUserId.ToString()));
            Assert.That(_controller.ViewBag.UserDocuments, Is.InstanceOf<List<Document>>());
            Assert.That(_controller.ViewBag.TypesOfApplication, Is.InstanceOf<List<TypeOfApplication>>());
        }

        [Test]
        public async Task MakeApplication_Post_WithValidData_RedirectsToAllApplications()
        {
            // Arrange
            int typeOfApplicationId = 1;
            DateTime startDate = DateTime.Today;
            string description = "Test application";
            List<int> attachedDocumentsIds = new List<int> { 1, 2 };

            // Act
            var result = await _controller.MakeApplication(typeOfApplicationId, startDate, description, attachedDocumentsIds) as RedirectToActionResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ActionName, Is.EqualTo("AllApplications"));

            var applications = await _context.Applications.ToListAsync();
            Assert.That(applications.Count, Is.EqualTo(1));
            Assert.That(applications[0].TypeOfApplicationId, Is.EqualTo(typeOfApplicationId));
            Assert.That(applications[0].Description, Is.EqualTo(description));
            Assert.That(applications[0].UserId, Is.EqualTo(testUserId));
        }
    }
}