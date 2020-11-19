using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeminarManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarManager.MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private IPersonRepository repository;

        public AuthenticationController(IPersonRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult CheckCredentials([FromForm] LoginModel login)
        {
            var user = repository.FindAdminByEmailAndPassword(login);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found!");
                return View("Login", login);
            }

            HttpContext.Session.SetInt32("user_id", user.ID);
            return Redirect("/Person/Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Items["user"] = null;
            return Redirect("/Authentication/Login");
        }
    }
}
