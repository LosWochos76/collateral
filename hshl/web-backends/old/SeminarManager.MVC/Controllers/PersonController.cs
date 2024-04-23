using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeminarManager.Model;
using SeminarManager.MVC.ViewModel;

namespace SeminarManager.MVC.Controllers
{
    public class PersonController : Controller
    {
        private ILogger<PersonController> logger;
        private IRepository repository;

        public PersonController(ILogger<PersonController> logger, IRepository repository) 
        {
            this.logger = logger;
            this.repository = repository;
        }

        public IActionResult Index() 
        {
            logger.LogInformation("Index method called");
            var objects = repository.Persons.All();
            return View(objects);
        }

        public IActionResult Add() 
        {
            var obj = new PersonViewModel();
            return View("Edit", obj);
        }

        public IActionResult Edit(int id)
        {
            var person = repository.Persons.ById(id);
            var obj = PersonViewModel.Convert(person);

            if (obj != null)
                return View("Edit", obj);
            else
                return NotFound();
        }

        public IActionResult Save([FromForm] PersonViewModel obj)
        {
            if (!ModelState.IsValid)
                return View("Edit", obj);

            var person = PersonViewModel.Convert(obj);
            repository.Persons.Save(person);
            return Redirect("/Person/Index");
        }

        public IActionResult Delete(int id)
        {
            var obj = repository.Persons.ById(id);
            if (obj != null)
            {
                repository.Persons.Delete(id);
                return Redirect("/Person/Index");
            } 
            else
            {
                return NotFound();
            }
        }
    }
}