using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeminarManager.Model;
using SeminarManager.MVC.ViewModel;

namespace src.Controllers
{
    public class SeminarController : Controller
    {
        private ILogger<SeminarController> logger;
        private IRepository repository;

        public SeminarController(ILogger<SeminarController> logger, IRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public IActionResult Index() 
        {
            logger.LogInformation("Index method called");
            var objects = repository.Seminars.All();
            return View(objects);
        }

        public IActionResult Add()
        {
            var obj = new SeminarViewModel();
            ViewBag.Persons = repository.Persons.All();

            return View("Edit", obj);
        }

        public IActionResult Edit(int id)
        {
            var seminar = repository.Seminars.ById(id);
            var obj = SeminarViewModel.Convert(seminar);
            obj.Attendees = repository.Attendees.Get(seminar.ID);
            ViewBag.Persons = repository.Persons.All();

            if (obj != null)
                return View("Edit", obj);
            else
                return NotFound();
        }

        public IActionResult Save([FromForm] SeminarViewModel obj)
        {
            if (obj.Teacher == 0) 
                ModelState.AddModelError("TeacherID", "You need to select a teacher for this seminar!");
            
            if (obj.Attendees.Count == 0)
                ModelState.AddModelError("Attendees", "Your need to add at least one attendee to this seminar!");

            if (!ModelState.IsValid)
            {
                ViewBag.Persons = repository.Persons.All();
                return View("Edit", obj);
            }

            var seminar = SeminarViewModel.Convert(obj);
            repository.Seminars.Save(seminar);
            repository.Attendees.Save(seminar.ID, obj.Attendees);

            return Redirect("Index");
        }

        public IActionResult Delete(int id)
        {
            var obj = repository.Seminars.ById(id);

            if (obj != null)
            {
                repository.Seminars.Delete(id);
                return Redirect("/Seminar/Index");
            }
            else
                return NotFound();
        }
    }
}