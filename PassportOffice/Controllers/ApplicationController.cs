using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PassportOffice.Models;

namespace PassportOffice.Controllers
{
    public class ApplicationController : Controller
    {
        // GET: ApplicationController   
        public IActionResult Applicationform()
        {
            return View("ApplicationForm");
        }

        public IActionResult AllApplications()
        {
            return View("AllApplications");
        }

        // GET: ApplicationController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApplicationController/Create
        [HttpPost]
        public ActionResult Create(Application app)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Create");
            }
            else
            {
                ModelState.AddModelError("", "В заявлении присутствуют ошибки.");
                return View(app);
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: ApplicationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ApplicationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ApplicationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ApplicationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ApplicationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
