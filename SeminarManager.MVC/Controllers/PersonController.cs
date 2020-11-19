using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeminarManager.Model;

namespace SeminarManager.MVC.Controllers
{
    public class PersonController : Controller
    {
        private ILogger<PersonController> logger;
        private IPersonRepository repository;

        public PersonController(ILogger<PersonController> logger, IPersonRepository repository) 
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
            var obj = new Person();
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

        public IActionResult Save([FromForm] Person obj)
        {
            if (!ModelState.IsValid)
                return View("Edit", obj);

            repository.Save(obj);
            return Redirect("/Person/Index");
        }

        public IActionResult Delete(int id)
        {
            var obj = repository.ById(id);

            if (obj != null)
            {
                repository.Delete(id);
                return Redirect("/Person/Index");
            } 
            else
            {
                return NotFound();
            }
        }
    }
}