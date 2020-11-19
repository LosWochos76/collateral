using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Extensions.Logging;
using SeminarManager.Model;

namespace src.Controllers
{
    public class SeminarController : Controller
    {
        private ILogger<SeminarController> logger;
        private ISeminarRepository repository;

        public SeminarController(ILogger<SeminarController> logger, ISeminarRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public IActionResult Index() 
        {
            logger.LogInformation("Index method called");
            var objects = repository.All();
            return View(objects);
        }

        public IActionResult Add()
        {
            var obj = new Seminar();
            return View("Edit", obj);
        }

        public IActionResult Edit(int id)
        {
            var obj = repository.ById(id);

            if (obj != null)
                return View("Edit", obj);
            else
                return NotFound();
        }

        public IActionResult Save([FromForm] Seminar obj)
        {
            if (!ModelState.IsValid)
                return View("Edit", obj);

            repository.Save(obj);
            return Redirect("Index");
        }

        public IActionResult Delete(int id)
        {
            var obj = repository.ById(id);

            if (obj != null)
            {
                repository.Delete(id);
                return Redirect("/Seminar/Index");
            }
            else
                return NotFound();
        }
    }
}