using Common.Models;
using Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ToDoService.Misc;

namespace ToDoService.Controllers;

public class AuthenticationController : Controller
{
    private readonly ILogger<AuthenticationController> logger;
    private readonly JwtTokenHelper jwtTokenHelper;
    private readonly IUserRepository userRepository;

    public AuthenticationController(
        ILogger<AuthenticationController> logger,
        JwtTokenHelper jwtTokenHelper,
        IUserRepository userRepository)
    {
        this.logger = logger;
        this.jwtTokenHelper = jwtTokenHelper;
        this.userRepository = userRepository;
    }

    [Route("/Login/")]
    [HttpPost]
    public IActionResult Login([FromBody] LoginViewModel login)
    {
        if (login is null || 
            string.IsNullOrEmpty(login.EMail) || 
            string.IsNullOrEmpty(login.Password))
            return BadRequest("Credentials are empty!");

        var user = userRepository.FindByLogin(login.EMail, login.Password);
        if (user is null)
            return BadRequest("User not found!");

        var token = jwtTokenHelper.CreateToken(user);
        return Ok(token);
    }
}