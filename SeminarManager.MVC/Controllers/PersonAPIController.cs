using Microsoft.AspNetCore.Mvc;
using SeminarManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarManager.MVC.Controllers
{
    public class PersonAPIController : Controller
    {
        private IPersonRepository repository;

        public PersonAPIController(IPersonRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            var objects = repository.All();
            return Json(objects);
        }
    }
}
