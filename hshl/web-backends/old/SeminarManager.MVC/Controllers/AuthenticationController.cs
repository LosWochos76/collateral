using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeminarManager.Model;
using SeminarManager.MVC.ViewModel;

namespace SeminarManager.MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private IRepository repository;

        public AuthenticationController(IRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult CheckCredentials([FromForm] Login login)
        {
            var user = repository.Persons.FindAdminByEmailAndPassword(login.Email, login.Password);
            
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
