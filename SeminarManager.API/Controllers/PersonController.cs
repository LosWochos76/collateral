using Microsoft.AspNetCore.Mvc;
using SeminarManager.API.Misc;
using SeminarManager.Model;
using System;

namespace SeminarManager.API.Controllers
{
    public class PersonController : Controller
    {
        private IPersonRepository repository;

        public PersonController(IPersonRepository repository)
        {
            this.repository = repository;
        }

        [Route("/Person/")]
        [HttpPost]
        public IActionResult Create([FromBody] Person obj)
        {
            repository.Save(obj);
            return Json(new OperationResult());
        }

        [Route("/Person/")]
        [HttpGet]
        public IActionResult Read()
        {
            var objects = repository.All();
            return Json(objects);
        }

        [Route("/Person/{id}")]
        [HttpGet]
        public IActionResult Read(int id)
        {
            var obj = repository.ById(id);

            if (obj == null)
                return Json(new OperationResult("Object not found!"));

            return Json(obj);
        }

        [Route("/Person/{id}")]
        [HttpPut]
        public IActionResult Update([FromRoute] int id, [FromForm] Person obj)
        {
            if (repository.ById(id) == null)
                return Json(new OperationResult("Object not found!"));

            obj.ID = id;
            repository.Save(obj);
            return Json(new OperationResult());
        }

        [Route("/Person/{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var user = (Person)HttpContext.Items["user"];
            if (id == user.ID)
                return Json(new OperationResult("Cannot delete current user!"));

            repository.Delete(id);
            return Json(new OperationResult());
        }
    }
}