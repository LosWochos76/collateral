using Microsoft.AspNetCore.Mvc;
using MvcApp.Model;

namespace MvcApp.Controllers;

public class LoginController : Controller
{
    public IActionResult Index([FromForm] LoginModel? login)
    {
        if (login is null || login.Username is null || login.Password is null)
            return View();

        if (login.Username.Equals("stuckenholz") && 
            login.Password.Equals("secret"))
            return Redirect("/Home");

        return View();
    }
}